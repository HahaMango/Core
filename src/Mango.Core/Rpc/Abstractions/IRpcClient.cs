using Castle.DynamicProxy;
using ImpromptuInterface;
using Mango.Core.Rpc.Abstractions.DataStructure;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Text;
using System.Threading.Tasks;

namespace Mango.Core.Rpc.Abstractions
{
    /// <summary>
    /// RPC客户端
    /// </summary>
    public interface IRpcClient
    {
        /// <summary>
        /// rpc调用方法
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<MethodRpcResponse> InvokeMethodAsync(MethodRpcRequest request);
    }
}
