using System;
using System.Net.Sockets;
using System.Threading.Tasks;

namespace activebc
{
    public class Server
    {
        public string IPAdress{ get; set; }
        public ushort Port { get; set; }
        public int CountrediRection { get; set; }
    }
}