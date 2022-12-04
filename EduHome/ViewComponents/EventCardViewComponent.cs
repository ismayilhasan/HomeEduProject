using EduHome.DAL;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EduHome.ViewComponents
{
    public class EventCardViewComponent : ViewComponent
    {
        private readonly AppDbContext _dbContext;

        public EventCardViewComponent(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var events = await _dbContext.Events.Where(e => !e.IsDeleted).ToListAsync();
            return View(events);
        }
    }
}
