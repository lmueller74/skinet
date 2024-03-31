using Microsoft.EntityFrameworkCore;
using Infrastructure.Data;
using Core.Interfaces;

internal class Program
{
    private static async Task Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.
        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        builder.Services.AddControllers();
        builder.Services.AddEndpointsApiExplorer();

        builder.Services.AddDbContext<StoreContext>(opt => {
            opt.UseSqlite(builder.Configuration.GetConnectionString("defaultConnection"));
        });
        
        builder.Services.AddScoped<IProductRepository, ProductRepository>();

        builder.Services.AddSwaggerGen();

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseAuthorization();
        app.MapControllers();

        // var summaries = new[]
        // {
        //     "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        // };

        // app.MapGet("/weatherforecast", () =>
        // {
        //     var forecast =  Enumerable.Range(1, 5).Select(index =>
        //         new WeatherForecast
        //         (
        //             DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
        //             Random.Shared.Next(-20, 55),
        //             summaries[Random.Shared.Next(summaries.Length)]
        //         ))
        //         .ToArray();
        //     return forecast;
        // })
        // .WithName("GetWeatherForecast")
        // .WithOpenApi();

        using var scope = app.Services.CreateScope();
        
        var services = scope.ServiceProvider;
        var context = services.GetRequiredService<StoreContext>();
        var logger  = services.GetRequiredService<ILogger<Program>>();

        try
        {
            await context.Database.MigrateAsync();
            await StoreContextSeed.SeedAsync(context);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "error during migration");
            throw;
        }

        app.Run();

    }
}

// record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary)
// {
//     public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
// }
