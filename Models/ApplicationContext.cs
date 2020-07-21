using Microsoft.EntityFrameworkCore;
namespace dotnet_bakery.Models
{
    public class ApplicationContext : DbContext
    {
        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options) {}
    }
}