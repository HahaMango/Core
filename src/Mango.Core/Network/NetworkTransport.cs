using Mango.Core.Network.Abstractions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Mango.Core.Network
{
    /// <summary>
    /// 网络传输基础接口实现
    /// </summary>
    public class NetworkTransport : INetworkTransport
    {
        private readonly ISocketConnection _socketConnection;

        /// <summary>
        /// 返回最近一次的连接状态
        /// </summary>
        public bool Connection
        {
            get
            {
                return _socketConnection?.Socket != null ? _socketConnection.Socket.Connected : false;
            }
        }

        public NetworkTransport(ISocketConnection socketConnection)
        {
            _socketConnection = socketConnection;
        }

        /// <summary>
        /// 发送指定数据并等待返回
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public async Task<byte[]> SendBytesAsync(ReadOnlyMemory<byte> data)
        {
            if(_socketConnection.NetworkStream == null)
            {
                throw new NullReferenceException(nameof(_socketConnection.NetworkStream));
            }
            var sr = new StreamWriter(_socketConnection.NetworkStream);
            await sr.WriteLineAsync("hello world");
            await sr.FlushAsync();
            return Encoding.UTF8.GetBytes("hello");
        }
    }
}
