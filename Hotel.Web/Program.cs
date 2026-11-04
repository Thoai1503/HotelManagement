
using Hotel.Web;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;

public class Program
{
    public static void Main(string[] args)
    {
        Console.WriteLine("Starting Hotel.Web application...");
        CreateWebHostBuilder(args).Build().Run();
    }

    public static IWebHostBuilder CreateWebHostBuilder(string[] args)
    {
        LoadDotEnv();
        return WebHost.CreateDefaultBuilder(args)
            .UseStartup<Startup>();
    }

    internal static void LoadDotEnv()
    {
        var envFile = FindDotEnvFile();
        if (envFile is null)
        {
            return;
        }

        var lineNumber = 0;
        foreach (var line in File.ReadLines(envFile))
        {
            lineNumber++;
            var entry = line.Trim();
            if (entry.Length == 0 || entry.StartsWith('#'))
            {
                continue;
            }

            if (entry.StartsWith("export ", StringComparison.Ordinal))
            {
                entry = entry["export ".Length..].TrimStart();
            }

            var separator = entry.IndexOf('=');
            if (separator <= 0)
            {
                throw new FormatException(
                    $"Invalid .env entry at line {lineNumber}.");
            }

            var key = entry[..separator].Trim();
            var value = entry[(separator + 1)..].Trim();
            if (key.Length == 0)
            {
                throw new FormatException(
                    $"Invalid .env entry at line {lineNumber}.");
            }

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

    private static string? FindDotEnvFile()
    {
        var startDirectories = new[]
        {
            Directory.GetCurrentDirectory(),
            AppContext.BaseDirectory
        };

        foreach (var startDirectory in startDirectories)
        {
            for (var directory = new DirectoryInfo(startDirectory);
                 directory is not null;
                 directory = directory.Parent)
            {
                var candidate = Path.Combine(directory.FullName, ".env");
                if (File.Exists(candidate))
                {
                    return candidate;
                }

                var webProjectCandidate = Path.Combine(
                    directory.FullName, "Hotel.Web", ".env");
                if (File.Exists(webProjectCandidate))
                {
                    return webProjectCandidate;
                }
            }
        }

        return null;
    }
}