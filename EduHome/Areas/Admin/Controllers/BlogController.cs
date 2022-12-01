using EduHome.Areas.Admin.Data;
using EduHome.Areas.Admin.Models;
using EduHome.DAL;
using EduHome.Models.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EduHome.Areas.Admin.Controllers
{
    public class BlogController : BaseController
    {
        private readonly AppDbContext _dbContext;

        public BlogController(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IActionResult> Index()
        {
            var blogs = await _dbContext.Blogs.ToListAsync();


            return View(blogs);
        }
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();
            var blog = await _dbContext.Blogs.Where(x => x.Id == id).FirstOrDefaultAsync();

            if (blog == null) return NotFound();

            if (blog.Id == null) return BadRequest();



            return View(blog);
        }

       
        public IActionResult Create()
        {

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(BlogCreateViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            if (!model.Image.IsImage())
            {
                ModelState.AddModelError("Image", "Please Enter Image");
                return View();
            }

            if (!model.Image.IsAllowedSize(5))
            {
                ModelState.AddModelError("Image", "Image Size can Contain max 5 mb");
                return View();
            }

            var unicalFileName = await model.Image.GenerateFile(Constants.BlogPath);


            var blog = new Blog()
            {
                ImageUrl = unicalFileName,
                Title = model.Title,
                Author = model.Author,
                Description = model.Description,
                Created = model.Created,

            };

            await _dbContext.Blogs.AddAsync(blog);


            await _dbContext.SaveChangesAsync();

            return Redirect(nameof(Index));
        }

        public async Task<IActionResult> Update(int? id)
        {
            if (id == null)
                return NotFound();

            var teacher = await _dbContext.Blogs.FindAsync(id);
            return View(new BlogUpdateViewModel
            {
                ImageUrl = teacher.ImageUrl,
                Title = teacher.Title,
                Author = teacher.Author,
                Description = teacher.Description,
                Created = teacher.Created


            });
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(int? id, BlogUpdateViewModel model)
        {
            if (id == null) return NotFound();

            var blog = await _dbContext.Blogs.FindAsync(id);

            if (blog == null) return NotFound();

            if (blog.Id == null)
                return BadRequest();

            if (!ModelState.IsValid)
            {
                return View(new BlogUpdateViewModel
                {
                    ImageUrl = model.ImageUrl
                });

            }
            if (!model.Image.IsImage())
            {
                ModelState.AddModelError("Image", "Please Enter Image");
                return View(new BlogUpdateViewModel
                {
                    ImageUrl = model.ImageUrl
                });
            }

            if (!model.Image.IsAllowedSize(5))
            {
                ModelState.AddModelError("Image", "Image Size can Contain max 5 mb");
                return View(new BlogUpdateViewModel
                {
                    ImageUrl = model.ImageUrl
                });
            }

            var path = Path.Combine(Constants.RootPath, "img", blog.ImageUrl);




            if (System.IO.File.Exists(path))
                System.IO.File.Delete(path);

            var unicalFileName = await model.Image.GenerateFile(Constants.BlogPath);


            blog.ImageUrl = unicalFileName;
            blog.Title = model.Title;
            blog.Description = model.Description;
            blog.Author = model.Author;
            blog.Created = DateTime.Now;



            await _dbContext.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();

            var blog = await _dbContext.Blogs.FindAsync(id);

            if (blog.Id == null) BadRequest();


            var path = Path.Combine(Constants.RootPath, "img", blog.ImageUrl);

            if (System.IO.File.Exists(path))
                System.IO.File.Delete(path);

            _dbContext.Blogs.Remove(blog);

            await _dbContext.SaveChangesAsync();


            return RedirectToAction(nameof(Index));
        }

    }
}
