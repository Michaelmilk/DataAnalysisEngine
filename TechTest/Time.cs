using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechTest
{
    public class Time
    {
        public static void DateTimeTest()
        {
            Console.WriteLine(DateTime.UtcNow);
            Console.WriteLine(DateTime.UtcNow.Date);
            Console.WriteLine(DateTime.Today);
            Console.WriteLine(DateTime.UtcNow.ToShortDateString());
            Console.WriteLine(DateTime.UtcNow.ToShortTimeString());
            Console.WriteLine(DateTime.UtcNow.ToUniversalTime());
            Console.WriteLine(DateTime.UtcNow.ToFileTime());
            Console.WriteLine(DateTime.UtcNow.ToLongDateString());
            Console.WriteLine(DateTime.Now.ToString("yyyyMMddTHHmmss"));
            //2018 - 01 - 30 15:57:00.947
            string timer1 = "1/31/2018";
            string timer2 = "2018-01-30 15:57:00.947";
            DateTime datetime1;
            DateTime datetime2;
            DateTime.TryParse(timer1, out datetime1);
            DateTime.TryParse(timer2, out datetime2);
            var timespan = datetime1 - datetime2;

            Console.WriteLine(timespan.Days);
            Console.WriteLine(timespan.Hours);
        }
    }
}
