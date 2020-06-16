using Mango.Core.Network.Abstractions;
using Mango.Core.Serialization.Extension;
using System;
using System.Buffers;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.IO.Pipelines;
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

        private readonly BlockingCollection<TcpMessage> _col;

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
            _col = new BlockingCollection<TcpMessage>();
            _socketConnection = socketConnection;
            Task.Run(Revice);
        }

        /// <summary>
        /// 发送指定数据并等待返回
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public async Task<byte[]> SendBytesAsync(ReadOnlyMemory<byte> data)
        {
            if (_socketConnection.NetworkStream == null)
            {
                throw new NullReferenceException(nameof(_socketConnection.NetworkStream));
            }
            var sw = _socketConnection.NetworkStream;
            var bs = data.ToArray();
            var cid = DateTime.Now.Second;
            var message = new TcpMessage
            {
                Data = bs,
                ConnectionId = cid
            };
            var j = message.ToJson();
            bs = Encoding.ASCII.GetBytes(j+"\n");
            await sw.WriteAsync(bs, 0, bs.Length);
            await sw.FlushAsync();

            var result =  await Task.Run(() =>
            {
                return TakeMessage(cid);
            });

            return result.Data;
        }

        private TcpMessage TakeMessage(int connectionId)
        {
            foreach (var i in _col.GetConsumingEnumerable())
            {
                if (i.ConnectionId == connectionId)
                {
                    return i;
                }
                _col.Add(i);
            }
            return null;
        }

        private async Task Revice()
        {
            try
            {
                var reader = PipeReader.Create(_socketConnection.NetworkStream);
                while (true)
                {
                    ReadResult result = await reader.ReadAsync();
                    ReadOnlySequence<byte> buffer = result.Buffer;

                    while (TryReadLine(ref buffer, out ReadOnlySequence<byte> line))
                    {
                        // Process the line.
                        var str = Encoding.ASCII.GetString(line.ToArray());
                        _col.Add(await str.ToObjectAsync<TcpMessage>());
                    }

                    // Tell the PipeReader how much of the buffer has been consumed.
                    reader.AdvanceTo(buffer.Start, buffer.End);

                    // Stop reading if there's no more data coming.
                    if (result.IsCompleted)
                    {
                        break;
                    }
                }

                // Mark the PipeReader as complete.
                await reader.CompleteAsync();
            }
            catch(System.Exception ex)
            {
                //服务端关闭触发异常
                Console.WriteLine(ex.Message);
            }
        }

        private static bool TryReadLine(ref ReadOnlySequence<byte> buffer, out ReadOnlySequence<byte> line)
        {
            // Look for a EOL in the buffer.
            SequencePosition? position = buffer.PositionOf((byte)'\n');

            if (position == null)
            {
                line = default;
                return false;
            }

            // Skip the line + the \n.
            line = buffer.Slice(0, position.Value);
            buffer = buffer.Slice(buffer.GetPosition(1, position.Value));
            return true;
        }

        private class TcpMessage
        {
            public int ConnectionId { get; set; }

            public byte[] Data { get; set; }
        }
    }
}
