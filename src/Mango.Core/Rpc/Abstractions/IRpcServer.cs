using Mango.Core.Rpc.Abstractions.DataStructure;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Mango.Core.Rpc.Abstractions
{
    /// <summary>
    /// RPC服务器接口
    /// </summary>
    public interface IRpcServer
    {
        /// <summary>
        /// 处理单条请求
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<MethodRpcResponse> Process(MethodRpcRequest request);
    }
}
