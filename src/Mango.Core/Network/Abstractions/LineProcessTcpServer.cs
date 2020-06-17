using Mango.Core.Log;
using Microsoft.Extensions.Logging;
using System;
using System.Buffers;
using System.Collections.Generic;
using System.IO.Pipelines;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Mango.Core.Network.Abstractions
{
    /// <summary>
    /// 以行（\n）为单位处理的TCP服务端
    /// </summary>
    public abstract class LineProcessTcpServer : IMangoTcpServer
    {
        private readonly ILogger<LineProcessTcpServer> _logger;

        public LineProcessTcpServer(ILogger<LineProcessTcpServer> logger)
        {
            if(logger == null)
            {
                _logger = LoggerHelper.Create<LineProcessTcpServer>();
            }
            _logger = logger;
        }

        public async Task Start(IPAddress address, int port)
        {
            var listenSocket = new Socket(SocketType.Stream, ProtocolType.Tcp);
            listenSocket.Bind(new IPEndPoint(address, port));

            _logger.LogInformation($"start listen to {address}:{port}");

            listenSocket.Listen(120);

            while (true)
            {
                var socket = await listenSocket.AcceptAsync();
                _ = ProcessLinesAsync(socket);
            }
        }

        public abstract Task Handle(ReadOnlyMemory<byte> data, NetworkStream stream);

        #region 辅助函数

        /// <summary>
        /// 接收并处理单个TCP连接
        /// </summary>
        /// <param name="socket"></param>
        /// <returns></returns>
        private async Task ProcessLinesAsync(Socket socket)
        {
            _logger.LogInformation($"[{socket.RemoteEndPoint}]: connected");

            // Create a PipeReader over the network stream
            var stream = new NetworkStream(socket);
            var reader = PipeReader.Create(stream);

            while (true)
            {
                ReadResult result = await reader.ReadAsync();
                ReadOnlySequence<byte> buffer = result.Buffer;

                while (TryReadLine(ref buffer, out ReadOnlySequence<byte> line))
                {
                    // Process the line.
                    ReadOnlyMemory<byte> memory = line.ToArray();
                    _ = Task.Run(() =>
                      {
                          Handle(memory, stream);
                      });
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

            _logger.LogInformation($"[{socket.RemoteEndPoint}]: disconnected");
        }

        /// <summary>
        /// 提取单行数据
        /// </summary>
        /// <param name="buffer"></param>
        /// <param name="line"></param>
        /// <returns></returns>
        private bool TryReadLine(ref ReadOnlySequence<byte> buffer, out ReadOnlySequence<byte> line)
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

        #endregion
    }
}
