using EduHome.Models.Entities;

namespace EduHome.ViewModels
{
    public class ContactViewModel
    {
        public Contact Contact { get; set; } = new();
        public ContactMessageViewModel ContactMessage { get; set; }
    }
}
