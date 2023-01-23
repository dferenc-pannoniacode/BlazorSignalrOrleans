using Microsoft.Extensions.Hosting;
using Serilog;
using System.Runtime.CompilerServices;

namespace BlazorSignalrOrleans.Silo
{
    public class Program
    {
        private static IHost? _host;

        static int Main(string[] args)
        {
            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Debug()
                .WriteTo.Async(x => x.Console())
                .CreateLogger();

            StartHost().Wait();

            Console.WriteLine("\n\n Press Enter to terminate...\n\n");
            Console.ReadLine();

            return 0;
        }

        public static async Task StartHost()
        {
            Log.Information("Silo starting up.");

            _host = Host.CreateDefaultBuilder()
                .UseOrleans(siloBuilder =>
                {
                    siloBuilder.UseLocalhostClustering();
                    siloBuilder.ConfigureLogging(x => x.AddSerilog());
                })
                .Build();

            await _host.StartAsync();
        }
        public static async Task StopHost()
        {
            if (_host != null)
            {
                Log.Information("Silo shutting down.");

                await _host.StopAsync();
            }
        }
    }
}