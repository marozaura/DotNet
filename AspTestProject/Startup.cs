using System.Reflection;
using AspTestProject.BLL;
using AspTestProject.BLL.Services.Implementations;
using AspTestProject.BLL.Services.Interfaces;
using AspTestProject.DAL;
using AspTestProject.DAL.Infrastructure;
using AspTestProject.DAL.Repositories.Implementations;
using AspTestProject.DAL.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace AspTestProject
{
    public class Startup
    {
        private IConfiguration _configuration;

        public Startup(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection service)
        {
            service.AddDbContext<AppDbContext>(options =>
                options.UseNpgsql(_configuration.GetConnectionString("DefaultConnection")));
            service.AddScoped<IUnitOfWork, UnitOfWork>();
            service.AddScoped<IOrganizationService, OrganizationService>();
            service.AddScoped<IOrganiztionRepository, OrganizationRepository>();
            service.AddSwaggerGen();
            service.AddControllers();

            var assemblies = new List<Assembly>
            {
                typeof(Program).Assembly,
                typeof(BllAccessReference).Assembly,
                typeof(DataAccessReference).Assembly
            };

            service.AddAutoMapper(assemblies);
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(options => 
                {
                    options.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
                    options.RoutePrefix = string.Empty;
                });
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
