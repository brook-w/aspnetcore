using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Options;

namespace CustomerDelegateMIddleware
{
    public static class WriteDateMiddlewareExtension
    {
        public static IApplicationBuilder UseWriteDate(this IApplicationBuilder builder, DateOptions options)
        {
            return builder.UseMiddleware<WriteDateMiddleware>(Options.Create(options));
        }
    }
}