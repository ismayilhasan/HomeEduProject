using EduHome.DAL;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EduHome.Controllers
{
    public class Event1Controller : Controller
    {
        private readonly AppDbContext _dbContext;

        public Event1Controller(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IActionResult Index()
        {
            return View();  
        }
        public async Task<IActionResult> Details(int? id)
        {
            if (id is null) return NotFound();
            var events = await _dbContext.Events.Where(c => !c.IsDeleted && c.Id == id).Include(e => e.EventSpeakers).ThenInclude(e => e.Speaker).FirstOrDefaultAsync();
            if (events is null) return NotFound();


            return View(events);
        }
    }
}
