namespace EduHome.Models.Entities
{
    public class Course : Entity
    {
        public bool IsDeleted { get; set; }
        public string ImageUrl { get; set; }
        public Category Category { get; set; }
        public int CategoryId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string About { get; set; }
        public string ApplyDetails { get; set; }
        public string SertificationContent { get; set; }
        public DateTime StartTime { get; set; }
        public string Duration { get; set; }
        public string ClassDuration { get; set; }
        public string SkilLevel { get; set; }
        public string Language { get; set; }
        public int StudentCount { get; set; }
        public string Assesment { get; set; }
    }
}
