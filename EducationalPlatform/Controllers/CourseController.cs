using EducationalPlatform.Data;
using EducationalPlatform.Exceptions;
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
            try
            {
                if (!ModelState.IsValid)
                    return View(model);

                var resultCourse = await _dataContext.Course.AsNoTracking().FirstOrDefaultAsync(x => x.Title == model.Title);

                if (resultCourse != null)
                    throw new CourseExistsException("Este curso já existe!");

                var course = new CourseModel(model.Title, model.Description);

                await _dataContext.AddAsync(course);
                await _dataContext.SaveChangesAsync();

                return RedirectToAction("Index");
            }
            catch (CourseExistsException ex)
            {
                TempData["ErrorMessage"] = ex.Message;
                return View("Register");
            }
            catch (Exception)
            {
                TempData["ErrorMessage"] = "Ocorreu um erro interno!";
                return View(model);
            }
        }

        [HttpGet]
        public async Task<IActionResult> Edit([FromRoute] Guid? id)
        {
            try
            {
                if (id == null)
                    throw new CourseNotFoundException("Este curso não existe!");

                var course = await _dataContext.Course.FirstOrDefaultAsync(x => x.Id == id);

                if (course == null)
                    throw new CourseNotFoundException("Este curso não existe!");

                return View(course);
            }
            catch (CourseNotFoundException ex) 
            {
                TempData["ErrorMessage"] = ex.Message;
                return View();
            }
            catch (Exception)
            {
                TempData["ErrorMessage"] = "Ocorreu um erro interno!";
                return View();
            }
        }

        [HttpPost]
        public async Task<IActionResult> Edit([FromForm] EditViewModel model)
        {
            if (!ModelState.IsValid)
                return View("Edit");

            try
            {
                var course = await _dataContext.Course.FindAsync(model.Id);

                if (course is null)
                    throw new CourseNotFoundException("Curso não encontrado!");

                if (model.Title != null)
                    course!.Title = model.Title;

                if (model.Description != null)
                    course!.Description = model.Description!;

                course!.LastUpdateDate = DateTime.Now;

                _dataContext.Course.Update(course);
                await _dataContext.SaveChangesAsync();

                return RedirectToAction("Index");
            }
            catch (CourseNotFoundException ex)
            {
                TempData["ErrorMessage"] = ex.Message;
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = ex.Message;
                return RedirectToAction("Index");
            }
        }

        [HttpGet]
        public async Task<IActionResult> Delete([FromRoute] Guid? id)
        {
            try
            {
                if (id == null)
                    throw new CourseNotFoundException("O curso não existe!");

                var course = await _dataContext.Course.FirstOrDefaultAsync(x => x.Id == id);

                if (course == null)
                    throw new CourseNotFoundException("O curso não existe!");

                return View(course);
            }
            catch (CourseNotFoundException ex)
            {
                TempData["ErrorMessage"] = ex.Message;
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = ex.Message;
                return RedirectToAction("Index");
            }
        }

        [HttpPost]
        public async Task<IActionResult> Delete([FromForm] Guid id)
        {
            try
            {
                var course = await _dataContext.Course.FirstOrDefaultAsync(x => x.Id == id);

                if (course is null)
                    throw new CourseNotFoundException("O curso não existe!");

                _dataContext.Course.Remove(course!);
                await _dataContext.SaveChangesAsync();

                return RedirectToAction("Index");
            }

            catch (CourseNotFoundException ex)
            {
                TempData["ErrorMessage"] = ex.Message;
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = ex.Message;
                return RedirectToAction("Index");
            }
        }
    }
}
