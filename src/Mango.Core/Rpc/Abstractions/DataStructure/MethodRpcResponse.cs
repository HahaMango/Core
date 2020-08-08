using System;
using System.Collections.Generic;
using System.Text;

namespace Mango.Core.Rpc.Abstractions.DataStructure
{
    /// <summary>
    /// Rpc方法响应体
    /// </summary>
    [Serializable]
    public class MethodRpcResponse
    {
        /// <summary>
        /// 状态信息
        /// </summary>
        public int Status { get; set; }

        /// <summary>
        /// 返回类型
        /// </summary>
        public Type ReturnType { get; set; }

        /// <summary>
        /// 方法返回值
        /// </summary>
        public object ReturnData { get; set; }

        /// <summary>
        /// 调用异常信息
        /// </summary>
        public System.Exception Exception { get; set; }
    }
}
