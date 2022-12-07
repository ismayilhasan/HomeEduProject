﻿namespace EduHome.Models.Entities
{
    public class ContactMessage : Entity
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string? Subject { get; set; }
        public string Message { get; set; }
        public bool IsDeleted { get; set; }
        public bool IsRead { get; set; }
    }
}
