using AspTestProject.DAL.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace AspTestProject.DAL
{
    public class AppContextFactory : IDesignTimeDbContextFactory<AppDbContext>
    {
        private const string ConnectionStringName = "DefaultConnection";
        private const string _apiProjectDirectoryName = "AspTestProject";
        public AppDbContext CreateDbContext(string[] args)
        {
            var configuration = new ConfigurationBuilder()
                .SetBasePath(GetBasePath())
                .AddJsonFile("appsettings.json")
                .Build();

            var builder = new DbContextOptionsBuilder<AppDbContext>();
            var connectionString = configuration.GetConnectionString(ConnectionStringName);

            builder.UseNpgsql(connectionString);

            return new AppDbContext(builder.Options);
        }

        private string GetBasePath()
        {
            var currentDirectoryPath = Directory.GetCurrentDirectory();
            var parentDirectoryPath = Directory.GetParent(currentDirectoryPath).FullName;
            var basePath = Path.Combine(parentDirectoryPath, _apiProjectDirectoryName);

            return basePath;
        }
    }
}
