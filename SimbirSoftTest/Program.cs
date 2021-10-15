using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using SimbirSoftTest.Classes;

namespace SimbirSoftTest
{
    class Program
    {
        static void Main()
        {
            using ILoger log = new Loger();
            IView ui = new ConsoleView();
            while (true)
            {
                ui.WriteLine("Для выхода введите exit");
                ui.Write("Введите адрес сайта, с которым желаете продолжить работу: ");

                var url = ui.ReadLine();
                if (url.ToLower() == "exit")
                    return;


                //https://www.simbirsoft.com/
                IParsingData ParsingData = new HTMLParsingData(url);
                try
                {
                    string text = ParsingData.GetData();
                    IStringParser ISP = new StringParser(text, new char[] { ' ', ',', '.', '!', '?', '"', ';', ':', '[', ']', '(', ')', '\n', '\r', '\t' });

                    Dictionary<string, int> res = ISP.GetReport();
                    foreach (KeyValuePair<string, int> str in res.OrderBy(x => x.Value))
                        ui.WriteLine($"{str.Key}: {str.Value}");
                }
                catch (UriFormatException ex)
                {
                    ui.WriteLine("Невозможно получить текст по данному URL. Проверьте правильность URL.");
                    log.ToLog(ex);
                }
                catch (ArgumentNullException ex)
                {
                    ui.WriteLine(ex.Message);
                    log.ToLog(ex);
                }
                catch (Exception ex)
                {
                    ui.WriteLine(ex.Message);
                    log.ToLog(ex);
                }
            }
        }
    }
}