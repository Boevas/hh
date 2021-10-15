using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimbirSoftTest.Classes
{
    public interface IStringParser
    {
        Dictionary<string, int> GetReport();
    }
}