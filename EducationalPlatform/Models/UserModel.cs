using Microsoft.AspNetCore.Identity;

namespace EducationalPlatform.Models
{
    public class UserModel : IdentityUser
    {
        public DateTime BirthDate { get; set; }
        public List<CourseModel> Courses { get; set; }

        public UserModel(string userName, DateTime birthDate, string email, string phoneNumber)
        {
            UserName = userName;
            BirthDate = birthDate.Date;
            Email = email;
            PhoneNumber = phoneNumber;
        }
    }
}
