using System;
using System.Linq;
using System.Reflection;
using SimMetricsCore.API;
namespace ConsoleApp6
{
    
    class Program
    {
        static void Main(string[] args)
        {
            var subclassTypes = Assembly
           .GetAssembly(typeof(AbstractStringMetric))
           .GetTypes()
           .Where(t => t.IsSubclassOf(typeof(AbstractStringMetric)));

            string str1 = "добрый день";
            string str2 = "добрый чудесный весенний день";
            foreach (Type t in subclassTypes)
            {
                AbstractStringMetric instance = (AbstractStringMetric)Activator.CreateInstance(t);
                double res = instance.GetSimilarity(str1, str2);
                //if(res > 0.7 || res < 0.3)
                    Console.WriteLine($"{t.Name} :{res}");
            }
            

            Console.WriteLine("Hello World!");
        }
    }
}
