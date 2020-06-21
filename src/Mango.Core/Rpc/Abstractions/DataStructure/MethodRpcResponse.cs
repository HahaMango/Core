using System;
using System.Collections.Generic;
using System.Text;

namespace Mango.Core.Rpc.Abstractions.DataStructure
{
    /// <summary>
    /// Rpc方法响应体
    /// </summary>
    public class MethodRpcResponse<T>
    {
        /// <summary>
        /// 状态信息
        /// </summary>
        public int Status { get; set; }

        /// <summary>
        /// 方法返回值
        /// </summary>
        public T ReturnData { get; set; }

        /// <summary>
        /// 调用异常信息
        /// </summary>
        public System.Exception Exception { get; set; }
    }
}
