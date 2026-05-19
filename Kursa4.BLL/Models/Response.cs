namespace Kursa4.BLL.Models
{
    public class Response<T>
    {
        public T Value { get; set; }

        public StatusCode Status { get; set; }

        public string Description { get; set; } = string.Empty;

    }
}
