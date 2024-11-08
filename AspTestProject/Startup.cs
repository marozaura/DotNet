using System.Reflection;
using System.Text;
using AspTestProject.BLL;
using AspTestProject.BLL.Services.Implementations;
using AspTestProject.BLL.Services.Interfaces;
using AspTestProject.Common.Settings;
using AspTestProject.Consumers;
using AspTestProject.DAL;
using AspTestProject.DAL.Infrastructure;
using AspTestProject.DAL.Repositories.Implementations;
using AspTestProject.DAL.Repositories.Interfaces;
using MassTransit;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;

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
            service.AddMassTransit(x =>
            {
                x.AddConsumer<OrderCreatedConsumer>(); 

                x.UsingRabbitMq((context, cfg) =>
                {
                    cfg.ConfigureEndpoints(context); 
                });
            });


            service.AddDbContext<AppDbContext>(options =>
                options.UseNpgsql(_configuration.GetConnectionString("DefaultConnection")));
            
            RegistrationRepositories(service);
            RegistrationServices(service);
            RegistrationMapperProfile(service);
            var appSettings = service.AddSettingsConfiguration(_configuration);
            service.AddJwtAuthenticationConfiguration(appSettings.JwtSettings);
            service.AddSwaggerConfiguration();
            service.AddControllers();
            
        }

        private static void RegistrationServices(IServiceCollection service)
        {
            service.AddScoped<IUnitOfWork, UnitOfWork>();
            service.AddScoped<IOrganizationService, OrganizationService>();
            service.AddScoped<IPasswordValidationService, PasswordValidationService>();
            service.AddScoped<IUserService, UserService>();
            service.AddScoped<IAuthService, AuthService>();
            service.AddScoped<ITokenGeneratorService, TokenGeneratorService>();
        }

        private static void RegistrationRepositories(IServiceCollection service)
        {
            service.AddScoped<IOrganiztionRepository, OrganizationRepository>();
            service.AddScoped<IUserRepository, UserRepository>();
        }

        private static void RegistrationMapperProfile(IServiceCollection service)
        {
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
                var currentAppVersion = Assembly.GetEntryAssembly().GetName().Version.ToString();
                var swaggerEndpointWithCurrentVersion = string.Format("/swagger/{0}/swagger.json", currentAppVersion);

                app.UseSwagger();
                app.UseSwaggerUI(c =>
                {
                    c.SwaggerEndpoint(swaggerEndpointWithCurrentVersion, "AspTestProject");
                    c.RoutePrefix = string.Empty;
                });
            }


            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }

       
    }

    public static class SettingsStartupExtension
    {
        public static ApplicationSettings AddSettingsConfiguration(this IServiceCollection services,
            IConfiguration configuration)
        {
            var jwtSetting = configuration.GetSection("JwtSettings");
            services.Configure<JwtSettings>(jwtSetting);

            return new ApplicationSettings
            {
                JwtSettings = jwtSetting.Get<JwtSettings>()
            };
        }
    }

    public static class JwtAuthenticationStartupExtension
    {
        public static void AddJwtAuthenticationConfiguration(this IServiceCollection services,
            JwtSettings jwtSettings)
        {
            var key = Encoding.ASCII.GetBytes(jwtSettings.JwtSecret);

            services.AddAuthentication(x =>
                {
                    x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                })
                .AddJwtBearer(x =>
                {
                    x.RequireHttpsMetadata = false;
                    x.SaveToken = true;
                    x.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(key),
                        ValidateIssuer = false,
                        ValidateAudience = false
                    };
                });
        }
    }

    public static class SwaggerStartupExtension
    {
        private const string SecuritySchemeName = "Authorization";

        public static void AddSwaggerConfiguration(this IServiceCollection services)
        {
            services.AddSwaggerGen(options =>
            {
                var currentAppVersion = Assembly.GetEntryAssembly().GetName().Version.ToString();
                options.SwaggerDoc(currentAppVersion,
                    new OpenApiInfo
                    {
                        Title = "AspTestProject",
                        Version = currentAppVersion
                    });

                var securitySchema = new OpenApiSecurityScheme
                {
                    Name = SecuritySchemeName,
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey,
                    Reference = new OpenApiReference
                    {
                        Type = ReferenceType.SecurityScheme,
                        Id = "Bearer"
                    }
                };

                options.AddSecurityDefinition("Bearer", securitySchema);

                var securityRequirement = new OpenApiSecurityRequirement
                {
                    { securitySchema, new[] { "Bearer" } }
                };

                options.AddSecurityRequirement(securityRequirement);
            });
        }
    }
}
