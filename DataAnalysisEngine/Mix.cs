using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

namespace DataAnalysisEngine
{
    public class Mix
    {
        public static void ReplaceViewNameWithPlaceholder()
        {
            string predix = "http://knowledge.microsoft.com/";
            string s = @"http://knowledge.microsoft.com/Douban_People_Film/language_human_language/Estonian";
            var t = s.Substring(predix.Length);
            var pos = t.IndexOf(@"/");
            var replaced = t.Substring(0, pos);
            s = s.Replace(replaced, "placeholder");
            Console.WriteLine(s);
        }

        public static bool IsMatchType(object obj, Type type)
        {
            return obj.GetType() == type;
        }

        public static void JsonPathSample()
        {
            JObject o = JObject.Parse(@"{
                    'Stores': [
                      'Lambton Quay',
                      'Willis Street'
                    ],
                    'Manufacturers': [
                      {
                        'Name': 'Acme Co',
                        'Products': [
                          {
                            'Name': 'Anvil',
                            'Price': 50
                          }
                        ]
                      },
                      {
                        'Name': 'Contoso',
                        'Products': [
                          {
                            'Name': 'Elbow Grease',
                            'Price': 99.95
                          },
                          {
                            'Name': 'Headlight Fluid',
                            'Price': 4
                          }
                        ]
                      }
                    ]
                }");

            // manufacturer with the name 'Acme Co'
            JToken acme = o.SelectToken("$.Manufacturers[?(@.Name == 'Acme Co')]");
            
            Console.WriteLine(acme);
            // { "Name": "Acme Co", Products: [{ "Name": "Anvil", "Price": 50 }] }

            // name of all products priced 50 and above
            //IEnumerable<JToken> pricyProducts = o.SelectToken("$..Products[?(@.Price >= 50)].Name");

            //foreach(JToken item in pricyProducts)
            //{
            //    Console.WriteLine(item);
            //}
            // Anvil
            // Elbow Grease
        }

        public static void ParseJsonObject()
        {
            string line = string.Empty;
            using (var rd = new StreamReader(@".\sampleJson"))
            {
                line = rd.ReadLine();
            }

            var data = JObject.Parse(line);
            //Console.WriteLine(data.nodeKey.Events.GetType());
            //foreach (var e in data.nodeKey.Events)
            //{
            //    Console.WriteLine(e.Name);
            //}

            //Console.WriteLine(data.nodeKey.ParentKey);


            Console.WriteLine("-------------------------");

            var token = data.SelectToken("nodeKey.Events");
            Console.WriteLine(token.GetType());
            if (token is JArray)
            {
                Console.WriteLine("is Jarray");
                //var eventNames = data.SelectToken("$.Events[?(@.ToBeAnnounced == 'false')].Name");
                //var eventName = data.SelectToken("$.Events[0].Name");
                //Console.WriteLine(eventNames.ToString());
                var token2 = data.SelectTokens("nodeKey.Events[*].Categories").First();
                Console.WriteLine(token2.GetType());
                var eventNames = data.SelectTokens("nodeKey.Events[*].Categories[*]").ToList();
                foreach(var eventName in eventNames)
                {
                    Console.WriteLine(eventName);
                }
            }

            //Console.WriteLine(token.Count);
            //Console.WriteLine(token.ToString());

            Console.WriteLine("-----------Events--------------");

            var token1 = data.SelectToken("nodeKey.__SubjectId__");
            Console.WriteLine(token1.Type);
            if (token1 != null)
            {
                Console.WriteLine(token1.ToString());
            }
            else
            {
                Console.WriteLine("ttt");
            }
        }

        public static void NewTonJsonSample()
        {
            JObject o = JObject.Parse(@"{
              'Stores': [
                'Lambton Quay',
                'Willis Street'
              ],
              'Manufacturers': [
                {
                  'Name': 'Acme Co',
                  'Products': [
                    {
                      'Name': 'Anvil',
                      'Price': 50
                    }
                  ]
                },
                {
                  'Name': 'Contoso',
                  'Products': [
                    {
                      'Name': 'Elbow Grease',
                      'Price': 99.95
                    },
                    {
                      'Name': 'Headlight Fluid',
                      'Price': 4
                    }
                  ]
                }
              ]
            }");
                    
            string[] storeNames = o.SelectToken("Stores").Select(s => (string)s).ToArray();
                      
            Console.WriteLine(string.Join(", ", storeNames));
            // Lambton Quay, Willis Street

            //string[] productNames = o["Manufacturers.Products"].Select(m => m.Cast<JProperty>()).Select(t => t.n)
            //.Where(n => n != null).ToArray();
            //Console.WriteLine(string.Join(", ", productNames));

            string[] firstProductNames = o["Manufacturers"].Select(m => (string)m.SelectToken("Products[1].Name"))
            .Where(n => n != null).ToArray();
                     
            Console.WriteLine(string.Join(", ", firstProductNames));
                      // Headlight Fluid
            
            decimal totalPrice = o["Manufacturers"].Sum(m => (decimal)m.SelectToken("Products[0].Price"));
                      
            Console.WriteLine(totalPrice);
                      // 149.95
        }
    }
}
