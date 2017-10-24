using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace WebTechTest
{
    public class MappingFileValidation
    {
        public static void ValidateMappingFile()
        {
            //var fileBytes = File.ReadAllBytes(@"D:\data\onebox\1\MappingStage\MappingFile\networths_org_mapping.xml");
            var fileBytes = File.ReadAllBytes(@"D:\work\onebox\mapping\networths_org_mapping - Copy.xml");
            HttpContent bytesContent = new ByteArrayContent(fileBytes);
            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/xml"));
                HttpResponseMessage responseMessage = null;
                try
                {
                    responseMessage =
                        client.PostAsync(
                            "http://entityrepository.binginternal.com/MapRepository/MapDefinition/ValidateMap",
                            bytesContent).Result;
                }
                catch (Exception ex)
                {
                    Console.WriteLine("exception");
                    Console.WriteLine(ex.Message);
                    Console.WriteLine(responseMessage.Content.ReadAsStringAsync());
                }
                //responseMessage =
                //        await client.PostAsync(
                //            "http://entityrepository.binginternal.com/MapRepository/MapDefinition/ValidateMap",
                //            bytesContent);
                //Console.WriteLine("asdfasedf");
                if (!responseMessage.IsSuccessStatusCode)
                {
                    Console.WriteLine("error");
                    Console.WriteLine(responseMessage);
                    Console.WriteLine(responseMessage.Content.ReadAsStringAsync().Result);

                    return;
                }
                Console.WriteLine(responseMessage.Content.ReadAsStreamAsync().Result);

                Console.ReadKey();
                Console.ReadKey();
            }
        }
    }
}
