using EduHome.Models.Entities;
using System.ComponentModel.DataAnnotations;

namespace EduHome.ViewModels
{
    public class ContactMessageViewModel
    {
        public string Name { get; set; }
        [EmailAddress]
        public string Email { get; set; }
        public string Subject { get; set; }
        public string Message { get; set; }
        public string? Adress { get; set; }

    }

  
}
