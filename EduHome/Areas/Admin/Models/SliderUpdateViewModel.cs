namespace EduHome.Areas.Admin.Models
{
    public class SliderUpdateViewModel
    {
        public string ImageUrl { get; set; } = String.Empty;
        public IFormFile Image { get; set; }

        public string Title { get; set; }
        public string Subtitle { get; set; }
        public string ButtonText { get; set; }

    }
}
