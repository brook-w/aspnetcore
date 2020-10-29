using System;
using System.IO;
using System.Net.Mime;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Rewrite;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Net.Http.Headers;

namespace UrlRewrite
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            #region URL 跳转

            var options = new RewriteOptions();
            options.AddRedirect("xcode/(.*)", "blog/$1")
                // 可以链式配置
                // .AddRedirect()
                // 可以重写
                .AddRewrite(@"test1/(\d+)", "test2/$1", true)
                // 配置 https
                .AddRedirectToHttps()
                ;

            // 根据配置文件 Apache
            using (StreamReader streamReader = File.OpenText(""))
            {
                options.AddApacheModRewrite(reader: streamReader);
            }

            // 根据配置文件 IIS
            using (StreamReader streamReader = File.OpenText(""))
            {
                options.AddIISUrlRewrite(reader: streamReader);
            }

            // 多种自定义添加方式
            options.Add(RedirectHelper.RedirectUrl);
            options.Add(new CustomerRule());


            app.UseRewriter(options);

            #endregion


            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapGet("/", async context => { await context.Response.WriteAsync("Hello World!"); });
            });
        }
    }

    public class RedirectHelper
    {
        public static void RedirectUrl(RewriteContext context)
        {
            if (context.HttpContext.Request.Path.StartsWithSegments(new PathString("xmlfile")))
            {
                return;
            }

            var request = context.HttpContext.Request;

            if (!request.Path.Value.EndsWith(".xml", StringComparison.OrdinalIgnoreCase)) return;
            var response = context.HttpContext.Response;
            response.StatusCode = StatusCodes.Status301MovedPermanently;
            context.Result = RuleResult.EndResponse;
            response.Headers[HeaderNames.Location] = "/xmlfile" + request.Path + request.QueryString;
        }
    }

    public class CustomerRule : IRule
    {
        public void ApplyRule(RewriteContext context)
        {
            if (context.HttpContext.Request.Path.StartsWithSegments(new PathString("xmlfile")))
            {
                return;
            }

            var request = context.HttpContext.Request;
            if (!request.Path.Value.EndsWith(".xml", StringComparison.OrdinalIgnoreCase)) return;
            var response = context.HttpContext.Response;
            response.StatusCode = StatusCodes.Status301MovedPermanently;
            context.Result = RuleResult.EndResponse;

            response.Headers[HeaderNames.Location] = "/xmlfile" + request.Path + request.QueryString;
        }
    }
}