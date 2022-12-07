using EduHome.Models.Entities;

namespace EduHome.Areas.Admin.Models
{
    public class ContactMessageReadViewModel
    {
        public List<ContactMessage> ContactMessages { get; set; }
        public bool IsAllReadMessages { get; set; }
    }
}
