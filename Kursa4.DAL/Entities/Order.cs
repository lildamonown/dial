namespace Kursa4.DAL.Entities
{
    public class Order
    {
        public int Id { get; set; }

        public User User { get; set; }
        public string UserId { get; set; } = string.Empty;

        public Status Status { get; set; }
        public int StatusId { get; set; }

        public DateTime? VisitDate { get; set; }

        public DateTime CreateAt { get; set; }

        public Car Car { get; set; }
        public int CarId { get; set; }

        public List<Subservice> Subservices { get; set; } = [];
    }
}

