using System;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Threading.Tasks;
using System.Linq;
using System.Net;
using Microsoft.Extensions.Configuration;

namespace activebc
{
    public class TcpProxy
    {
        private TcpClient Client;
        private readonly TcpClient Server = new();
        private readonly Сonfiguration Сonfiguration;

        public TcpProxy(IConfiguration configuration)
        {
            Сonfiguration = configuration.Get<Сonfiguration>();
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

                    //Псевдо Балансировщик нагрузки
                    Server s;
                    lock ("{F4C3660E-2FAF-43D8-A9A7-EEE34FAFB648}")
                    {
                        s = Сonfiguration.Servers.OrderBy(x => x.CountrediRection).First();
                    }

                    //Переадресация данных
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
            int bytesRead;
            byte[] recvbuf = new byte[8192];
            do
            {
                bytesRead = await target.GetStream().ReadAsync(recvbuf.AsMemory(0, recvbuf.Length));
                await destination.GetStream().WriteAsync(recvbuf.AsMemory(0, bytesRead));
            } while (bytesRead != 0);
        }
    }
}