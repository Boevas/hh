using System;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Threading.Tasks;
using System.Linq;
using System.Net;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System.IO;

namespace activebc
{
    public class TcpProxy
    {
        private TcpClient client;
        private TcpClient server;
        private readonly Сonfiguration Сonfiguration;
        private readonly ILogger<TcpProxy> Log;

        public TcpProxy(IConfiguration configuration, ILogger<TcpProxy> log)
        {
            Сonfiguration = configuration.Get<Сonfiguration>();
            Log = log;
        }
        public void Start()
        {
            try
            {
                Log.LogInformation($"Start service Proxy");
                var listner = new TcpListener(IPAddress.Parse(Сonfiguration.IPAddress), Сonfiguration.Port);
                listner.Start();
                while (true)
                    Start(listner.AcceptTcpClient()).Wait();
            }
            catch (Exception ex)
            {
                Log.LogError(ex, ex.Message);
            }
        }
        private async Task Start(TcpClient Client)
        {
            try
            { 
                client = Client;
                Log.LogInformation($"Connect client IP:{((IPEndPoint)client?.Client?.RemoteEndPoint)?.Address}");

                //Псевдо Балансировщик нагрузки
                Server s = Сonfiguration.Servers.OrderBy(x => x.CountrediRection).First();

                //Переадресация данных
                Log.LogInformation($"Client IP:{((IPEndPoint)client?.Client?.RemoteEndPoint)?.Address} redirect to IP:{s.IPAdress} Port{s.Port}");
                await Redirect(s);
                s.CountrediRection = ++s.CountrediRection;
            }
            catch (Exception ex)
            {
                Log.LogError(ex, ex.Message);
            }
        }
        private async Task Redirect(Server s)
        {
            try
            { 
                server = new();
                await server.ConnectAsync(s.IPAdress, s.Port);
                Task T1 = Task.Factory.StartNew(async () => { await RedirectTask(client, server); });
                Task T2 = Task.Factory.StartNew(async () => { await RedirectTask(server, client); });
                Task.WaitAll(T1, T2);
            }
            catch (Exception ex)
            {
                Log.LogError(ex, ex.Message);
            }
        }

        private async Task RedirectTask(TcpClient target, TcpClient destination)
        {
            try
            {
                int ByteTotal = 0;
                int bytesRead;
                byte[] recvbuf = new byte[8192];
                do
                {
                    bytesRead = await target.GetStream().ReadAsync(recvbuf.AsMemory(0, recvbuf.Length));
                    await destination.GetStream().WriteAsync(recvbuf.AsMemory(0, bytesRead));
                    ByteTotal += bytesRead;
                } while (0 != bytesRead);

                Log.LogInformation
                    ($"IP:{((IPEndPoint)target?.Client?.RemoteEndPoint)?.Address}" +
                    $" Port:{((IPEndPoint)target?.Client?.RemoteEndPoint)?.Port}" +
                    $" send to IP:{((IPEndPoint)destination?.Client?.RemoteEndPoint)?.Address}" +
                    $" Port:{((IPEndPoint)destination?.Client?.RemoteEndPoint)?.Port} bytes:{ByteTotal}");
            }
            catch (IOException ex)
            {
                Log.LogWarning(ex, ex.Message);
            }
            catch (Exception ex)
            {
                Log.LogError(ex, ex.Message);
            }
        }
    }
}