namespace Kursa4.UI.Models.Outputs
{
    public class ReviewModel
    {
        public int Id { get; set; }

        public string UserName { get; set; } = string.Empty;

        public string Text { get; set; } = string.Empty;

        public int Grade { get; set; }

        public DateTime CreateAt { get; set; }
    }
}
