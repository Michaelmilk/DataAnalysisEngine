using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Reflection;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using Newtonsoft.Json;

namespace TechTest
{

    public class EntityDistribution
    {
        public long PropertyCount { get; set; }
        public long EntityCount { get; set; }
        public List<string> ExternalIdSamples { get; set; }
    }

    class Program
    {
        static void Main(string[] args)
        {
            //ExtractFunctionFromDll.ExtractFunctionsFromDll();
            //ExtractFunctionFromDll.ExtractSubBetweenSpecificString();
            //MethodList.CombineMethods();

            //Dictionary<string, string> deprecatedProperty = new Dictionary<string, string>()
            //{
            //    {"sss", "dsf" },
            //    {"aaa1", "Bbb" },
            //    {"2222", "111" }
            //};
            //Dictionary<string, bool> deprecatedPropertyDict = new Dictionary<string, bool>()
            //{
            //    {"sss", false },
            //    //{"aaa", false },
            //    //{"222", false }
            //};
            //var existingDeprecatedProperty = deprecatedPropertyDict.Keys.SingleOrDefault(t => deprecatedProperty[t] == "dsf");
            //Console.WriteLine(existingDeprecatedProperty);

            //List<int> list = new List<int>() {1,2,3,3,4,5,5,6,6};
            //var distinct = list.Distinct().ToList();
            //var exs = list.Except(distinct).ToList();
            //exs.ForEach(Console.WriteLine);

            //typeof(EntityDistribution).GetProperties().ToList().ForEach(t => Console.WriteLine(t.Name));

            //PropertyInfo[] myPropertyInfo;
            //// Get the properties of 'Type' class object.
            //myPropertyInfo = typeof(EntityDistribution).GetProperties();
            //Console.WriteLine("Properties of System.Type are:");
            //for (int i = 0; i < myPropertyInfo.Length; i++)
            //{
            //    Console.WriteLine(myPropertyInfo[i].Name.ToString());
            //}

            //string destinationURL = "http://www.contoso.com/default.aspx?user=test";

            //Console.WriteLine(HttpUtility.UrlEncode(destinationURL));

            //CancelTask.CancelTaskByTime();


            RegexTest.ExtractComosNum();
        }
    }
}
