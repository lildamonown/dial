namespace Kursa4.UI.Models.Outputs
{
    public class ServiceModel
    {
        public int Id { get; set; }

        public string Name { get; set; } = string.Empty;

        public bool Visible { get; set; }

        public List<SubserviceModel> Subservices { get; set; } = [];
    }
}
