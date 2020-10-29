using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.NetworkInformation;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Hosting;

namespace FileServerAndRoute
{
    public class StartupFileServer
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            // 这个中间件需要放在 UseStaticFiles 前面 因为会扫描  index.html default.html 等文件
            // 就像这样被指定
            // public DefaultFilesOptions(SharedOptions sharedOptions)
            //     : base(sharedOptions)
            //     => this.DefaultFileNames = (IList<string>) new List<string>()
            //     {
            //         "default.htm",
            //         "default.html",
            //         "index.htm",
            //         "index.html"
            //     };

            app.UseDefaultFiles();

            // 使用目录浏览
            app.UseDirectoryBrowser();
            app.UseDefaultFiles(new DefaultFilesOptions()
            {
                DefaultFileNames = new string[] {"demo.html"}
            });

            // 自定义文件提供者
            app.UseStaticFiles();

            app.UseStaticFiles(new StaticFileOptions()
            {
                // 可以添加文件缓存标记
                OnPrepareResponse = (context) =>
                {
                    context.Context.Response.Headers.Add("Cache-Control", "Public,max-age=600");
                },
                FileProvider = new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), "files")),
                RequestPath = "/samplefile"
            });

            // 使用文件服务会代替 UseStaticFiles UseDirectoryBrowser UseDefaultFiles
            app.UseFileServer();


            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapGet("/", async context => { await context.Response.WriteAsync("Hello World!"); });
            });
        }
    }
}