using Microsoft.EntityFrameworkCore;
namespace dotnet_bakery.Models
{
    public class ApplicationContext : DbContext
    {
        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options) {}
        public DbSet<BreadInventory> BreadInventory { get; set; }
        public DbSet<Baker> Bakers { get; set; }
    }
}