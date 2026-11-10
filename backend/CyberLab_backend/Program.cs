using Infrastructure;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

builder.Services.AddInfrastructure(builder.Configuration);

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    using var scope = app.Services.CreateScope();

    var dbContext = scope.ServiceProvider
        .GetRequiredService<CyberLabDbContext>();

    await dbContext.Database.MigrateAsync();
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.MapGet("/api/health", () =>
    Results.Ok(new
    {
        Status = "Healthy",
        Application = "CyberLab Backend"
    }));

app.MapGet("/api/database/health",
    async (CyberLabDbContext dbContext) =>
    {
        bool connected = await dbContext.Database.CanConnectAsync();

        return connected
            ? Results.Ok(new
            {
                Status = "Connected",
                Database = "PostgreSQL"
            })
            : Results.Problem("Database connection failed");
    });

app.Run();