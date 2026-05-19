namespace Kursa4.DAL.Entities
{
    public class CarSeries
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public int CarBrandId { get; set; }
        public CarBrand CarBrand { get; set; }
    }
}
