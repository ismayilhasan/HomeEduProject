using EduHome.Areas.Admin.Data;
using EduHome.Areas.Admin.Models;
using EduHome.DAL;
using EduHome.Models.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EduHome.Areas.Admin.Controllers
{
    public class FeedbackSliderController : BaseController
    {
        private readonly AppDbContext _dbContext;

        public FeedbackSliderController(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IActionResult> Index()
        {
            var feedbackSliders = await _dbContext.FeedbackSliders.ToListAsync();


            return View(feedbackSliders);
        }
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();
            var feedbackSlider = await _dbContext.FeedbackSliders.Where(x => x.Id == id).FirstOrDefaultAsync();

            if (feedbackSlider == null) return NotFound();

            if (feedbackSlider.Id == null) return BadRequest();



            return View(feedbackSlider);
        }
        public IActionResult Create()
        {
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]

        public async Task<IActionResult> Create(FeedbackSliderCreateViewModel model)
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

            var unicalFileName = await model.Image.GenerateFile(Constants.FeedbackPath);


            var FeedbackSlider = new FeedbackSlider()
            {
                ImageUrl = unicalFileName,
                Name = model.Name,
                Feedback = model.Feedback,
                Position = model.Position
            };

            await _dbContext.FeedbackSliders.AddAsync(FeedbackSlider);


            await _dbContext.SaveChangesAsync();

            return Redirect(nameof(Index));
        }
        public async Task<IActionResult> Update(int? id)
        {
            if (id == null)
                return NotFound();

            var feedbackSlider = await _dbContext.FeedbackSliders.FindAsync(id);
            return View(new FeedbackSliderUpdateViewModel
            {
                ImageUrl = feedbackSlider.ImageUrl,
                Name = feedbackSlider.Name,
                Feedback = feedbackSlider.Feedback,
                Position = feedbackSlider.Position
                


            });



        }


        [HttpPost]
        [ValidateAntiForgeryToken]


        public async Task<IActionResult> Update(int? id, FeedbackSliderUpdateViewModel model)
        {
            if (id == null) return NotFound();

            var feedbackSlider = await _dbContext.FeedbackSliders.FindAsync(id);

            if (feedbackSlider == null) return NotFound();

            if (feedbackSlider.Id == null)
                return BadRequest();

            if (!ModelState.IsValid)
            {
                return View(new TeacherUpdateViewModel
                {
                    ImageUrl = model.ImageUrl
                });

            }
            if (!model.Image.IsImage())
            {
                ModelState.AddModelError("Image", "Please Enter Image");
                return View(new TeacherUpdateViewModel
                {
                    ImageUrl = model.ImageUrl
                });
            }

            if (!model.Image.IsAllowedSize(5))
            {
                ModelState.AddModelError("Image", "Image Size can Contain max 5 mb");
                return View(new SliderUpdateViewModel
                {
                    ImageUrl = model.ImageUrl
                });
            }

            var path = Path.Combine(Constants.RootPath, "img", feedbackSlider.ImageUrl);




            if (System.IO.File.Exists(path))
                System.IO.File.Delete(path);

            var unicalFileName = await model.Image.GenerateFile(Constants.FeedbackPath);


            feedbackSlider.ImageUrl = unicalFileName;
            feedbackSlider.Name = model.Name;
            feedbackSlider.Feedback = model.Feedback;
            feedbackSlider.Position = model.Position;



            await _dbContext.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();

            var feedbackSlider = await _dbContext.FeedbackSliders.FindAsync(id);

            if (feedbackSlider.Id == null) BadRequest();


            var path = Path.Combine(Constants.RootPath, "img", feedbackSlider.ImageUrl);

            if (System.IO.File.Exists(path))
                System.IO.File.Delete(path);

            _dbContext.FeedbackSliders.Remove(feedbackSlider);

            await _dbContext.SaveChangesAsync();


            return RedirectToAction(nameof(Index));
        }
    }
}
