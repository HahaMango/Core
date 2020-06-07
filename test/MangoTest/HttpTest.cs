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
            var parm = new Dictionary<string, string>
            {
                { "q", "百度" }
            };
            var result = await HttpHelper.GetAsync("https://cn.bing.com/search", parm);
            Assert.True(result.IsSuccessStatusCode);
        }
    }
}
