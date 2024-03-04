using Microsoft.EntityFrameworkCore;
using PortfolioInvestimentos.Domain.Api.Middlewares;
using PortfolioInvestimentos.Domain.Api.Services;
using PortfolioInvestimentos.Domain.Infra.Context;
using PortfolioInvestimentos.Domain.IoC;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;

builder.Services
    .AddAuthentication(x =>
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
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes("599930f7ba74b85bba6c2b33cdabc2c090160e712ba1c346a290991fe5317d03")),
            ValidateIssuer = false,
            ValidateAudience = false
        };
    });

builder
    .Services
    .AddFluentValidationAutoValidation();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddAuthorization();
builder.Services.AddControllers();

var server = configuration["DbServer"] ?? "localhost";
var port = configuration["DbPort"] ?? "1433"; // Default SQL Server port
var user = configuration["DbUser"] ?? "SA"; // Warning do not use the SA account
var password = configuration["Password"] ?? "numsey#2021";
var database = configuration["Database"] ?? "Portfolio";
var connectionString = $"Server={server}, {port};Initial Catalog={database};User ID={user};Password={password};Persist Security Info=False;Encrypt=False";

builder
    .Services
    .AddDbContext<ApplicationDbContext>(
    b => b.UseSqlServer(connectionString
    ));


builder.Services.AddRepositoriesCollection();
builder.Services.AddServicesCollection();
builder.Services.AddValidatorsCollection();
builder.Services.AddHandlersCollection();

builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "App.API", Version = "v1" });
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
    {
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "JWT Authorization header using the Bearer scheme. \r\n\r\n Enter 'Bearer' [space] and then your token in the text input below.\r\n\r\nExample: \"Bearer 12345abcdef\"",
    });

    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            new string[] {}
        }
    });

    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference { Type = ReferenceType.SecurityScheme, Id = "oauth2" }
            },
            new[] { "readAccess", "writeAccess" }
        }
    });
});


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
