using Microsoft.EntityFrameworkCore;

namespace PRSCapstoneProject.Models
{
    public class PRSDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Vendor> Vendors { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Request> Requests { get; set; }
        public DbSet<RequestLine> RequestLines { get; set; }
        
        public PRSDbContext(DbContextOptions<PRSDbContext> options) : base(options) { }
    }
}
