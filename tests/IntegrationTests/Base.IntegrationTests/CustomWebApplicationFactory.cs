using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Logging;
using Base.Infrastructure.Data;
using Microsoft.AspNetCore.Identity;
using Base.Domain.Entities;

namespace Base.IntegrationTests;

public class CustomWebApplicationFactory<TStartup> : WebApplicationFactory<TStartup> where TStartup : class
{
    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.ConfigureLogging(logging => logging.ClearProviders());

        builder.UseEnvironment("IntegrationTests"); // Define o nome do ambiente

        builder.ConfigureServices(services =>
        {
            // Remove qualquer contexto anterior
            services.RemoveAll(typeof(DbContextOptions<ApplicationDbContext>));

            // Adiciona contexto em memória
            services.AddDbContext<ApplicationDbContext>(options =>
            {
                options.UseInMemoryDatabase("TestDb");
            });

            // Registra Identity para permitir resolver UserManager
            services
                .AddIdentityCore<User>()
                .AddRoles<Role>()
                .AddEntityFrameworkStores<ApplicationDbContext>();

            // Garante que o banco está criado
            var sp = services.BuildServiceProvider();
            using var scope = sp.CreateScope();
            var db = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
            var userManager = scope.ServiceProvider.GetRequiredService<UserManager<User>>();
            var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<Role>>();

            db.Database.EnsureCreated();

            // Cria a role Admin se não existir
            if (!roleManager.RoleExistsAsync("Admin").Result)
            {
                roleManager.CreateAsync(new Role("Admin")).Wait();
            }

            var adminUser = User.Create("Moraes", "admin@admin.com", "admin@admin.com");
            var result = userManager.CreateAsync(adminUser, "SenhaForte123!").Result;

            if (result.Succeeded)
                userManager.AddToRoleAsync(adminUser, "Admin").Wait();
        });
    }
}