using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace EnvConfig1
{
    /// <summary>
    /// 环境变量和配置文件
    /// </summary>
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; set; }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            Console.WriteLine(Configuration["SiteName"]);
            Console.WriteLine(Configuration.GetSection("SiteName").Value);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            // 环境判断
            // Environments.Production;
            // Environments.Development;
            // Environments.Staging;
            // if (env.IsDevelopment())
            // {
            // }
            // if (env.IsProduction())
            // {
            // }
            // if (env.IsStaging())
            // {
            // }

            Console.WriteLine("到了这里");

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapGet("/", async context => { await context.Response.WriteAsync("Hello World!"); });
            });
        }


        /// <summary>
        /// 可以根据环境名方法
        /// </summary>
        /// <param name="app"></param>
        /// <param name="env"></param>
        public void ConfigureDevelopment(IApplicationBuilder app, IWebHostEnvironment env)
        {
            Console.WriteLine("Development");
        }

        public void ConfigureProduction(IApplicationBuilder app, IWebHostEnvironment env)
        {
            Console.WriteLine("Production");
        }
    }
}