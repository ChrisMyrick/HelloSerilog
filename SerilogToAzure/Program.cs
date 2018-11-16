using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Serilog;

namespace SerilogToAzure
{
    class Program
    {
        static void Main(string[] args)
        {
            // Test
            // A LoggerConfiguration can be read from app.settings
            Log.Logger = new LoggerConfiguration()
                .ReadFrom.AppSettings()
                .CreateLogger();

            LogToAzure();
        }

        private static void LogToAzure()
        {
            Console.WriteLine("Logging to Azure...");

            var position = new { Latitude = 25, Longitude = 134 };
            var elapsedMs = 34;

            // The @ operator in front of Position tells Serilog to serialize the object
            // passed in, rather than convert it using ToString().
            Log.Information("Hello from program.cs in SerilogToAzure.");
            Log.Error("Processed {@Position} in {Elapsed} ms.", position, elapsedMs);                      

            Console.Read();
            Log.CloseAndFlush();
        }
    }
}
