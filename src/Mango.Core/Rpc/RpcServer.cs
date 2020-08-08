using Mango.Core.Network;
using Mango.Core.Network.Abstractions;
using Mango.Core.Rpc.Abstractions;
using Mango.Core.Rpc.Abstractions.DataStructure;
using Microsoft.Extensions.Logging;
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
    /// RPC服务器
    /// </summary>
    internal class RpcServer : HandlePackageLineTcpServer<DataPackage>, IRpcServer
    {
        private readonly IServiceProvider _serviceProvider;

        public RpcServer(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        /// <summary>
        /// 处理单个请求
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<MethodRpcResponse> Process(MethodRpcRequest request)
        {
            var response = new MethodRpcResponse();
            if(request == null)
            {
                response.Status = -1;
                response.Exception = new ArgumentNullException(nameof(request));
                return response;
            }
            var service = _serviceProvider.GetService(request.TargetType);
            if(service == null)
            {
                response.Status = -1;
                response.Exception = new InvalidOperationException("远程方法不存在");
                return response;
            }
            var methodInfo = request.TargetMethod;
            var result = methodInfo.Invoke(service, request.Params);

            response.Status = 1;
            response.ReturnData = result;
            response.ReturnType = methodInfo.ReturnType;
            return response;
        }

        /// <summary>
        /// 处理单个原始TCP请求
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        protected override async ValueTask<Memory<byte>> HandleBusiness(ReadOnlyMemory<byte> input)
        {
            IFormatter formatter = new BinaryFormatter();
            using(var ms = new MemoryStream())
            {
                await ms.WriteAsync(input.ToArray(), 0, input.Length);
                var request = (MethodRpcRequest)formatter.Deserialize(ms);
                var response = await Process(request);
                ms.Position = 0;
                formatter.Serialize(ms, response);
                return ms.ToArray();
            }
        }
    }
}
