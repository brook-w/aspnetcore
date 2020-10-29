using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;

namespace PipelineAndMiddleware.Middleware
{
    public class RequestSetOptionsMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly IOptions<AppOptions> _options;

        public RequestSetOptionsMiddleware(RequestDelegate next, IOptions<AppOptions> options)
        {
            _next = next;
            _options = options;
        }

        public async Task Invoke(HttpContext httpContext)
        {
            Console.WriteLine("RequestSetOptionsMiddleware.Invoke");
            var option = httpContext.Request.Query["option"];

            if (!string.IsNullOrEmpty(option))
            {
                await httpContext.Response.WriteAsync("this is test");
                _options.Value.Options = option;
            }
            else
            {
                await _next(httpContext);
            }
        }
    }

    public class AppOptions
    {
        public string Options { get; set; }
    }
}