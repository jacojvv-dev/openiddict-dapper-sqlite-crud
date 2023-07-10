using System.Reflection;
using DbUp;

namespace CRUD.Api.MigrationRunner;

public class Worker : BackgroundService
{
    private readonly IConfiguration _configuration;
    private readonly ILogger<Worker> _logger;

    public Worker(IConfiguration configuration, ILogger<Worker> logger)
    {
        _configuration = configuration;
        _logger = logger;
    }

    protected override Task ExecuteAsync(CancellationToken stoppingToken)
    {
        var connectionString = _configuration.GetConnectionString("DefaultConnection");

        _logger.LogInformation("Starting Migrations on {connection}", connectionString);

        var upgrader =
            DeployChanges
                .To
                .SQLiteDatabase(connectionString)
                .WithScriptsEmbeddedInAssembly(Assembly.GetExecutingAssembly())
                .LogToConsole()
                .Build();

        var result = upgrader.PerformUpgrade();
        if (!result.Successful)
            _logger.LogCritical("Failed to migrate database, {error}", result.Error.Message);
        else
            _logger.LogInformation("Migrations Completed");

        return Task.CompletedTask;
    }
}