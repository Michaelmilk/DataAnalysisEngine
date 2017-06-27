using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechTest
{
    class Program
    {
        static void Main(string[] args)
        {
            //StopwatchTest();
            AnyTest();
        }

        public static void AnyTest()
        {
            List<string> existing = new List<string> { "a", "b", "c" };
            List<string> current = new List<string> { "aa" };
            if (existing.Any(current.Contains))
            {
                Console.WriteLine("in");
            }
            Console.WriteLine("out");
        }

        public static void StopwatchTest()
        {
            Stopwatch stopwatch = Stopwatch.StartNew();
            while (true)
            {
                if (stopwatch.ElapsedMilliseconds > 5000)
                {
                    stopwatch.Stop();
                    break;
                }
                Console.WriteLine(stopwatch.ElapsedMilliseconds);
            }
            Console.WriteLine("---------------");
            Console.WriteLine(stopwatch.ElapsedMilliseconds);
        }

        public static void ReplaceStringTest()
        {
            string s = "http://knowledge.microsoft.com/Sports/sports_team/98aa610362565b4b91b29b28071ccef7";
            Console.WriteLine(s.Replace("placeholder", "SportEnstarData"));
        }
    }
}
