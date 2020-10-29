using System;
using CustomerDelegateMIddleware;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using PipelineAndMiddleware.Service;

namespace PipelineAndMiddleware
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddGuidService();
            // 自定义 Startup 并类似于管道配置
            // services.AddTransient<IStartupFilter, RequestSetOptionsStartup>();
            // services.AddControllers();

            // 多例
            // services.AddTransient<IService, Service.Service>();
            // 单例 
            // services.AddSingleton<IService>(new Service.Service());
            // services.AddScoped();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseMiddleware<WriteGuidMiddleware>(); // 与上面 AddGuidService 为一对

            app.Use(async (context, next) =>
            {
                await context.Response.WriteAsync("AAA");
                await next.Invoke();
            });

            app.Use(async (context, next) =>
            {
                await context.Response.WriteAsync("BBB");
                await next.Invoke();
            });
            app.Use(new DelegateMiddleware().DelegateMiddleware1);

            // 映射请求分支
            app.Map("/map1",
                appBuilder =>
                {
                    app.Use(new DelegateMiddleware().DelegateMiddleware1).Run(async context =>
                    {
                        await context.Response.WriteAsync("map1");
                    });
                });

            app.Map("/map2",
                appBuilder =>
                {
                    app.Use(new DelegateMiddleware().DelegateMiddleware1).Run(async context =>
                    {
                        await context.Response.WriteAsync("map1");
                    });
                });


            app.MapWhen(context =>
                {
                    // 这里就可以判断信息进行请求相应
                    return true;
                },
                appBuilder =>
                {
                    app.Use(new DelegateMiddleware().DelegateMiddleware1).Run(async context =>
                    {
                        await context.Response.WriteAsync("map1");
                    });
                });

            app.UseMiddleware<WriteDateMiddleware>();
            app.UseWriteDate(new DateOptions {Format = "yyyy-MM-dd"});

            // 在请求末端配置  必须配置
            app.Run(async (context) => { await context.Response.WriteAsync(DateTime.Now.ToLongDateString()); });


            return;
            if (env.IsDevelopment()) app.UseDeveloperExceptionPage();

            app.UseStaticFiles();

            app.UseRouting();
            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
        }
    }
}