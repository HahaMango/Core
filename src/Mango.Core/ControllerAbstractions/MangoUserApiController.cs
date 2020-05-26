using Mango.Core.DataStructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;

namespace Mango.Core.ControllerAbstractions
{
    /// <summary>
    /// 基于用户的基础控制器实现
    /// </summary>
    public class MangoUserApiController : MangoUserBaseApiController<ControllerUser>
    {
        /// <summary>
        /// 获取用户信息（只返回授权用户信息，没有授权返回为空）
        /// </summary>
        /// <returns></returns>
        public override ControllerUser GetUser()
        {
            if(User == null)
            {
                return null;
            }
            if (!User.Identity.IsAuthenticated)
            {
                return null;
            }
            var identity = User.Identity;
            var userId = User.Claims.FirstOrDefault(item => item.Type == ClaimTypes.NameIdentifier)?.Value;
            if(!string.IsNullOrEmpty(userId))
            {
                return null;
            }
            var userName = User.Claims.FirstOrDefault(item => item.Type == ClaimTypes.Name)?.Value;
            var role = User.Claims.FirstOrDefault(item => item.Type == ClaimTypes.Role)?.Value;
            return new ControllerUser
            {
                UserId = Convert.ToInt64(userId),
                UserName = userName,
                Role = role
            };
        }
    }
}
