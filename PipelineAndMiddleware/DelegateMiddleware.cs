using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace PipelineAndMiddleware
{
    public class DelegateMiddleware
    {
        public Task DelegateMiddleware1(HttpContext httpContext, Func<Task> next)
        {
            httpContext.Response.WriteAsync("CCC");
            return next();
        }
    }
}