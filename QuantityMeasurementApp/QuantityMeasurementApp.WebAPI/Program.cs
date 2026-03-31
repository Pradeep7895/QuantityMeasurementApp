using Microsoft.EntityFrameworkCore;
using QuantityMeasurementApp.Repository;
using QuantityMeasurementApp.Repository.Interfaces;
using QuantityMeasurementApp.Service.Interfaces;
using QuantityMeasurementApp.Service.Services;
using StackExchange.Redis;
using NLog.Web;

var builder = WebApplication.CreateBuilder(args);

// Logging
builder.Logging.ClearProviders();
builder.Logging.SetMinimumLevel(LogLevel.Trace);
builder.Host.UseNLog();

// DB
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Redis
builder.Services.AddSingleton<IConnectionMultiplexer>(sp =>
{
    var redisConnection = builder.Configuration.GetConnectionString("Redis");
    return ConnectionMultiplexer.Connect(redisConnection);
});

// Services
builder.Services.AddScoped<IQuantityService, QuantityService>();
builder.Services.AddScoped<IConversionService, ConversionService>();
builder.Services.AddScoped<IArithmeticService, ArithmeticService>();
builder.Services.AddScoped<IEqualityService, EqualityService>();
builder.Services.AddScoped<IValidationService, ValidationService>();
builder.Services.AddScoped<RedisCacheService>();

// Repository
builder.Services.AddScoped<IQuantityHistoryRepository, QuantityHistoryRepository>();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Middleware
app.UseMiddleware<GlobalExceptionMiddleware>();

app.UseSwagger();
app.UseSwaggerUI();

app.MapControllers();
app.MapGet("/", () => "Quantity Measurement API is running...");

app.Run();