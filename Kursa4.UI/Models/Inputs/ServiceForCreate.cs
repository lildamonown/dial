using System.ComponentModel.DataAnnotations;

namespace Kursa4.UI.Models.Inputs
{
    public class ServiceForCreate
    {
        [Required(ErrorMessage = "Название услуги обязательно")]
        [StringLength(100, MinimumLength = 3,
            ErrorMessage = "Название должно быть от 3 до 100 символов")]
        [Display(Name = "Название услуги")]
        public string Name { get; set; } = string.Empty;

        [Display(Name = "Видимость для клиентов")]
        public bool Visible { get; set; }
    }
}
