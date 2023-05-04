using System.ComponentModel.DataAnnotations;

namespace Project.Areas.ViewModels
{
    public class CourseCreateVM
    {
        [Required]
        public string CourseName { get; set; }
        [Required]
        public string CourseDescribtion { get; set; }
        [Required]
        public int Sale { get; set; }
        [Required]
        public string Price { get; set; }

        public int CourseHostId { get; set; }

        public List<IFormFile> Photos { get; set; }
    }
}
