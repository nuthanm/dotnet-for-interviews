// This is for only web based application
//var builder = WebApplication.CreateBuilder(args);

using EFCore_CodeFirstApproach;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

// This is for console based application
var hostBuilder = Host.CreateDefaultBuilder(args)
   .ConfigureAppConfiguration((hostContext, config) =>
   {
       // This ensures appsettings.json is loaded for console apps
       config.SetBasePath(Directory.GetCurrentDirectory());
       config.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);
       config.AddEnvironmentVariables();
   })
   .ConfigureServices((hostContext, services) =>
   {
       // Get connection string from configuration
       var connectionString = hostContext.Configuration.GetConnectionString("DatabaseConnection");

       // Use your custom extension method from the class library
       services.AddBookDBContext(connectionString);

       // Add any other services your console app needs
       services.AddTransient<MyConsoleAppRunner>(); // Example: A service that runs your main logic
   })
   .ConfigureLogging(logging =>
   {
       logging.ClearProviders(); // Clear default providers
       logging.AddConsole(); // Add console logger

       //// You can set log levels here, similar to appsettings.json
       //logging.SetMinimumLevel(LogLevel.Information);
       //// Specifically for EF Core SQL queries
       //logging.AddFilter("Microsoft.EntityFrameworkCore.Database.Command", LogLevel.Information);
   })
   .Build();


// Run the application
// Build the host
var host = hostBuilder;

// Resolve and run your main logic
using (var scope = host.Services.CreateScope())
{
    var runner = scope.ServiceProvider.GetRequiredService<MyConsoleAppRunner>();
    await runner.RunAsync();
}


public class MyConsoleAppRunner
{
    private readonly BookDBContext _dbContext;
    private readonly ILogger<MyConsoleAppRunner> _logger;

    public MyConsoleAppRunner(BookDBContext dbContext, ILogger<MyConsoleAppRunner> logger)
    {
        _dbContext = dbContext;
        _logger = logger;
    }

    public async Task RunAsync()
    {
        _logger.LogInformation("Console App started.");

        try
        {
            // Apply migrations (optional, good for dev environment)
            _logger.LogInformation("Applying pending migrations...");
            await _dbContext.Database.MigrateAsync();
            _logger.LogInformation("Migrations applied.");

            _logger.LogInformation("Fetching books...");
            _logger.LogInformation($"EF Core to SQL Query: {_dbContext.Books.ToQueryString()}");
            var books = await _dbContext.Books.ToListAsync();
            foreach (var book in books)
            {
                _logger.LogInformation($"Book: {book.Title} by {book.Author}");
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred during execution.");
        }

        _logger.LogInformation("Console App finished.");
    }
}
