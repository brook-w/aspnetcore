using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using PipelineAndMiddleware.Middleware;

namespace PipelineAndMiddleware
{
    public class RequestSetOptionsStartup : IStartupFilter
    {
        public Action<IApplicationBuilder> Configure(Action<IApplicationBuilder> next)
        {
            return builder =>
            {
                // 注入自自定义中间件
                builder.UseMiddleware<RequestSetOptionsMiddleware>();

                // 这样的扩展也是可以的
                // builder.UseRequestSetOptions();

                builder.UseCors("test");
                next(builder);
            };
        }
    }
}