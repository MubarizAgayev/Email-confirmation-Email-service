using Project.Models;

namespace Project.ViewModels
{
    public class HomeVM
    {
        public List<Slider> Sliders { get; set; }
        public List<Course> Courses { get; set; }
        public List<CourseHost> CourseHosts { get; set; }
        public List<CourseImage> CourseImages { get; set; }
        public List<Events> Events { get; set; }
        public List<News> News { get; set; }
        public List<NewsOwner> NewsOwners { get; set; }
        public List<NewsImage> NewsImages { get; set; }
    }
}
