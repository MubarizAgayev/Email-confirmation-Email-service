namespace Project.Models
{
    public class Course : BaseEntity
    {
        public string CourseName { get; set; }
        public string CourseDescribtion { get; set; }
        public int Sale { get; set; }
        public decimal Price { get; set; }
        public int CourseHostId { get; set; }
        public CourseHost CourseHost { get; set; }
        public ICollection<CourseImage> Images { get; set; }
    }
}
