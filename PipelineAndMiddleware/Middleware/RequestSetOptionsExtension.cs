using Microsoft.AspNetCore.Builder;

namespace PipelineAndMiddleware.Middleware
{
    public static class RequestSetOptionsExtension
    {
        public static IApplicationBuilder UseRequestSetOptions(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<RequestSetOptionsMiddleware>();
        }
    }
}