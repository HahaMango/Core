using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Mango.Core.Network.Abstractions
{
    /// <summary>
    /// 网络传输基础接口
    /// </summary>
    public interface INetworkTransport
    {
        /// <summary>
        /// 发送指定数据并等待返回
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        Task<byte[]> SendBytesAsync(ReadOnlyMemory<byte> data);
    }
}
