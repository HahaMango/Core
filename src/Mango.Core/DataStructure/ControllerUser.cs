using System;
using System.Collections.Generic;
using System.Text;

namespace Mango.Core.DataStructure
{
    /// <summary>
    /// 映射控制器的User
    /// </summary>
    public class ControllerUser
    {
        /// <summary>
        /// 用户Id
        /// </summary>
        public long UserId { get; set; }

        /// <summary>
        /// 用户名
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// 用户角色
        /// </summary>
        public string Role { get; set; }
    }
}
