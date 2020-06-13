using Mango.Core.Network.Abstractions;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace Mango.Core.Network
{
    /// <summary>
    /// 
    /// </summary>
    public class SingletonSocketConnection : ISocketConnection
    {
        private static SingletonSocketConnection _socketConnection;

        private SingletonSocketConnection()
        {
            Socket = new Socket(SocketType.Stream, ProtocolType.Tcp);

            Console.WriteLine($"Connecting to {Socket.RemoteEndPoint}");

            Socket.Connect(new IPEndPoint(IPAddress.Loopback, 8087));
            NetworkStream = new NetworkStream(Socket);
        }

        /// <summary>
        /// 获取一个SocketConnection
        /// </summary>
        /// <returns></returns>
        public static SingletonSocketConnection Instance()
        {
            if(_socketConnection == null)
            {
                _socketConnection = new SingletonSocketConnection();
            }
            return _socketConnection;
        }

        /// <summary>
        /// 套接字
        /// </summary>
        public Socket Socket { get; }

        /// <summary>
        /// 关联套接字的网络流
        /// </summary>
        public NetworkStream NetworkStream { get; }
    }
}
