using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Mango.Core.HttpService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Sample.Models;

namespace Sample.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IJsonHttpService<TestJsonEntity> _jsonHttpService;

        public HomeController(ILogger<HomeController> logger,IJsonHttpService<TestJsonEntity> jsonHttpService)
        {
            _logger = logger;
            _jsonHttpService = jsonHttpService;
        }

        public async Task<IActionResult> Index()
        {
            return View();
        }

        public async Task<IActionResult> Privacy()
        {
            var o = await _jsonHttpService.PostAsync("https://localhost:5001/api/test/get","");
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public string PrintByteArray(byte[] array)
        {
            string s = "";
            for (int i = 0; i < array.Length; i++)
            {
                s+=$"{array[i]:X2}";
                if ((i % 4) == 3) s+=" ";
            }
            return s;
        }
    }
}
