using System.ComponentModel.DataAnnotations;

namespace EduHome.Models.Entities
{
    public class Event : Entity
    {
        public bool IsDeleted { get; set; }
        public string ImageUrl { get; set; }
        public string Title { get; set; }
        [DataType(DataType.DateTime)]
        public DateTime StartTime { get; set; }
        [DataType(DataType.DateTime)]
        public DateTime EndTime { get; set; }
        public string Venue { get; set; }
        public string Content { get; set; }

    }
}
