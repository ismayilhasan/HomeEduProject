using EduHome.Models.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Drawing.Printing;

namespace EduHome.DAL
{
    public class AppDbContext : IdentityDbContext<User>
    {
   
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }

        public DbSet<Slider> Sliders { get; set; }
        public DbSet<Teacher> Teachers { get; set; }
        public DbSet<Blog> Blogs { get; set; }
        public DbSet<FeedbackSlider> FeedbackSliders { get; set; }

        public DbSet<Course> Courses { get; set; }
        public DbSet<Category> Categories { get; set; }

        public DbSet<Event> Events { get; set; }
        public DbSet<Speaker> Speakers { get; set; }
        public DbSet<EventSpeaker> EventSpeakers { get; set; }
        public DbSet<Contact> Contacts { get; set; }
        public DbSet<ContactMessage> ContactMessages { get; set; }

    }
}
