using Mango.Core.Network.Abstractions;
using System;
using System.Threading.Tasks;

namespace Mango.Core.Network
{
    /// <summary>
    /// 以行（\n）为单位处理的TCP客户端
    /// </summary>
    public class LineProcessTcpClient : IMangoTcpClient
    {
        public Task SendAsync(byte[] sendData)
        {
            throw new NotImplementedException();
        }

        public Task SendAsync(ReadOnlyMemory<byte> sendData)
        {
            throw new NotImplementedException();
        }

        public Task<Memory<byte>> TakeResponseAsync(byte[] sendData)
        {
            throw new NotImplementedException();
        }

        public Task<Memory<byte>> TakeResponseAsync(ReadOnlyMemory<byte> sendData)
        {
            throw new NotImplementedException();
        }
    }
}
