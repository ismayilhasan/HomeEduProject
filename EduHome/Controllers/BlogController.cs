using EduHome.DAL;
using EduHome.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EduHome.Controllers
{
    public class BlogController : Controller
    {
        private readonly AppDbContext _dbContext;

        public BlogController(AppDbContext dbContext)
        {
            _dbContext = dbContext;
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


    }
}
