using System.ComponentModel.DataAnnotations;

namespace Kursa4.UI.Models.Inputs
{
    public class UserRegister
    {
        [Required(ErrorMessage = "Email должен быть введён")]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; } = string.Empty;

        [Required(ErrorMessage = "Введите имя")]
        [Display(Name = "Name")]
        public string FirstName { get; set; } = string.Empty;

        [Required(ErrorMessage = "Введите фамилию")]
        [Display(Name = "Surname")]
        public string Surname { get; set; } = string.Empty;

        [Required(ErrorMessage = "Введите номер телефона")]
        [Display(Name = "PhoneNumber")]
        [Phone(ErrorMessage = "Неправильный формат номера телефона")]
        public string PhoneNumber { get; set; } = string.Empty;

        [Required(ErrorMessage = "Введите пароль")]
        [DataType(DataType.Password)]
        [StringLength(100, ErrorMessage =
            "Поле {0} должно содержать не менее {2} и не более {1} символов.", MinimumLength = 5)]
        [Display(Name = "Password")]
        public string Password { get; set; } = string.Empty;

        [Required(ErrorMessage = "Повторите пароль")]
        [Compare("Password", ErrorMessage = "Пароли не совпадают")]
        [DataType(DataType.Password)]
        [Display(Name = "Confirm Password")]
        public string PasswordConfirm { get; set; } = string.Empty;
    }
}
