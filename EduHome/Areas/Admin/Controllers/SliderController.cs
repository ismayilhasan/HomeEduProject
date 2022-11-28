using EduHome.Areas.Admin.Models;
using EduHome.DAL;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EduHome.Areas.Admin.Controllers
{
    public class SliderController : BaseController
    {
        private readonly AppDbContext _dbContext;

        public SliderController(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }



        public async Task<IActionResult> Index()
        {
            var Sliders = await _dbContext.Sliders.ToListAsync();

            
            return View(Sliders);
        }

        public IActionResult Create()
        {

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]

        public async Task<IActionResult> Create(SliderCreateModel model)
        {
            if(!ModelState.IsValid)
            {
                return View(model);
            }
            if (!model.Image.ContentType.Contains("image"))
            {
                ModelState.AddModelError("Image", "Please Enter Image");
                return View();
            }

            if (model.Image.Length > 1024*1024)
            {
                ModelState.AddModelError("Image", "Image Size can Contain max 1 mb");
                return View();
            }
                

            return Redirect(nameof(Index));
        }
    }
}
