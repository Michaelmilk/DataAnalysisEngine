using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechTest
{
    public static class MethodList
    {
        public static void CombineMethods()
        {
            List<Action> validationActions = new List<Action>()
            {
                () => Check1("SSDFSD"),
                () => Check2("AACVVV")
            };

            foreach (var va in validationActions)
            {
                va();
            }
            //var p = validationActions.Select(t => t()).ToList();
            List<Func<string, string>> validationFuncs = new List<Func<string, string>>()
            {
                MethodList.Check1,
                MethodList.Check2
            };

            foreach (var va in validationFuncs)
            {
                Console.WriteLine(va);
                Console.WriteLine(va.Method.Name);
                va.Method.Name.IndexOf("dd", StringComparison.OrdinalIgnoreCase);
                va("dss");
            }
        }

        public static string Check1(string s)
        {
            Console.WriteLine($"{s} first output");
            return s;
        }

        public static string Check2(string s)
        {
            Console.WriteLine($"{s} second output");
            return s;
        }
    }
}
