using Microsoft.EntityFrameworkCore;
using OyoAgro.DataAccess.Layer.Interfaces;
using OyoAgro.DataAccess.Layer;
using OyoAgro.DataAccess.Layer.Models;
using OyoAgro.Api.Authorizations;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using OyoAgro.DataAccess.Layer.Settings;
using OyoAgro.DataAccess.Layer.Helpers;



var builder = WebApplication.CreateBuilder(args);

// Log connection string in Development mode only (for debugging)
if (builder.Environment.IsDevelopment())
{
    var connectionString = builder.Configuration["Database:DefaultConnection"];
    var dbProvider = builder.Configuration["Database:DBProvider"];
    Console.WriteLine("=== Development Mode ===");
    Console.WriteLine($"Database Provider: {dbProvider}");
    if (!string.IsNullOrEmpty(connectionString))
    {
        // Mask password in connection string for security
        var maskedConnection = connectionString;
        var passwordMatch = System.Text.RegularExpressions.Regex.Match(connectionString, @"Password=([^;]+)");
        if (passwordMatch.Success)
        {
            maskedConnection = connectionString.Replace(passwordMatch.Groups[1].Value, "***");
        }
        Console.WriteLine($"Connection String: {maskedConnection}");
    }
    else
    {
        Console.WriteLine("WARNING: Connection string is empty!");
    }
    Console.WriteLine("=======================");
}

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<AppDbContext>(options =>
{
    var dbProvider = builder.Configuration["Database:DBProvider"];
    var connection = builder.Configuration["Database:DefaultConnection"];

    switch (dbProvider?.ToLower())
    {
        case "mssql":
            options.UseSqlServer(connection);
            break;
        case "postgresql":
        case "npgsql":
            options.UseNpgsql(connection);
            break;
        case "mysql":
            options.UseMySql(connection, ServerVersion.AutoDetect(connection));
            break;
        
        default:
            throw new Exception($"Unsupported DBProvider: {dbProvider}");
    }
});


builder.Services.AddAuthentication("Bearer")
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = builder.Configuration["Jwt:Issuer"],
            ValidAudience = builder.Configuration["Jwt:Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"])
            )
        };
    });
builder.Services.AddApiVersioning(options =>
{
    options.AssumeDefaultVersionWhenUnspecified = true;
    options.DefaultApiVersion = new Microsoft.AspNetCore.Mvc.ApiVersion(1, 0);
    options.ReportApiVersions = true;
});

builder.Services.AddVersionedApiExplorer(options =>
{
    options.GroupNameFormat = "'v'VVV";
    options.SubstituteApiVersionInUrl = true;
});

builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

builder.Services.AddConfigureServices();

SystemConfig.Database = builder.Configuration.GetSection("Database").Get<Database>();
SystemConfig.JWTSecret = builder.Configuration.GetSection("Jwt").Get<Jwt>();


var app = builder.Build();
AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);


app.UseSwagger();
app.UseSwaggerUI();


app.UseHttpsRedirection();
app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

// Configure port for Render deployment (only when PORT env variable is set)
var port = Environment.GetEnvironmentVariable("PORT");
if (!string.IsNullOrEmpty(port))
{
    // Production/Render deployment - use PORT environment variable
    app.Urls.Clear();
    app.Urls.Add($"http://0.0.0.0:{port}");
}
// Otherwise, use launchSettings.json URLs for local development

app.Run();


