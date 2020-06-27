using Mango.Core.Network;
using Mango.Core.Network.Abstractions;
using Mango.Core.Rpc.Abstractions;
using Microsoft.AspNetCore.Mvc;
using Sample.Models;
using System;
using System.Diagnostics;
using System.Text;
using System.Threading.Tasks;

namespace Sample.Controllers
{
    public class HomeController : Controller
    {
        private readonly IMangoTcpClient _client;

        public HomeController(IMangoTcpClient client)
        {
            _client = client;
        }

        public async Task<IActionResult> Index()
        {
            return View();
        }

        public async Task<IActionResult> Privacy()
        {
            MyTest.Test();
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
