
using EduHome.DAL;
using EduHome.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace EduHome.Controllers
{
    public class HomeController : Controller
    {
        private readonly AppDbContext _dbContext;

        public HomeController(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IActionResult Index()
        {
            var sliders = _dbContext.Sliders.ToList();
            var blogs = _dbContext.Blogs.ToList();
            var courses = _dbContext.Courses.ToList();
            var events = _dbContext.Events.ToList();
            var homeViewModel = new HomeViewModel()
            {
                Sliders = sliders,
                Blogs = blogs,
                Courses = courses,
                Events = events
            };

            return View(homeViewModel);
        }


    }
}