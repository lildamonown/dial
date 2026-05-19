using Kursa4.UI.Models.Outputs;
using System.ComponentModel.DataAnnotations;

namespace Kursa4.UI.Models.Inputs
{
    public class OrderForEdit
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Пользователь обязателен")]
        public string UserId { get; set; } = string.Empty;

        public int StatusId { get; set; }

        [Display(Name = "Когда хотят приехать")]
        public DateTime? VisitDate { get; set; }

        public DateTime CreateAt { get; set; }

        public int CarId { get; set; }

        [MinLength(1, ErrorMessage = "Выберите хотя бы одну подуслугу")]
        public List<SubserviceModel> Subservices { get; set; } = new();
    }
}
