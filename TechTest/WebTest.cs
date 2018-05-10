using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace TechTest
{
    public class WebTest
    {
        public static void ModelVersionParse()
        {
            using (HttpClient client = new HttpClient())
            {
                var response = client.GetAsync("https://wrapstar.bing.net/Model/Detail/66037?environment=WrapStar").Result;
                Console.WriteLine(response);
                Console.WriteLine(response.Content.ReadAsStringAsync().Result);
            }
        }
    }
}
