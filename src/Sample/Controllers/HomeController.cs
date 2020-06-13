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
using Mango.Core.Network;

namespace Sample.Controllers
{
    public class HomeController : Controller
    {

        public HomeController()
        {
        }

        public async Task<IActionResult> Index()
        {
            return View();
        }

        public async Task<IActionResult> Privacy()
        {
            NetworkTransport nt = new NetworkTransport(SingletonSocketConnection.Instance());
            await nt.SendBytesAsync(new ReadOnlyMemory<byte>());
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
