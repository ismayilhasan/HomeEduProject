namespace EduHome.Models.Entities
{
    public class Contact : Entity
    {
        public string Message { get; set; }
        public string Adress { get; set; }
        public string ContactNumber { get; set; }
        public string Website { get; set; }

        public bool IsDeleted { get; set; }
    }
}
