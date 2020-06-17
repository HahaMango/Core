using System;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Text;

namespace Mango.Core.Network.Abstractions
{
    /// <summary>
    /// 套接字连接接口
    /// </summary>
    [Obsolete]
    public interface ISocketConnection
    {
        /// <summary>
        /// 套接字
        /// </summary>
        Socket Socket { get; }

        /// <summary>
        /// 对应套接字的网络连接流
        /// </summary>
        NetworkStream NetworkStream { get; }
    }
}
