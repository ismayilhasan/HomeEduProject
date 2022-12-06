using EduHome.DAL;
using EduHome.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace EduHome.Controllers
{
    public class AboutController : Controller
    {
        private readonly AppDbContext _dbContext;

        public AboutController(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IActionResult Index()
        {
            var Teachers = _dbContext.Teachers.Where(x => !x.IsDeleted).ToList();
            var Events = _dbContext.Events.Where(x => !x.IsDeleted).ToList();
            var aboutViewModel = new AboutViewModel
            {
                teachers = Teachers,
                events = Events
            };
            return View(aboutViewModel);
        }
    }
}
