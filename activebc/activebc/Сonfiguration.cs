using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace activebc
{
    public class Сonfiguration
    {
        public int Port { get; set; }

        public string IPAddress { get; set; }

        public ICollection<Server> Servers { get; set; }
    }
}
