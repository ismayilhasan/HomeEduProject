using EduHome.DAL;
using EduHome.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EduHome.Controllers
{
    public class CourseController : Controller
    {
        private readonly AppDbContext _dbContext;

        public CourseController(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> Details(int id)
        {
            var course = await _dbContext.Courses.Where(x => !x.IsDeleted && x.Id == id).FirstOrDefaultAsync();
            var categories = await _dbContext.Categories.Where(x => !x.IsDeleted && x.Id == id).Include(c => c.Courses).ToListAsync();
            var blog = await _dbContext.Blogs.Where(b => !b.IsDeleted).OrderByDescending(b => b.Id).ToListAsync();

            var courseViewModel = new CourseViewModel
            {
                Course = course,
                Blogs = blog,
                Categories = categories
            };
            return View(courseViewModel);
        }

        public IActionResult Search(string searchedText)
        {
            if(string.IsNullOrEmpty(searchedText))
            {
                return NoContent(); 
            }
            var courses = _dbContext.Courses.Where(x => x.Title.ToLower().Contains(searchedText.ToLower())).ToList();
            return PartialView("_SearchedCoursePartial", courses);
        }
    }
}
