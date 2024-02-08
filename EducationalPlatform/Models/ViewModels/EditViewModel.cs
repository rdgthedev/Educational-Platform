using System.ComponentModel.DataAnnotations;

namespace EducationalPlatform.Models.ViewModels
{
    public class EditViewModel
    {
        public Guid? Id { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }
    }
}
