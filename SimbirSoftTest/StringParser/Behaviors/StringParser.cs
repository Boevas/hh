using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimbirSoftTest.Classes
{
    public class StringParser: IStringParser
    {
        private string Data;
        private char[] Splitters;
        private Dictionary<string, int> WordsCount = new Dictionary<string, int>();

        public StringParser(string Data, char[] Splitters)
        {
            if (null == Data)
                throw new ArgumentNullException("Задайте данные Data", nameof(Data));

            this.Data = Data;

            if (null == Splitters)
                throw new ArgumentNullException("Задайте список разделителей", nameof(Splitters));
            
            this.Splitters = Splitters;
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
