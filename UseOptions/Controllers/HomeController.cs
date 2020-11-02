using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using UseOptions.Models;

namespace UseOptions.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IOptions<LogLevelOption> _options;

        public HomeController(ILogger<HomeController> logger, IOptions<LogLevelOption> options,IOptionsSnapshot<LogLevelOption> optionsSnapshot)
        {
            _logger = logger;
            _options = options;
            // optionsSnapshot.Get("_options1")
            // optionsSnapshot.Get("_options2")
        }

        public IActionResult Index()
        {
            Console.WriteLine(_options.Value.Default);
            Console.WriteLine(_options.Value.Microsoft);
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel {RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier});
        }
    }
}