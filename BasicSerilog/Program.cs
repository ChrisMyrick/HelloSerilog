using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Serilog;

namespace BasicSerilog
{
    class Program
    {
        static void Main(string[] args)
        { 
            LogToConsole();
            LogToFile();
            LogParameters();
        }

        /// <summary>
        /// Example of logging to the console window
        /// </summary>
        private static void LogToConsole()
        {
            // A LoggerConfiguration is used to create and assign the default logger.
            var log = new LoggerConfiguration()
                .WriteTo.Console()
                .CreateLogger();

            // The simplest way to set up Serilog is using the static Log class.
            // Apply the configuration to the static logger
            Log.Logger = log;

            Log.Information("The static log object has been configured.");
            Log.Warning("This is a warning message.");
        }

        /// <summary>
        /// Example of logging to a rolling log file
        /// </summary>
        private static void LogToFile()
        {
            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Debug()
                .WriteTo.Console()
                .WriteTo.File("logs\\myapp.txt", rollingInterval: RollingInterval.Day)
                .CreateLogger();

            int a = 10, b = 0;
            try
            {
                Log.Debug("Dividing {A} by {B}", a, b);
                Console.WriteLine(a / b);
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Something went wrong");
            }
        }

        /// <summary>
        /// Example of converting params to JSON and output to log file
        /// </summary>
        private static void LogParameters()
        {
            var position = new { Latitude = 25, Longitude = 134 };
            var elapsedMs = 34;

            // The @ operator in front of Position tells Serilog to serialize the object
            // passed in, rather than convert it using ToString().
            Log.Information("Processed {@Position} in {Elapsed} ms.", position, elapsedMs);

            Console.ReadKey();
            Log.CloseAndFlush();
        }
    }
}
