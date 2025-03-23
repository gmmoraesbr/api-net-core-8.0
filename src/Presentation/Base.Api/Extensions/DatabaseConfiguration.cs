using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Base.Infrastructure.Data;

namespace Base.Api.Extensions
{
    public static class DatabaseConfiguration
    {
        public static IServiceCollection AddConfiguredDatabase(this IServiceCollection services, IConfiguration configuration, IWebHostEnvironment env)
        {
            if (env.EnvironmentName == "IntegrationTests")
            {
                // Ignora o banco em testes, ele será registrado via CustomWebApplicationFactory
                return services;
            }

            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseNpgsql(configuration.GetConnectionString("DefaultConnection")));

            return services;
        }
    }
}
