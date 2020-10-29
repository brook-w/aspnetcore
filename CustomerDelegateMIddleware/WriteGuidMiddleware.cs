using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

namespace CustomerDelegateMIddleware
{
    public class WriteGuidMiddleware : IMiddleware
    {
        private readonly IGuidService _guidService;

        public WriteGuidMiddleware(IGuidService guidService)
        {
            _guidService = guidService;
        }

        public Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            // 两种方式实现服务获取
            IGuidService guidService = context.RequestServices.GetService<IGuidService>();
            
            
            context.Response.WriteAsync(_guidService.Guid());
            return next(context);
        }
    }

    public interface IGuidService
    {
        public string Guid();
    }

    public class DefaultGuidService : IGuidService
    {
        public string Guid()
        {
            return System.Guid.NewGuid().ToString("N");
        }
    }

    public static class GuidServiceExtension
    {
        public static IServiceCollection AddGuidService(this IServiceCollection serviceCollection)
        {
            return serviceCollection.AddTransient<IGuidService, DefaultGuidService>();
        }
    }
}