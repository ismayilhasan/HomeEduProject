namespace EduHome.Models.Entities
{
    public class Category : Entity
    {
        public bool IsDeleted { get; set; }
        public string Name { get; set; }
        public List<Course> Courses { get; set; }
    }
}
