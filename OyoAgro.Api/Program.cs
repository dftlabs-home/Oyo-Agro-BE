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

app.Run();


