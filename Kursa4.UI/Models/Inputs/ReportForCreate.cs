using System.ComponentModel.DataAnnotations;

namespace Kursa4.UI.Models.Inputs
{
    public class ReportForCreate
    {
        public int OrderId { get; set; }

        [Display(Name = "Финальная цена")]
        [Required(ErrorMessage = "Финальная цена обязательна")]
        [Range(0.01, double.MaxValue, ErrorMessage = "Цена должна быть больше 0")]
        public double FinitePrice { get; set; }

        public DateTime DateCompleted { get; set; }

        [Display(Name = "Имя мастера")]
        [Required(ErrorMessage = "Имя мастера обязательно")]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "Имя должно быть от 2 до 50 символов")]
        public string NameMaster { get; set; } = string.Empty;

        [Display(Name = "Фамилия мастера")]
        [Required(ErrorMessage = "Фамилия мастера обязательна")]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "Фамилия должна быть от 2 до 50 символов")]
        public string SurnameMaster { get; set; } = string.Empty;
    }
}
