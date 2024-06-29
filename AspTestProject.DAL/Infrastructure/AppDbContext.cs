using Microsoft.EntityFrameworkCore;

namespace AspTestProject.DAL.Infrastructure
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }
    }
}
