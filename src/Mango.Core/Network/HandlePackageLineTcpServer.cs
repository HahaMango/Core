using Mango.Core.Serialization.Extension;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Mango.Core.Network
{
    /// <summary>
    /// 处理行封装TCP服务端
    /// </summary>
    public abstract class HandlePackageLineTcpServer<T> : LineAbstractTcpServer
        where T : DataPackage , new()
    {
        public override async ValueTask<Memory<byte>> Handle(ReadOnlyMemory<byte> input)
        {
            var requestString = Encoding.UTF8.GetString(input.ToArray());
            var jsonObject = await requestString.ToObjectAsync<T>();
            var businessResult = await HandleBusiness(jsonObject.Data);
            var response = new T();
            response.Id = jsonObject.Id;
            response.Data = businessResult.ToArray();
            return Encoding.UTF8.GetBytes(response.ToJson());
        }

        protected abstract ValueTask<Memory<byte>> HandleBusiness(ReadOnlyMemory<byte> input);
    }
}
