using RisedorApi.Api.Endpoints;
using RisedorApi.Application.Handlers;
using RisedorApi.Infrastructure.Persistence;
using RisedorApi.Infrastructure.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using FluentValidation.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Configure JWT Settings
builder.Services.Configure<JwtSettings>(builder.Configuration.GetSection("JwtSettings"));
builder.Services.AddScoped<JwtAuthManager>();

// Add JWT Authentication
var jwtSettings = builder.Configuration.GetSection("JwtSettings").Get<JwtSettings>();
builder.Services
    .AddAuthentication(options =>
    {
        options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    })
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = jwtSettings?.Issuer,
            ValidAudience = jwtSettings?.Audience,
            IssuerSigningKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(
                    jwtSettings?.SecretKey
                        ?? throw new InvalidOperationException("JWT SecretKey is not configured")
                )
            )
        };
    });

// Add Authorization
builder.Services.AddAuthorization(options =>
{
    // Add policies for each role
    options.AddPolicy("Vendor", policy => policy.RequireRole("Vendor"));
    options.AddPolicy("SalesRep", policy => policy.RequireRole("SalesRep"));
    options.AddPolicy("Supermarket", policy => policy.RequireRole("Supermarket"));
    options.AddPolicy("Staff", policy => policy.RequireRole("Staff"));
});

// Add DbContext
builder.Services.AddDbContext<ApplicationDbContext>(
    options => options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"))
);

// Add MediatR
builder.Services.AddMediatR(cfg =>
{
    cfg.RegisterServicesFromAssemblyContaining<GetWeatherForecastQueryHandler>();
});

// Add validation
builder.Services.AddValidatorsFromAssembly(typeof(CreateUserCommandValidator).Assembly);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// Add Authentication & Authorization middleware
app.UseAuthentication();
app.UseAuthorization();

// Add error handling middleware before other middleware
app.UseErrorHandling();

// Map endpoints
app.MapWeatherForecastEndpoints();
app.MapAuthEndpoints();
app.MapOrderEndpoints();
app.MapUserEndpoints();
app.MapProductEndpoints();
app.MapPromotionEndpoints();

app.Run();

record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary)
{
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}
