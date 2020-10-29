using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;

namespace CustomerDelegateMIddleware
{
    public class WriteDateMiddleware
    {
        private readonly RequestDelegate _next;

        private IOptions<DateOptions> _options;

        public WriteDateMiddleware(RequestDelegate next, IOptions<DateOptions> options)
        {
            _next = next;
            _options = options;
        }

        public Task InvokeAsync(HttpContext context)
        {
            context.Response.WriteAsync(DateTime.Now.ToString(_options.Value.Format));
            return _next(context);
        }
    }
}