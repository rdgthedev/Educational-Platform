using EducationalPlatform.Models;

namespace EducationalPlatform.Services
{
    public interface IOrderService
    {
        public IQueryable<CourseModel> Order(IQueryable<CourseModel> courses, string sortOrder);
    }
}
