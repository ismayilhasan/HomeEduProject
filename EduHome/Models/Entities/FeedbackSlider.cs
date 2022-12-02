namespace EduHome.Models.Entities
{
    public class FeedbackSlider : Entity
    {
        public bool IsDeleted { get; set; }
        public string ImageUrl { get; set; }
        public string Feedback { get; set; }
        public string Position {get; set; }

        public string Name { get; set; }


    }
}
