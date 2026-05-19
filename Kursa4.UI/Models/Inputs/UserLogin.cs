using System.ComponentModel.DataAnnotations;

namespace Kursa4.UI.Models.Inputs
{
    public class UserLogin
    {
        [Required]
        [Display(Name = "Email")]
        [EmailAddress]
        public string Email { get; set; } = string.Empty;

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        [StringLength(100, ErrorMessage =
            "Поле {0} должно содержать не менее {2} и не более {1} символов", MinimumLength = 5)]
        public string Password { get; set; } = string.Empty;
    }
}
