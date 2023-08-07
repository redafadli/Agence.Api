using Agence.Api;
using Serilog;
using System.Reflection;

internal class Program {
    private static readonly IConfigurationRoot configuration = ConfigureConfiguration(new ConfigurationBuilder());

    public static void Main(string[] args) {
        try {
            CreateHostBuilder(args).Build().Run();
        } catch (Exception ex) {
            Log.Fatal($"Failed to start {Assembly.GetExecutingAssembly().GetName().Name}", ex);
            throw;
        }
    }

    public static IHostBuilder CreateHostBuilder(string[] args) =>
        Microsoft.Extensions.Hosting.Host.CreateDefaultBuilder(args)
            .ConfigureHostConfiguration(x => ConfigureConfiguration(x))
            .ConfigureAppConfiguration(x => ConfigureConfiguration(x))
            .ConfigureWebHostDefaults(webBuilder => {
                webBuilder.UseStartup<Startup>();
            });

    private static IConfigurationRoot ConfigureConfiguration(IConfigurationBuilder builder) {
        // Load appsettings.json
        builder.SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: false)
            .AddJsonFile("appsettings.env.json", optional: true, reloadOnChange: false)
            .AddEnvironmentVariables();
        return builder.Build();
    }
}
