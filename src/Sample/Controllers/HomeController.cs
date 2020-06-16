using Mango.Core.Network;
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
        private readonly NetworkTransport _networkTransport;

        public HomeController(NetworkTransport networkTransport)
        {
            _networkTransport = networkTransport;
        }

        public async Task<IActionResult> Index()
        {
            return View();
        }

        public async Task<string> Privacy()
        {
            var result = await _networkTransport.SendBytesAsync(Encoding.ASCII.GetBytes("hello world"));
            var s = Encoding.ASCII.GetString(result);
            return s;
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
