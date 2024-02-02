using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.ComponentModel.DataAnnotations.Schema;

namespace EducationalPlatform.Models
{
    public class CourseModel
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime? LastUpdateDate { get; set; }

        public CourseModel(string title, string description, DateTime? lastUpdateDate = null)
        {
            Title = title;
            Description = description;  
            CreateDate = DateTime.UtcNow;
            LastUpdateDate = lastUpdateDate;
        }


        public List<UserModel> Users { get; set; }
    }
}
