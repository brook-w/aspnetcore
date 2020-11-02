using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Hosting;

namespace FileProviderSample
{
    public class Startup
    {
        // IFileProvider; 文件提供者相应的提供者

        // PhysicalFileProvider;
        // EmbeddedFileProvider;
        // CompositeFileProvider;

        public void ConfigureServices(IServiceCollection services)
        {
            IFileProvider physicalFileProvider = new PhysicalFileProvider(Directory.GetCurrentDirectory());
            IFileProvider embeddedFileProvider = new EmbeddedFileProvider(Assembly.GetEntryAssembly());

            // 组合文件系统
            // 装饰器模式
            IFileProvider compositeFileProvider = new CompositeFileProvider(physicalFileProvider, embeddedFileProvider);
            services.AddSingleton<IFileProvider>(compositeFileProvider);
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }


            app.UseEndpoints(endpoints =>
            {
                endpoints.MapGet("/", async context => { await context.Response.WriteAsync("Hello World!"); });
            });
        }
    }
}