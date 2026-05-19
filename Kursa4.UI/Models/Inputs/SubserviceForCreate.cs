using System.ComponentModel.DataAnnotations;

namespace Kursa4.UI.Models.Inputs
{
    public class SubserviceForCreate
    {
        [Required(ErrorMessage = "Название обязательно")]
        [StringLength(100, MinimumLength = 3, ErrorMessage = "Название должно быть от 3 до 100 символов")]
        [Display(Name = "Название подуслуги")]
        public string Name { get; set; } = string.Empty;

        [Required(ErrorMessage = "Описание обязательно")]
        [StringLength(500, ErrorMessage = "Описание не должно превышать 500 символов")]
        [Display(Name = "Описание")]
        [DataType(DataType.MultilineText)]
        public string Description { get; set; } = string.Empty;

        [Required(ErrorMessage = "Не указана родительская услуга")]
        [Display(Name = "ID родительской услуги")]
        [Range(1, int.MaxValue, ErrorMessage = "Выберите услугу")]
        public int ServiceId { get; set; }

        public TimeSpan LeadTime { get; set; }

        [Display(Name = "Дни")]
        [Range(0, 365, ErrorMessage = "Дни должны быть между 0 и 365")]
        public int LeadTimeDays
        {
            get => LeadTime.Days;
            set => LeadTime = new TimeSpan(value, LeadTime.Hours, LeadTime.Minutes, 0);
        }

        [Display(Name = "Часы")]
        [Range(0, 23, ErrorMessage = "Часы должны быть между 0 и 23")]
        public int LeadTimeHours
        {
            get => LeadTime.Hours;
            set => LeadTime = new TimeSpan(LeadTime.Days, value, LeadTime.Minutes, 0);
        }

        [Display(Name = "Минуты")]
        [Range(0, 59, ErrorMessage = "Минуты должны быть между 0 и 59")]
        public int LeadTimeMinutes
        {
            get => LeadTime.Minutes;
            set => LeadTime = new TimeSpan(LeadTime.Days, LeadTime.Hours, value, 0);
        }

        [Display(Name = "Фиксированная цена")]
        public bool FixPrice { get; set; }

        [Required(ErrorMessage = "Укажите цену")]
        [Range(0.01, 10000, ErrorMessage = "Цена должна быть между 0.01 и 10 000")]
        [Display(Name = "Цена (BYN)")]
        [DataType(DataType.Currency)]
        public double Price { get; set; }

        [Display(Name = "Видимость для клиентов")]
        public bool Visible { get; set; }
    }
}
