using EduHome.Models.Entities;

namespace EduHome.ViewModels
{
    public class BlogViewModel
    {
        public List<Blog> Blogs { get; set; } = new List<Blog>();
        public List<Blog> LatesBlogs { get; set; } = new List<Blog>();

        public Blog Blog { get; set; } = new Blog();
    }
}
