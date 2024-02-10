using EducationalPlatform.Models;
using System.Reflection.Metadata.Ecma335;

namespace EducationalPlatform.Services
{
    public class OrderService : IOrderService
    {
        public IQueryable<CourseModel> Order(IQueryable<CourseModel> courses, string sortOrder)
        {
            switch (sortOrder)
            {
                case "name_desc":
                    courses = courses.OrderByDescending(x => x.Title);
                    break;
                case "Date":
                    courses = courses.OrderByDescending(x => x.CreateDate);
                    break;
                default:
                    courses = courses.OrderBy(x => x.Title);
                    break;
            }

            return courses;
        }
    }
}
