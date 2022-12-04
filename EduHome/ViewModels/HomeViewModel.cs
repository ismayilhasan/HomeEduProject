using EduHome.Models.Entities;

namespace EduHome.ViewModels
{
    public class HomeViewModel
    {
        public List<Slider> Sliders { get; set; } = new List<Slider>();
        public List<Blog> Blogs { get; set; } = new List<Blog>();

    }
}
