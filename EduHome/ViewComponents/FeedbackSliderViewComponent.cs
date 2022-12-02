using EduHome.DAL;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EduHome.ViewComponents
{
    public class FeedbackSliderViewComponent : ViewComponent
    {
        private readonly AppDbContext _dbContext;
        public FeedbackSliderViewComponent(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var feedbackSliders = await _dbContext.FeedbackSliders.Where(t => !t.IsDeleted).ToListAsync();
            return View(feedbackSliders);
        }
    }
}
