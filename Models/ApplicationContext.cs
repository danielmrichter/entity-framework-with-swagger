using Microsoft.EntityFrameworkCore;
namespace DotnetBakery.Models
{
    public class ApplicationContext : DbContext
    {
      public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options) {}
          
      // Name of field ("Bakers") is what DB table will be called!
      public DbSet<Baker> Bakers {get; set;} 
    }
}