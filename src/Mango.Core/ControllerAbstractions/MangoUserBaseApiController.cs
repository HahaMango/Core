using Mango.Core.ApiResponse;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Text;

namespace Mango.Core.ControllerAbstractions
{
    /// <summary>
    /// 提供获取用户信息控制器抽象
    /// </summary>
    public abstract class MangoUserBaseApiController<TUser> : MangoBaseApiController
    {
        public abstract TUser GetUser();
    }
}
