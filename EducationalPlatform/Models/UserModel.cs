using EducationalPlatform.Models.Enums;
using Microsoft.AspNetCore.Identity;
using System.Net;

namespace EducationalPlatform.Models
{
    public class UserModel : IdentityUser
    {
        public DateTime BirthDate { get; set; }
        public int CourseId { get; set; }
        public List<CourseModel> Courses { get; set; }
    }
}
