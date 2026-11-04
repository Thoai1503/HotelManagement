using MySqlConnector;

LoadEnvironmentFile();
var connectionString =
    Environment.GetEnvironmentVariable("ConnectionStrings__DefaultConnection")
    ?? throw new InvalidOperationException("Database connection string is not configured.");

await using var connection = new MySqlConnection(connectionString);
await connection.OpenAsync();
Console.WriteLine($"Database: {connection.Database}");

await using (var command = connection.CreateCommand())
{
    command.CommandText = """
        SELECT COLUMN_NAME
        FROM INFORMATION_SCHEMA.COLUMNS
        WHERE TABLE_SCHEMA = DATABASE()
            AND TABLE_NAME = 'hotels'
        ORDER BY ORDINAL_POSITION
        """;
    await using var reader = await command.ExecuteReaderAsync();
    Console.WriteLine("Hotel columns:");
    while (await reader.ReadAsync())
    {
        Console.WriteLine($" - {reader.GetString(0)}");
    }
}

await using (var command = connection.CreateCommand())
{
    command.CommandText = """
        SELECT MigrationId
        FROM __EFMigrationsHistory
        ORDER BY MigrationId
        """;
    await using var reader = await command.ExecuteReaderAsync();
    Console.WriteLine("Migration history:");
    while (await reader.ReadAsync())
    {
        Console.WriteLine($" - {reader.GetString(0)}");
    }
}

static void LoadEnvironmentFile()
{
    var file = Path.Combine(Directory.GetCurrentDirectory(), "Hotel.Web", ".env");
    if (!File.Exists(file))
    {
        file = Path.Combine(Directory.GetCurrentDirectory(), ".env");
    }

    foreach (var rawLine in File.ReadLines(file))
    {
        var line = rawLine.Trim();
        if (line.Length == 0 || line.StartsWith('#'))
        {
            continue;
        }

        if (line.StartsWith("export ", StringComparison.Ordinal))
        {
            line = line["export ".Length..].TrimStart();
        }

        var separator = line.IndexOf('=');
        if (separator <= 0)
        {
            continue;
        }

        var key = line[..separator].Trim();
        var value = line[(separator + 1)..].Trim();
        if (value.Length >= 2 &&
            ((value[0] == '"' && value[^1] == '"') ||
             (value[0] == '\'' && value[^1] == '\'')))
        {
            value = value[1..^1];
        }

        if (Environment.GetEnvironmentVariable(key) is null)
        {
            Environment.SetEnvironmentVariable(key, value);
        }
    }
}
