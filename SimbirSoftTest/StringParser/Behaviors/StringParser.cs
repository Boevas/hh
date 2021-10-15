
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimbirSoftTest.Classes
{
    public class StringParser: IStringParser
    {
        private readonly string Data;
        private readonly char[] Splitters;
        private readonly Dictionary<string, int> WordsCount = new();

        public StringParser(string Data, char[] Splitters)
        {
            this.Data = Data ?? throw new ArgumentNullException(nameof(Data), "Задайте данные Data");
            this.Splitters = Splitters ?? throw new ArgumentNullException(nameof(Splitters), "Задайте список разделителей");
        }

        public Dictionary<string, int> GetReport()
        {
            foreach (var word in Data.Split(Splitters).Where(x=>x.Length!=0))
            { 
                WordsCount.TryGetValue(word.ToUpper(), out var count);
                WordsCount[word.ToUpper()] = count + 1;
            }
            return WordsCount;
        }
    }
}
