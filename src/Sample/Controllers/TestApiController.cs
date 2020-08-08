using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Mango.Core.ApiResponse;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Sample.Models;

namespace Sample.Controllers
{
    [ApiController]
    public class TestApiController : ControllerBase
    {
        [HttpPost("api/test/get")]
        public async Task<TestJsonEntity> RetrunJson([FromBody]TestJsonEntity testJsonEntity)
        {
            return testJsonEntity;
        }
    }
}