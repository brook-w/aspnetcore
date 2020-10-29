using System.IO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.Net.Http.Headers;

namespace FileServerAndRoute
{
    public class FileController : Controller
    {
        public IActionResult GetFile()
        {
            var file = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "1.json");
            return new PhysicalFileResult(file, MediaTypeHeaderValue.Parse("application/json;utf-8"));
            
        }
    }
}