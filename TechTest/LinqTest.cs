using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechTest
{
    public class LinqTest
    {
        public static void EmptyListFirstOrDefault()
        {
            var list = new List<string>();
            var t = list.FirstOrDefault();
            Console.WriteLine(t);
        }
    }
}
