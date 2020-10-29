using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using PipelineAndMiddleware.Service;

namespace PipelineAndMiddleware.Controllers
{
    [ApiController]
    [Route("api/[Controller]")]
    public class TodoController : Controller
    {
        private readonly IService _service;

        public TodoController(IService service)
        {
            _service = service;
        }

        [HttpGet]
        public string Get()
        {
            return _service.GetGuid();
        }

        [HttpGet("GetService")]
        public string GetService()
        {
            var service = HttpContext.RequestServices.GetService<IService>();

            return service.GetGuid();
        }
    }
}