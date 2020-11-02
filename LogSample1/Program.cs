using System;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;


namespace LogSample1
{
    class Program
    {
        static void Main(string[] args)
        {
            ILoggerFactory factory = new LoggerFactory();
            IServiceCollection serviceCollection = new ServiceCollection();
            ILogger logger = factory.CreateLogger("");
            logger.Log(LogLevel.Debug, "");
            logger.LogDebug("");
            Console.WriteLine("Hello World!");
        }
    }
}