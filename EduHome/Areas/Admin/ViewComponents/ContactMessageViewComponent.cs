using EduHome.Areas.Admin.Models;
using EduHome.DAL;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EduHome.Areas.Admin.ViewComponents
{
    public class ContactMessageViewComponent : ViewComponent
    {
        private readonly AppDbContext _dbContext;

        public ContactMessageViewComponent(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var message = await _dbContext.ContactMessages.ToListAsync();

            var isAllReadMessage = message.All(x => x.IsRead);

            return View(new ContactMessageReadViewModel
            {
                ContactMessages = message,
                IsAllReadMessages = isAllReadMessage
            });
        }
    }
}
