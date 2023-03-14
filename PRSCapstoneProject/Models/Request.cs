using Microsoft.AspNetCore.Server.Kestrel.Core.Internal.Http;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace PRSCapstoneProject.Models
{
    public class Request
    {
        public int Id { get; set; }
        [StringLength(80)]
        public string Description { get; set; }
        [StringLength(80)]
        public string Justification { get; set; }
        [StringLength(80)]
        public string? RejectionReason { get; set; }
        [StringLength(20)]
        public string DeliveryMode { get; set; } = "Pickup";
        [StringLength(10)]
        public string Status { get; set; } = "NEW";
        [Column(TypeName = "decimal(11,2)")]
        public decimal Total { get; set; } = decimal.Zero;
        public int UserId { get; set; }
        public virtual User? User { get; set; }
        public virtual ICollection<RequestLine>? RequestLines { get; set; }

    }
}
