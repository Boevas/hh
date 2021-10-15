using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HtmlAgilityPack;

namespace SimbirSoftTest.Classes
{
    class HTMLParsingData : IParsingData
    {
        readonly private string URL;

        public HTMLParsingData(string URL)
        {
            this.URL = URL ?? throw new ArgumentNullException(nameof(URL), "Задайте URL страницы");
        }

        public string GetData()
        {
            return new HtmlWeb().Load(URL).DocumentNode.InnerText;
        }
    }
}
