using System;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Threading.Tasks;
using System.Linq;
using System.Net;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System.IO;
using System.Buffers;
using System.IO.Pipelines;
using System.Text;

namespace ServerPipeline
{
    public class Pipeline
    {
        private readonly Сonfiguration Сonfiguration;
        private readonly ILogger<Pipeline> Log;

        public Pipeline(IConfiguration configuration, ILogger<Pipeline> log)
        {
            Сonfiguration = configuration.Get<Сonfiguration>();
            Log = log;
        }
        public void Start()
        {
            try
            {
                Log.LogInformation($"Start service Pipeline");
                Log.LogInformation($"Listening IP:{Сonfiguration.IPAddress} on port {Сonfiguration.Port}");

                var listenSocket = new Socket(SocketType.Stream, ProtocolType.Tcp);
                listenSocket.Bind(new IPEndPoint(IPAddress.Parse(Сonfiguration.IPAddress), Сonfiguration.Port));
                listenSocket.Listen(Сonfiguration.SocketListenbacklog);
                while (true)
                    ProcessLinesAsync(listenSocket.Accept()).Wait();
            }
            catch (Exception ex)
            {
                Log.LogError(ex, ex.Message);
            }
        }
        private async Task ProcessLinesAsync(Socket socket)
        {
            try
            { 
                Log.LogInformation($"[{socket.RemoteEndPoint}]: connected");

                var stream = new NetworkStream(socket);
                var reader = PipeReader.Create(stream);

                while (true)
                {
                    ReadResult result = await reader.ReadAsync();
                    ReadOnlySequence<byte> buffer = result.Buffer;

                    while (TryReadLine(ref buffer, out ReadOnlySequence<byte> line))
                        ProcessLine(line);

                    reader.AdvanceTo(buffer.Start, buffer.End);

                    if (result.IsCompleted)
                        break;
                }

                await reader.CompleteAsync();

                Log.LogInformation($"[{socket.RemoteEndPoint}]: disconnected");
            }
            catch (Exception ex)
            {
                Log.LogError(ex, ex.Message);
            }
        }

        private bool TryReadLine(ref ReadOnlySequence<byte> buffer, out ReadOnlySequence<byte> line)
        {
            try
            { 
                SequencePosition? position = buffer.PositionOf((byte)'\n');

                if (position == null)
                {
                    line = default;
                    return false;
                }

                line = buffer.Slice(0, position.Value);
                buffer = buffer.Slice(buffer.GetPosition(1, position.Value));
                return true;
            }
            catch (Exception ex)
            {
                Log.LogError(ex, ex.Message);
                line = default;
                return false;
            }
        }

        private void ProcessLine(in ReadOnlySequence<byte> buffer)
        {
            try
            { 
                foreach (var segment in buffer)
                    Console.Write(Encoding.UTF8.GetString(segment.Span));

                Console.WriteLine();
            }
            catch (Exception ex)
            {
                Log.LogError(ex, ex.Message);
            }
        }
    }
}