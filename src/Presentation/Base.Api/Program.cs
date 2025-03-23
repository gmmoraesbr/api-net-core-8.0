using Microsoft.EntityFrameworkCore;
using MediatR;
using Base.Infrastructure.Data;
using Serilog;
using Base.Application.Configuration;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Base.Domain.Contracts.Repositories;
using Base.Infrastructure.Repositories;
using Base.Api.Middlewares;
using Base.Application.Behaviors;
using Base.Api.Filters;
using Base.Application.Interfaces;
using FluentValidation;
using Base.Application.Features.Orders.Validators;
using Base.Infrastructure.Services;
using Microsoft.AspNetCore.Identity;
using Base.Application.Common;
using Microsoft.AspNetCore.Mvc.Filters;
using Base.Domain.Entities;
using Base.Application.Features.Auth.Validators;
using System.Runtime;
using Base.Api.Extensions;
using Base.Application.Mappings;
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



//builder.Services.AddScoped<IActionFilter>(provider => new RequireHeaderFilter("d9a5a5f2-6a93-4a71-9db9-f74c78e32ec5"));

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
