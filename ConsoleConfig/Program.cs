using System;
using System.IO;
using Microsoft.Extensions.Configuration;

namespace ConsoleConfig
{
    class Program
    {
        static void Main(string[] args)
        {
            var config = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("config.json")
                .Build();
            Console.WriteLine("Hello World!");
        }
    }
}