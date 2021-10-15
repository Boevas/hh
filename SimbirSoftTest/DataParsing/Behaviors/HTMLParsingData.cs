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
            if (URL == null)
                throw new ArgumentNullException("Задайте URL страницы", nameof(URL));

            this.URL = URL;
        }

        public string GetData()
        {
            return new HtmlWeb().Load(URL).DocumentNode.InnerText;
        }
    }
}
