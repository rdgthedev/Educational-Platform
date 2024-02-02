using EducationalPlatform.Data;
using EducationalPlatform.Models;
using EducationalPlatform.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EducationalPlatform.Controllers
{
    public class CourseController(DataContext dataContext) : Controller
    {
        private readonly DataContext _dataContext = dataContext;
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var courses = await _dataContext.Course.AsNoTracking().ToListAsync();
            return View(courses);
        }

        [HttpGet]
        [Authorize]
        public IActionResult Register() => View();

        [HttpPost]
        public async Task<IActionResult> CreateCourse([FromForm] RegisterCourseViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var resultCourse = await _dataContext.Course.AsNoTracking().FirstOrDefaultAsync(x => x.Title == model.Title);

            if (resultCourse != null)
                throw new Exception();

            var course = new CourseModel(model.Title, model.Description);
            await _dataContext.AddAsync(course);
            await _dataContext.SaveChangesAsync();

            return RedirectToAction("Index"); 
        }
    }
}
