using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.NodeServices;
using Microsoft.AspNetCore.SpaServices.StaticFiles;
using Microsoft.Extensions.Logging;
using StaticFileAutoCompile.Models;
using Microsoft.AspNetCore.SpaServices;

namespace StaticFileAutoCompile.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly INodeServices _nodeServices;

        public HomeController(ILogger<HomeController> logger, INodeServices nodeServices)
        {
            _logger = logger;
            _nodeServices = nodeServices;
        }

        public async Task<IActionResult> Index()
        {
            var result = await _nodeServices.InvokeAsync<int>("Node/node.js", 1, 2);
         
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
