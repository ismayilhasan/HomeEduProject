using EduHome.DAL;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EduHome.ViewComponents
{
    public class EventItemViewComponent : ViewComponent
    {
        private readonly AppDbContext _dbContext;

        public EventItemViewComponent(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var events = await _dbContext.Events.Where(e => !e.IsDeleted).OrderByDescending(e => e.Id).ToListAsync();
            return View(events);
        }
    }
}
