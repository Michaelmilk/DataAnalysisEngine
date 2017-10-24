using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Cache;
using System.Text;
using System.Threading.Tasks;

namespace WebTechTest
{
    class Program
    {
        static void Main(string[] args)
        {
            //MappingFileValidation.ValidateMappingFile();
            HashSet<int> h = new HashSet<int>() {1,2,3,5,6};
            var list = h.ToList();
            list.ForEach(Console.WriteLine);
        }
    }
}
