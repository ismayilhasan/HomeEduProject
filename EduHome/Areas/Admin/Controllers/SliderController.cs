using EduHome.Areas.Admin.Data;
using EduHome.Areas.Admin.Models;
using EduHome.DAL;
using EduHome.Models.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Drawing.Printing;
using System.Security.Cryptography.X509Certificates;

namespace EduHome.Areas.Admin.Controllers
{
    public class SliderController : BaseController
    {
        private readonly AppDbContext _dbContext;
        private readonly IWebHostEnvironment _environment;

   

        public SliderController(AppDbContext dbContext, IWebHostEnvironment environment)
        {
            _dbContext = dbContext;
            _environment = environment;
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

        public async Task<IActionResult> Create(SliderUpdateViewModel model)
        {
            if(!ModelState.IsValid)
            {
                return View(model);
            }
            if (!model.Image.IsImage())
            {
                ModelState.AddModelError("Image", "Please Enter Image");
                return View();
            }

            if (!model.Image.IsAllowedSize(2))
            {
                ModelState.AddModelError("Image", "Image Size can Contain max 1 mb");
                return View();
            }

            var unicalFileName = await model.Image.GenerateFile(Constants.SliderPath);
 

            var slider = new Slider()
            {
                ImageUrl = unicalFileName,
                Title = model.Title,
                Subtitle = model.Subtitle,
                ButtonText = model.ButtonText,
                ButtonSrc = "test"
            };

            await _dbContext.Sliders.AddAsync(slider);


            await _dbContext.SaveChangesAsync();

            return Redirect(nameof(Index));
        }

        public async Task<IActionResult> Update(int? id)
        {
            if (id == null)
                return NotFound();

            var sliderItem = await _dbContext.Sliders.FindAsync(id);
            return View(new SliderUpdateViewModel { 
                ImageUrl = sliderItem.ImageUrl
            });



        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(int? id, SliderUpdateViewModel model)
        {
            if (id is null) return NotFound();

            var sliderItem = await _dbContext.Sliders.Where(x => x.Id == id).FirstOrDefaultAsync();

            if (sliderItem is null) return NotFound();

            if (sliderItem.Id != id) return BadRequest();

            if (model.Image != null)
            {

                if (!ModelState.IsValid)
                {
                    return View(new SliderUpdateViewModel
                    {

                        ImageUrl = sliderItem.ImageUrl,

                    });
                }

                if (!model.Image.IsImage())
                {
                    ModelState.AddModelError("Image", "Sekil secilmelidir");
                    return View(new SliderUpdateViewModel
                    {

                        ImageUrl = sliderItem.ImageUrl,

                    });
                }
                if (!model.Image.IsAllowedSize(2))
                {
                    ModelState.AddModelError("Image", "Sekil secilmelidir");
                    return View(new SliderUpdateViewModel
                    {

                        ImageUrl = sliderItem.ImageUrl,

                    });
                }



                var path = Path.Combine(Constants.SliderPath, "img", "slider", sliderItem.ImageUrl);

                if (System.IO.File.Exists(path))
                    System.IO.File.Delete(path);

                var unicalFileName = await model.Image.GenerateFile(Constants.SliderPath);
                sliderItem.ImageUrl = unicalFileName;
            }
            sliderItem.Subtitle = model.Subtitle;
            sliderItem.Title = model.Title;
            sliderItem.ButtonText = model.ButtonText;
            sliderItem.ButtonSrc = "testr";


            await _dbContext.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }




    }
}
