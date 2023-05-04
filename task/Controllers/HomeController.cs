using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using Project.Data;
using Project.Models;
using Project.ViewModels;

namespace Project.Controllers
{
    public class HomeController : Controller
    {
        private readonly AppDbContext _context;
        public HomeController(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {

            List<Slider> sliders = await _context.Sliders.Where(m => !m.SoftDelete).ToListAsync();
            List<Course> courses = await _context.Courses.Where(m => !m.SoftDelete).ToListAsync();
            List<CourseHost> courseHosts = await _context.CourseHosts.Where(m => !m.SoftDelete).ToListAsync();
            List<CourseImage> courseImages = await _context.CourseImages.Where(m => !m.SoftDelete).ToListAsync();
            List<Events> events = await _context.Events.Where(m => !m.SoftDelete).ToListAsync();
            List<News> news = await _context.News.Where(m => !m.SoftDelete).ToListAsync();
            List<NewsOwner> newsOwners = await _context.NewsOwners.Where(m => !m.SoftDelete).ToListAsync();
            List<NewsImage> newsImages = await _context.NewsImages.Where(m => !m.SoftDelete).ToListAsync();

            HomeVM model = new HomeVM()
            {
                Sliders = sliders,
                Courses= courses,
                CourseHosts = courseHosts,
                CourseImages = courseImages,
                Events = events,
                News = news,
                NewsOwners = newsOwners,
                NewsImages = newsImages,
            };

            return View(model);
        }

        
    }
}