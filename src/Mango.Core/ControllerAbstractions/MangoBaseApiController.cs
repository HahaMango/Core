using Mango.Core.ApiResponse;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Text;

namespace Mango.Core.ControllerAbstractions
{
    public abstract class MangoBaseApiController : ControllerBase
    {
        /// <summary>
        /// 返回成功
        /// </summary>
        /// <returns></returns>
        public virtual ApiResult OK()
        {
            return new ApiResult
            {
                Code = Enums.Code.Ok,
                Message = "成功"
            };
        }

        /// <summary>
        /// 返回错误
        /// </summary>
        /// <returns></returns>
        public virtual ApiResult Error()
        {
            return new ApiResult
            {
                Code = Enums.Code.Error,
                Message = "错误"
            };
        }

        /// <summary>
        /// 返回授权错误
        /// </summary>
        /// <returns></returns>
        public virtual ApiResult AuthorizeError()
        {
            return new ApiResult
            {
                Code = Enums.Code.Unauthorized,
                Message = "该操作需要授权登录后才能继续进行"
            };
        }

        /// <summary>
        /// 返回权限错误
        /// </summary>
        /// <returns></returns>
        public virtual ApiResult ForbiddenError()
        {
            return new ApiResult
            {
                Code = Enums.Code.Forbidden,
                Message = "当前用户权限不足，不能继续执行"
            };
        }
    }
}
