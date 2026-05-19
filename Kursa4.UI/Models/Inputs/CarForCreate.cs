using System.ComponentModel.DataAnnotations;

namespace Kursa4.UI.Models.Inputs
{
    public class CarForCreate
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Поле 'Марка' обязательно для заполнения")]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "Марка должна быть от 2 до 50 символов")]
        [Display(Name = "Марка")]
        public string Mark { get; set; } = string.Empty;

        [Required(ErrorMessage = "Поле 'Год' обязательно для заполнения")]
        [Range(1900, 2025, ErrorMessage = "Год должен быть между 1900 и 2025")]
        [Display(Name = "Год выпуска")]
        public int Year { get; set; }

        [Required(ErrorMessage = "Поле 'Тип двигателя' обязательно для заполнения")]
        [StringLength(30, ErrorMessage = "Тип двигателя не должен превышать 30 символов")]
        [Display(Name = "Тип двигателя")]
        public string TypeEngine { get; set; } = string.Empty;

        [Required(ErrorMessage = "Поле 'Привод' обязательно для заполнения")]
        [StringLength(20, ErrorMessage = "Привод не должен превышать 20 символов")]
        [Display(Name = "Привод")]
        public string Drive { get; set; } = string.Empty;

        [Required(ErrorMessage = "Поле 'Тип кузова' обязательно для заполнения")]
        [StringLength(30, ErrorMessage = "Тип кузова не должен превышать 30 символов")]
        [Display(Name = "Тип кузова")]
        public string TypeBody { get; set; } = string.Empty;

        [Required(ErrorMessage = "Поле 'Номер автомобиля' обязательно для заполнения")]
        [Display(Name = "Номер автомобиля")]
        public string NumCar { get; set; } = string.Empty;
    }
}
