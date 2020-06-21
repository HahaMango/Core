using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace Mango.Core.Rpc.Abstractions.DataStructure
{
    /// <summary>
    /// Rpc方法请求体
    /// </summary>
    public class MethodRpcRequest
    {
        /// <summary>
        /// 目标类型
        /// </summary>
        public Type TargetType { get; set; }

        /// <summary>
        /// 目标方法
        /// </summary>
        public MethodInfo TargetMethod { get; set; }

        /// <summary>
        /// 目标方法请求参数列表
        /// </summary>
        public object[] Params { get; set; }
    }
}
