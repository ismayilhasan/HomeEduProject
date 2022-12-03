using EduHome.Models.Entities;

namespace EduHome.ViewModels
{
    public class CourseViewModel
    {
        public List<Blog> Blogs { get; set; }
        public Course Course { get; set; }
        public List<Category> Categories { get; set; }
    }
}
