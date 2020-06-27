using Mango.Core.Network.Abstractions;
using Mango.Core.Rpc.Abstractions;
using Mango.Core.Rpc.Abstractions.DataStructure;
using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;

namespace Mango.Core.Rpc
{
    /// <summary>
    /// RPC请求客户端
    /// </summary>
    public class RpcClient : IRpcClient
    {
        private readonly IMangoTcpClient _tcpClient;

        public RpcClient(IMangoTcpClient tcpClient)
        {
            _tcpClient = tcpClient;
        }

        /// <summary>
        /// 发送RPC请求
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<MethodRpcResponse> InvokeMethodAsync(MethodRpcRequest request)
        {
            IFormatter formatter = new BinaryFormatter();
            using (var ms = new MemoryStream())
            {
                formatter.Serialize(ms, request);
                var response = await _tcpClient.TakeResponseAsync(ms.ToArray());
                ms.Position = 0;
                await ms.WriteAsync(response.ToArray(), 0, response.Length);
                return (MethodRpcResponse)formatter.Deserialize(ms);
            }
        }
    }
}
