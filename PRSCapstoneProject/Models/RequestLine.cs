using System.Text.Json.Serialization;

namespace PRSCapstoneProject.Models
{
    public class RequestLine
    {
        public int Id { get; set; }
        public int RequestId { get; set; }
        public int ProductId { get; set; }
        public int Quantity { get; set; } = 1;
        public virtual Product? Product { get; set; }
        [JsonIgnore]
        public virtual Request? Request { get; set; }
    }
}
