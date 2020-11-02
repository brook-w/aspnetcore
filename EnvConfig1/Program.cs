using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;

namespace EnvConfig1
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Dictionary<string, string> config = new Dictionary<string, string>()
            {
                {
                    "name1", "lisi"
                },
                {
                    "name2", "zhangsan"
                }
            };

            // 读取自定义配置文件
            ConfigurationBuilder builder = new ConfigurationBuilder();
            builder.SetBasePath(Directory.GetCurrentDirectory())
                // 配置内存配置
                .AddInMemoryCollection(config)
                // 命令行
                .AddCommandLine(args)
                .AddCommandLine(new string[] {"key=value"})
                .AddJsonFile("config.json")
                // .AddXmlFile()  // 读取 xml 文件
                ;

            IConfiguration configuration = builder.Build();

            // 单节点
            Console.WriteLine(configuration["name"]);
            // 对象
            Console.WriteLine(configuration["address:beijing"]);
            // 数组
            Console.WriteLine(configuration["address:1:beijing"]);


            // 可以将配置文件读入一个 类对象
            configuration.GetSection("").Bind("");
            configuration.GetSection("").GetValue<Startup>("");

            var age = configuration.GetValue<int>("", 1);

            var command = configuration["key"];

            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    // 这里可以启动多个环境变量 可以选择使用一个
                    webBuilder.UseStartup<Startup>();
                    // webBuilder.UseStartup<StartupDevelopment>();
                    // webBuilder.UseStartup<StartupStaging>();
                });
    }
}