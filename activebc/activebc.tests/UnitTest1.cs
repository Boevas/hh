using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.IO;
using System.Text;
using System.Text.Json;
using Xunit;

namespace activebc.tests
{
    public class UnitTest1
    {
        [Fact]
        public void Test1()
        {
            dynamic json = new ExpandoObject();
            json.Port = 1111;
            json.IPAddress = "127.0.0.1";

            dynamic server1 = new ExpandoObject();
            server1.IPAdress = "127.0.0.1";
            server1.Port = 123;

            dynamic server2 = new ExpandoObject();
            server2.IPAdress = "127.0.0.1";
            server2.Port = 456;

            dynamic server3 = new ExpandoObject();
            server3.IPAdress = "127.0.0.1";
            server3.Port = 789;

            json.Servers = new List<dynamic>() { server1, server2, server3 };
            string jsonString = JsonSerializer.Serialize(json);
        }
    }
}
