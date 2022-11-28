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
    }
}
