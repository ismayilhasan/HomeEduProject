namespace EduHome.Areas.Admin.Models
{
    public class FeedbackSliderCreateViewModel
    {
        public string ImageUrl { get; set; } = String.Empty;
        public IFormFile Image { get; set; }
        public string Name { get; set; }
        public string Feedback { get; set; }
        public string Position { get; set; }


    }
}
