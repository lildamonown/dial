using System.ComponentModel.DataAnnotations;

namespace Kursa4.UI.Models.Inputs
{
    public class UserEdit
    {
        [Required(ErrorMessage = "ID пользователя обязателен")]
        public string Id { get; set; } = string.Empty;

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
    }
}
