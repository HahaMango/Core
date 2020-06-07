using Mango.Infrastructure.Helper;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace MangoTest
{
    public class HttpTest
    {
        [Fact]
        public async Task TestGet()
        {
            var result = await HttpHelper.GetAsync("https://cn.bing.com/");
            Assert.True(result.IsSuccessStatusCode);
        }
    }
}
