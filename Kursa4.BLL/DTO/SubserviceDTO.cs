namespace Kursa4.BLL.DTO
{
    public class SubserviceDTO
    {
        public int Id { get; set; }

        public string Name { get; set; } = string.Empty;

        public string Description { get; set; } = string.Empty;

        public int ServiceId { get; set; }

        public TimeSpan LeadTime { get; set; }

        public bool FixPrice { get; set; }

        public double Price { get; set; }

        public bool Visible { get; set; }
    }
}
