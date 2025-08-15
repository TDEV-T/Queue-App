using Microsoft.EntityFrameworkCore;
using QueueService.Models;
using QueueService.Repository.Implementations;
using QueueService.Repository.Interfaces;
using QueueService.Services.Implementations;
using QueueService.Services.Interfaces;
using System.Text.Json;

var builder = WebApplication.CreateBuilder(args);

var environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Development";
builder.Configuration
    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
    .AddJsonFile($"appsettings.{environment}.json", optional: true);

builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.SnakeCaseLower;
    });

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<IQueueService, QueueService.Services.Implementations.QueueService>();
builder.Services.AddScoped<IQueueStateRepository, QueueStateRepository>();
builder.Services.AddScoped<ITicketRepository, TicketRepository>();

builder.Services.AddDbContext<QueueDbContext>(options =>
    options.UseNpgsql(
        builder.Configuration.GetConnectionString("QueueDbContext"),
        npgsqlOptions => npgsqlOptions.MigrationsAssembly("QueueService")
    )
);

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    try
    {
        var dbContext = services.GetRequiredService<QueueDbContext>();
        await dbContext.Database.MigrateAsync();
    }
    catch (Exception ex)
    {
        var logger = services.GetRequiredService<ILogger<Program>>();
        logger.LogError(ex, "An error occurred while migrating the database.");
    }
}

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors(corsBuilder => corsBuilder
    .WithOrigins("http://localhost:3000", "http://localhost:4200", "https://localhost:4200", "https://localhost:5001")
    .AllowAnyMethod()
    .AllowAnyHeader()
    .AllowCredentials());

app.UseHttpsRedirection();

app.MapControllers();

app.Run();
