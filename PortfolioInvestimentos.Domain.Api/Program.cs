using Microsoft.EntityFrameworkCore;
using PortfolioInvestimentos.Domain.Api.Middlewares;
using PortfolioInvestimentos.Domain.Api.Configurations;
using PortfolioInvestimentos.Domain.IoC;
using FluentValidation.AspNetCore;
using SharpGrip.FluentValidation.AutoValidation.Mvc.Extensions;
using PortfolioInvestimentos.Application.Validators;
using PortfolioInvestimentos.Application;

var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;

builder
    .Services
    .AddFluentValidationAutoValidation(opt =>
    {
        opt.OverrideDefaultResultFactoryWith<CustomResultFactory>();
    });

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddAuthorization();
builder.Services.AddControllers();

builder.Services.AddStackExchangeRedisCache(opt =>
    opt.Configuration = configuration.GetConnectionString("Cache"));

builder.Services.Configure<EmailSettings>(builder.Configuration.GetSection("EmailSettings"));

var emailSettings = configuration.GetSection("EmailSettings").Get<EmailSettings>();

builder.Services.AddSingleton(x => emailSettings);

builder.Services.AddSwaggerConfiguration();
builder.Services.AddCookiesConfiguration();
builder.Services.AddDatabaseConfiguration(configuration);
builder.Services.AddQuartzConfiguration();


builder.Services.AddRepositoriesCollection();
builder.Services.AddServicesCollection();
builder.Services.AddValidatorsCollection();
builder.Services.AddHandlersCollection();

var app = builder.Build();

app.UseMiddleware<ExceptionHandlingMiddleware>();

app.MigrationInitialisation();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();
