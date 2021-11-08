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
        private TcpClient client;
        private readonly TcpClient server = new();
        private readonly Сonfiguration Сonfiguration;
        private readonly ILogger<TcpProxy> Log;

        public TcpProxy(IConfiguration configuration, ILogger<TcpProxy> log)
        {
            Сonfiguration = configuration.Get<Сonfiguration>();
            Log = log;
        }
        public TcpProxy(Сonfiguration configuration, ILogger<TcpProxy> log)
        {
            Сonfiguration = configuration;
            Log = log;
        }
        public void Start()
        {
            Log.LogInformation($"Start service Proxy");
            var listner = new TcpListener(IPAddress.Parse(Сonfiguration.IPAddress), Сonfiguration.Port);
            listner.Start();
            while (true)
                new TcpProxy(Сonfiguration, Log).Start(listner.AcceptTcpClient()).Wait();
        }
        private async Task Start(TcpClient Client)
        {

            client = Client;
            Log.LogInformation($"Connect client IP:{((IPEndPoint)client.Client.RemoteEndPoint).Address}");

            //Псевдо Балансировщик нагрузки
            Server s;
            s = Сonfiguration.Servers.OrderBy(x => x.CountrediRection).First();

            //Переадресация данных
            Log.LogInformation($"Client IP:{((IPEndPoint)client.Client.RemoteEndPoint).Address} redirect to IP:{s.IPAdress} Port{s.Port}");
            await Redirect(s.IPAdress, s.Port);
            s.CountrediRection = ++s.CountrediRection;
        }
        private async Task Redirect(string address, int port)
        {
            await server.ConnectAsync(address, port);
            Task T1 = Task.Factory.StartNew(async () => { await RedirectTask(client, server); });
            Task T2 = Task.Factory.StartNew(async () => { await RedirectTask(server, client); });
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
                ($"IP:{((IPEndPoint)target.Client.RemoteEndPoint).Address}" +
                $" Port:{((IPEndPoint)target.Client.RemoteEndPoint).Port}" +
                $" send to IP:{((IPEndPoint)destination.Client.RemoteEndPoint).Address}" +
                $" Port:{((IPEndPoint)destination.Client.RemoteEndPoint).Port} bytes:{ByteTotal}");
        }
    }
}