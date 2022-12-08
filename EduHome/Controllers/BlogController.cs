using EduHome.DAL;
using EduHome.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EduHome.Controllers
{

    public class BlogController : Controller
    {
        private readonly AppDbContext _dbContext;
        private int _blogCount;
        public BlogController(AppDbContext dbContext)
        {
            _dbContext = dbContext;
            _blogCount = _dbContext.Blogs.Count();
        }

        public async Task<IActionResult> Index()
        {
            var blogs = await _dbContext.Blogs.Where(b => !b.IsDeleted).ToListAsync();
            return View(blogs);
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id is null) return NotFound();

            var blog = await _dbContext.Blogs.SingleOrDefaultAsync(b => b.Id == id);

            if (blog.Id != id) return NotFound();
           
            var latesBlog = await _dbContext.Blogs
               .Where(b => !b.IsDeleted)
               .OrderByDescending(b => b.Id)
               .ToListAsync();

            var blogViewModel = new BlogViewModel { Blog = blog, LatesBlogs = latesBlog };

            return View(blogViewModel);
        }

        public async Task<IActionResult> Partial(int skip)
        {
            if (skip >= _blogCount)
                return BadRequest();
            var blogs = await _dbContext.Blogs.Skip(skip).Take(3).ToListAsync();
            return PartialView("_BlogPartial", blogs);
        }


    }
}
