using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.AspNetCore.Routing.Constraints;
using Microsoft.Extensions.DependencyInjection;

namespace FileServerAndRoute
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
            #region 自定义路由

            // 自定义路由
            app.UseRouter(new MyRoute());
            // 自定义路由 2 
            var routeBuilder = new RouteBuilder(app);
            routeBuilder.MapRoute("blog",
                    context => { return context.Response.WriteAsync(context.GetRouteValue("key").ToString()); })
                // 在这里可以继续配置中间件路由 里面可以使用新的管道继续配置下去
                //.MapMiddlewareRoute()
                ;


            routeBuilder.MapRoute("files/{fileName}.{ext:}", //: 可以后面写正则表达式进行更高的扩展
                ctx => ctx.Response.WriteAsync($"{ctx.GetRouteValue("fileName")}---{ctx.GetRouteValue("ext")}"));


            // 动态生成 url 路径
            routeBuilder.MapRoute("", ctx =>
            {
                var dictionary = new RouteValueDictionary() {{"filename", "xcode"}};
                var vpc = new VirtualPathContext(ctx, null, dictionary);
                var path = routeBuilder.Build().GetVirtualPath(vpc).VirtualPath;
                return ctx.Response.WriteAsync(path);
            });

            app.UseRouter(routeBuilder.Build());

            #endregion


            app.UseRouting();
            app.UseEndpoints(builder =>
            {
                builder.MapControllerRoute(
                    "default",
                    "",
                    null,
                    constraints: new
                    {
                        controller = new LengthRouteConstraint(3, 8),
                        id = new IntRouteConstraint(),
                        action = new RegexRouteConstraint(@"\d[1,2,3]")
                    }
                );
            });
        }
    }

    public class MyRoute : IRouter
    {
        public VirtualPathData GetVirtualPath(VirtualPathContext context)
        {
            throw new System.NotImplementedException();
        }

        public Task RouteAsync(RouteContext context)
        {
            throw new System.NotImplementedException();
        }
    }
}