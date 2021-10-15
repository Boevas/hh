using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimbirSoftTest.Classes
{
    public class Loger: ILoger
    {
        private readonly StreamWriter sw;
        
        public Loger()
        {
            sw = new StreamWriter("Loger.log", true, System.Text.Encoding.Default);
        }

        public Loger(string FilePath, Encoding encoding)
        {
            if (null == FilePath)
                throw new ArgumentNullException("Задайте имя файла логирования", nameof(FilePath));

            if (null == encoding)
                throw new ArgumentNullException("Задайте кодировку", nameof(encoding));

            sw = new StreamWriter(FilePath, true, encoding);
        }

        public void ToLog(Exception ex)
        {
            sw.WriteLine($"DateTime: {DateTime.Now}");
            sw.WriteLine($"Message: {ex.Message}");
            sw.WriteLine($"Source: {ex.Source}");
            sw.WriteLine($"Stack Trace: {ex.StackTrace}");
            sw.WriteLine("___________________________________");
        }


        #region IDisposable

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (true == disposing)
                sw?.Dispose();
        }

        #endregion IDisposable
    }
}
