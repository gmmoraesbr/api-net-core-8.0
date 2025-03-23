using Serilog;
using Base.Api.Middlewares;
using FluentValidation;
using Base.Application.Common;
using Base.Api.Extensions;
using Base.Api.Configuration;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddConfiguredDatabase(builder.Configuration, builder.Environment);

builder.Services.AddHttpContextAccessor(); // Necessário para acessar o contexto do usuário

builder.Services.AddApplicationRepositories().AddApplicationServices();

builder.Services.AddControllers();

builder.Services.AddApiVersioning();

builder.Host.UseSerilog((context, config) =>
    config.WriteTo.Console().ReadFrom.Configuration(context.Configuration));

builder.Services.AddJwtAuthentication(builder.Configuration).AddJwtAuthorization();

builder.Services.AddMediatR(cfg =>
    cfg.RegisterServicesFromAssemblies(AppDomain.CurrentDomain.GetAssemblies()));

// Registra os Behaviors no MediatR
builder.Services.AddMediatRPipelineBehaviors();

builder.Services.AddScoped<DomainEventDispatcher>();

builder.Services.AddCustomControllers();

builder.Services.AddSingleton(
    builder.Configuration.GetSection("Correlation").Get<CorrelationSettings>()
    ?? new CorrelationSettings()
);

foreach (var assembly in AppDomain.CurrentDomain.GetAssemblies())
    builder.Services.AddValidatorsFromAssembly(assembly);

builder.Services.AddMappingProfiles();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseRouting();             // essencial
app.UseAuthentication();      // JWT
app.UseAuthorization();       // Authorization

app.UseCustomMiddlewares();

app.MapControllers();         // essencial para encontrar controllers

app.Run();

public partial class Program { } // Necessário para WebApplicationFactory
