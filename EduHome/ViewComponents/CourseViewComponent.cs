using EduHome.DAL;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EduHome.ViewComponents
{
    public class CourseViewComponent : ViewComponent
    {
        private readonly AppDbContext _dbContext;

        public CourseViewComponent(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var courses = await _dbContext.Courses.Where(x => !x.IsDeleted).Include(c => c.Category).ToListAsync();

            return View(courses);
        }
    }
}
