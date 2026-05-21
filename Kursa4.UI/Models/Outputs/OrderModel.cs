using Kursa4.BLL.DTO;
using System.ComponentModel.DataAnnotations;

namespace Kursa4.UI.Models.Outputs
{
    public class OrderModel
    {
        public int Id { get; set; }

        [Display(Name = "Имя")]
        public string UserName { get; set; } = string.Empty;

        [Display(Name = "Фамилия")]
        public string UserSurname { get; set; } = string.Empty;

        [Display(Name = "Номер телефона")]
        public string UserPhoneNumber { get; set; } = string.Empty;

        [Display(Name = "Статус")]
        public string StatusName { get; set; } = string.Empty;
        
        [Display(Name = "Хочет приехать")]
        public DateTime? VisitDate { get; set; }

        [Display(Name = "Дата создания заказа")]
        public DateTime CreateAt { get; set; }

        [Display(Name = "Посмотреть авто")]
        public int CarId { get; set; }

        [Display(Name = "Выбранные подуслуги")]
        public List<string> NameSubservice { get; set; } = [];

        [Display(Name = "Предварительная цена")]
        public double SumPrice { get; set; }

        [Display(Name = "Финальная цена")]
        public double? FinitePrice { get; set; }

        [Display(Name = "Комментарий мастера")]
        public string? ReportComment { get; set; }

        [Display(Name = "Мастер")]
        public string? MasterName { get; set; }
    }
}
