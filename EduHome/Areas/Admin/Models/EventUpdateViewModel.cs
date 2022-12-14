using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace EduHome.Areas.Admin.Models
{
    public class EventUpdateViewModel
    {
        public IFormFile? Image { get; set; }
        public string? ImageUrl { get; set; }
        public string Title { get; set; }
        [DataType(DataType.DateTime)]
        public DateTime StartTime { get; set; }
        [DataType(DataType.DateTime)]
        public DateTime EndTime { get; set; }
        public string Venue { get; set; }
        public string Content { get; set; }
        public List<SelectListItem>? Speakers { get; set; }
        public List<int> SpeakerIds { get; set; }
    }
}
