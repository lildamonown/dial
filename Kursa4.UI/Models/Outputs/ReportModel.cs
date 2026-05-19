using System.ComponentModel.DataAnnotations;

namespace Kursa4.UI.Models.Outputs
{
    public class ReportModel
    {
        public int Id { get; set; }

        [Display(Name = "Id заказа")]
        public int OrderId { get; set; }

        [Display(Name = "Финальная цена")]
        public double FinitePrice { get; set; }

        [Display(Name = "Дата завершения")]
        public DateTime DateCompleted { get; set; }

        [Display(Name = "Имя мастера")]
        public string NameMaster { get; set; } = string.Empty;

        [Display(Name = "Фамилия мастера")]
        public string SurnameMaster { get; set; } = string.Empty;

        public List<string> OrderSubservices { get; set; } = [];
        public double OrderSumPrice { get; set; }
        public string CarMark { get; set; } = string.Empty;
        public string ClientName { get; set; } = string.Empty;
        public string ClientPhone { get; set; } = string.Empty;
    }
}
