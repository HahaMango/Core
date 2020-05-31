using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Mango.Core.ApiResponse;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Sample.Controllers
{
    [ApiController]
    public class TestApiController : ControllerBase
    {
        [HttpGet("api/test/get")]
        public async Task<ApiResult> RetrunJson()
        {
            return new ApiResult
            {
                Code = Mango.Core.Enums.Code.Ok,
                Message = "消息"
            };
        }
    }
}