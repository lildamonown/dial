using Kursa4.BLL.DTO;
using Kursa4.UI.Models.Outputs;
using System.ComponentModel.DataAnnotations;

namespace Kursa4.UI.Models.Inputs
{
    public class OrderForCreate
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Пользователь обязателен")]
        public string UserId { get; set; } = string.Empty;

        public int StatusId { get; set; }

        [Required(ErrorMessage = "Выберите дату визита")]
        [Display(Name = "Когда хотите приехать")]
        public DateTime? VisitDate { get; set; }

        public DateTime CreateAt { get; set; }

        public CarForCreate Car { get; set; } = new CarForCreate();

        [MinLength(1, ErrorMessage = "Выберите хотя бы одну подуслугу")]
        public List<SubserviceModel> Subservices { get; set; } = new();
    }
}
