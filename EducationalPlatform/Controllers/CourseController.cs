using EducationalPlatform.Data;
using EducationalPlatform.Exceptions;
using EducationalPlatform.Models;
using EducationalPlatform.Models.ViewModels;
using EducationalPlatform.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using X.PagedList;

namespace EducationalPlatform.Controllers
{
    public class CourseController(DataContext dataContext, IOrderService orderService) : Controller
    {
        private readonly DataContext _dataContext = dataContext;
        private readonly IOrderService _orderService = orderService;

        [HttpGet]
        public async Task<IActionResult> Index([FromQuery] int? pageNumber, 
            [FromQuery] string searchString, 
            [FromQuery] string sortOrder)
        {
            ViewData["CurrentSort"] = sortOrder;
            ViewData["TitleSortParm"] = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewData["DateSortParm"] = sortOrder == "Date" ? "date_desc" : "Date";

            try
            {
                IQueryable<CourseModel> coursesQuery = _dataContext.Course.AsNoTracking();

                if (!string.IsNullOrEmpty(searchString))
                    coursesQuery = coursesQuery.Where(x => x.Title.Contains(searchString));

                coursesQuery = _orderService.Order(coursesQuery, sortOrder);

                var courses = await coursesQuery.ToPagedListAsync(pageNumber ?? 1, 10);
                return View(courses);
            }
            catch (Exception)
            {
                await Task.Delay(5000);
                TempData["ErrorMessage"] = "Occoreu um erro interno!";
                return RedirectToAction("Index", "Home");
            }
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
