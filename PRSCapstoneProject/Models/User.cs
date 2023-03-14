using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace PRSCapstoneProject.Models
{
    [Index("Username", IsUnique = true)]
    public class User
    {
        public int Id { get; set; }
        [StringLength(30)]
        public string Username { get; set; } = string.Empty;
        [StringLength(30)]
        public string Password { get; set; } = string.Empty;
        [StringLength(30)] 
        public string Firstname { get; set; } = string.Empty;
        [StringLength(30)] 
        public string Lastname { get; set; } = string.Empty;
        [StringLength(12)]
        public string? Phone { get; set; }
        [StringLength(255)]
        public string? Email { get; set; }
        public bool IsReviewer { get; set; } = false;
        public bool IsAdmin { get; set; } = false;
    }
}
