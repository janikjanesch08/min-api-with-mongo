using MongoDB.Driver;
using Microsoft.Extensions.Options;

var builder = WebApplication.CreateBuilder(args);

var configSection = builder.Configuration.GetSection("DatabaseSettings");
builder.Services.Configure<DatabaseSettings>(configSection);

var app = builder.Build();

app.MapGet("/check", (IOptions<DatabaseSettings> options) =>
{
    var connectionString = options.Value.ConnectionString;

    try
    {
        using var cts = new CancellationTokenSource(TimeSpan.FromSeconds(5));
        var client = new MongoClient(connectionString);
        var databases = client.ListDatabaseNames(cts.Token).ToList();
        return Results.Ok("Zugriff auf MongoDB ok. Datenbanken: " + string.Join(", ", databases));
    }
    catch (TimeoutException ex)
    {
        return Results.Problem("Timeout beim Zugriff auf MongoDB: " + ex.Message);
    }
    catch (Exception ex)
    {
        return Results.Problem("Fehler beim Zugriff auf MongoDB: " + ex.Message);
    }
});

app.Run();

public class DatabaseSettings
{
    public string ConnectionString { get; set; } = "";
}
