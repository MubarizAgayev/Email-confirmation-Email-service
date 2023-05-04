using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Text.RegularExpressions;
using Project.Areas.ViewModels;
using Project.Data;
using Project.Models;
using Project.Helpers;

namespace Project.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CourseController : Controller
    {

        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _env;
        public CourseController(AppDbContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }
        public async Task<IActionResult> Index()
        {
            IEnumerable<Course> courses = await _context.Courses.Include(m=>m.CourseHost).Include(m=>m.Images).ToListAsync();
            return View(courses);
        }

        [HttpGet]
        public async Task<IActionResult> Detail(int? id)
        {
            if (id is null) return BadRequest();
            Course course = await _context.Courses.Include(m=>m.CourseHost).Include(m=>m.Images).FirstOrDefaultAsync(m=>m.Id == id);

            if (course is null) return NotFound();

            ViewBag.desc = Regex.Replace(course.CourseDescribtion, "<.*?>", string.Empty);
            return View(course);
        }

        [HttpGet]
        public async Task<IActionResult> Create(int? id)
        {
            

            ViewBag.hosts = await GetCategoriesAsync();

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CourseCreateVM model)
        {
            try
            {
                ViewBag.hosts = await GetCategoriesAsync();

                if (!ModelState.IsValid)
                {
                    return View(model);
                }

                foreach (var photo in model.Photos)
                {
                    if (!photo.CheckFileType("image/"))
                    {
                        ModelState.AddModelError("Photo", "File type must be image");
                        return View();
                    }

                    if (!photo.CheckFileSize(200))
                    {
                        ModelState.AddModelError("Photo", "Image size must be max 200kb");
                        return View();
                    }
                }


                List<CourseImage> courseImages = new();

                foreach (var photo in model.Photos)
                {
                    string fileName = Guid.NewGuid().ToString() + "_" + photo.FileName;

                    string path = FileHelper.GetFilePath(_env.WebRootPath, "images", fileName);

                    await FileHelper.SaveFileAsync(path, photo);

                    CourseImage productImage = new()
                    {
                        Image = fileName
                    };

                    courseImages.Add(productImage);
                }

                courseImages.FirstOrDefault().IsMain = true;

                decimal convertedPrice = decimal.Parse(model.Price.Replace(".", ","));

                Course newcourse = new()
                {
                    CourseName = model.CourseName,
                    CourseDescribtion= model.CourseDescribtion,
                    Sale = model.Sale,
                    Price = convertedPrice,
                    CourseHostId = model.CourseHostId,
                    Images = courseImages
                };


                await _context.CourseImages.AddRangeAsync(courseImages);
                await _context.Courses.AddAsync(newcourse);
                await _context.SaveChangesAsync();


                return RedirectToAction(nameof(Index));

            }
            catch (Exception)
            {

                throw;
            }
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int? id)
        {
            try
            {
                if (id == null) return BadRequest();

                Course course = await _context.Courses.Include(m=>m.Images).FirstOrDefaultAsync(m => m.Id == id);

                if (course is null) return NotFound();

                foreach (var item in course.Images)
                {
                    string path = FileHelper.GetFilePath(_env.WebRootPath, "images", item.Image);

                    FileHelper.DeleteFile(path);
                }
                

                _context.Courses.Remove(course);

                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));

            }
            catch (Exception)
            {

                throw;
            }
        }

        private async Task<SelectList> GetCategoriesAsync()
        {
            IEnumerable<CourseHost> coursesHost = await _context.CourseHosts.ToListAsync();
            return new SelectList(coursesHost, "Id", "HostName");
        }


    }
}
