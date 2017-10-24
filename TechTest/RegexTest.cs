using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Maps.DataPlatform.DataServices.Metadata;

namespace TechTest
{
    public class RegexTest
    {
        public static void ExtractComosNum()
        {
            string vc = "https://cosmos08.osdinfra.net/cosmos/Knowledge/";
            var param = vc.Split('/').ToList();
            param.ForEach(Console.WriteLine);
            int cnt = param.Count;
            int pos = param[2].IndexOf('.');
            var cosmos = param[2].Substring(0, pos);
            Console.WriteLine($"{cosmos}/{param[cnt - 2]}");
        }
    }
}
