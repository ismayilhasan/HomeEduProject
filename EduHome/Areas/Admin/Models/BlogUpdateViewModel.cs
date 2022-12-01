namespace EduHome.Areas.Admin.Models
{
    public class BlogUpdateViewModel
    {
        public string ImageUrl { get; set; } = String.Empty;
        public IFormFile Image { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public DateTime Created { get; set; }
        public string Description { get; set; }
    }
}
