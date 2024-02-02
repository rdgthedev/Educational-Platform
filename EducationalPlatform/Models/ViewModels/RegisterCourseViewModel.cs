using System.ComponentModel.DataAnnotations;

namespace EducationalPlatform.Models.ViewModels
{
    public class RegisterCourseViewModel
    {
        [Required(ErrorMessage = "O título é obrigatório!")]
        public string Title { get; set; }

        [Required(ErrorMessage = "A descrição é obrigatória!")]
        public string Description { get; set; }
    }
}
