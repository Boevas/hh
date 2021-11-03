using System;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Threading.Tasks;
using System.Linq;
using System.Net;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace activebc
{
    public class TcpProxy
    {
        private TcpClient Client;
        private readonly TcpClient Server = new();
        private readonly Сonfiguration Сonfiguration;
        private readonly ILogger Log;

        public TcpProxy(IConfiguration configuration, ILogger log)
        {
            Сonfiguration = configuration.Get<Сonfiguration>();
            Log = log;
        }

        async public Task Start()
        {
            var listner = new TcpListener(IPAddress.Parse(Сonfiguration.IPAddress), Сonfiguration.Port);
            while (true)
            {
                try
                {
                    listner.Start();
                    Client = await listner.AcceptTcpClientAsync();
                    Log.LogInformation($"Connect client IP:{((IPEndPoint)Client.Client.RemoteEndPoint).Address}");

                    //Псевдо Балансировщик нагрузки
                    Server s;
                    lock ("{F4C3660E-2FAF-43D8-A9A7-EEE34FAFB648}")
                    {
                        s = Сonfiguration.Servers.OrderBy(x => x.CountrediRection).First();
                    }

                    //Переадресация данных
                    Log.LogInformation($"Client IP:{((IPEndPoint)Client.Client.RemoteEndPoint).Address} redirect to IP:{s.IPAdress} Port{s.Port}");
                    await Redirect(s.IPAdress, s.Port);

                    lock ("{F4C3660E-2FAF-43D8-A9A7-EEE34FAFB648}")
                    {
                        s.CountrediRection = ++s.CountrediRection;
                    }
                }
                finally
                {
                    listner.Stop();
                }
            }
        }

        private async Task Redirect(string address, int port)
        {
            await Server.ConnectAsync(address, port);
            Task T1 = Task.Factory.StartNew(async () => { await RedirectTask(Client, Server); });
            Task T2 = Task.Factory.StartNew(async () => { await RedirectTask(Server, Client); });
        }

        private async Task RedirectTask(TcpClient target, TcpClient destination)
        {
            int ByteTotal=0;
            int bytesRead;
            byte[] recvbuf = new byte[8192];
            do
            {
                bytesRead = await target.GetStream().ReadAsync(recvbuf.AsMemory(0, recvbuf.Length));
                await destination.GetStream().WriteAsync(recvbuf.AsMemory(0, bytesRead));
                ByteTotal += bytesRead;
            } while (bytesRead != 0);

            Log.LogInformation
                ($"Client IP:{((IPEndPoint)target.Client.RemoteEndPoint).Address}" +
                $" send to Server IP:{((IPEndPoint)destination.Client.RemoteEndPoint).Address}" +
                $" and Port:{((IPEndPoint)destination.Client.RemoteEndPoint).Port} bytes:{ByteTotal}");
        }
    }
}