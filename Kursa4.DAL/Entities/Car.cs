
namespace Kursa4.DAL.Entities
{
    public class Car
    {
        public int Id { get; set; }

        public string Mark { get; set; } = string.Empty;

        public int Year { get; set; }

        public string TypeEngine { get; set; } = string.Empty;

        public string Drive { get; set; } = string.Empty;

        public string TypeBody { get; set; } = string.Empty;

        public string NumCar { get; set; } = string.Empty;
    }
}