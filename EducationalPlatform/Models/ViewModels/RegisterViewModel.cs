using System.ComponentModel.DataAnnotations;

namespace EducationalPlatform.Models.ViewModels
{
    public class RegisterViewModel
    {
        [Required(ErrorMessage = "O Nome é obrigatório!")]
        public string FullName { get; set; }
        [Required(ErrorMessage = "A Data de Nascimento é obrigatória!")]
        public DateTime BirthDate { get; set; }

        [Required(ErrorMessage = "O Email é obrigatório!")]
        [EmailAddress(ErrorMessage = "Digite um Email válido!")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Digite o telefone!")]
        [Phone(ErrorMessage = "Digite um telefone válido!")]
        public string Phone { get; set; }

        [Required(ErrorMessage = "A Senha é obrigatória!")]
        public string Password { get; set; }

        [Required(ErrorMessage = "A confirmação da senha é obrigatória!")]
        [Compare(nameof(Password), ErrorMessage = "A confirmação da senha não confere com o campo Senha.")]
        public string ConfirmPassword { get; set; }
    }
}
