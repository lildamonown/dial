using System;

namespace Kursa4.BLL.DTO
{
    public class PriceHistoryDTO
    {
        public int Id { get; set; }

        public int SubserviceId { get; set; }

        public double OldPrice { get; set; }

        public double NewPrice { get; set; }

        public string Comment { get; set; } = string.Empty;

        public DateTime ChangedAt { get; set; }

        public string MasterName { get; set; } = string.Empty;

        public string MasterSurname { get; set; } = string.Empty;
    }
}
