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
            QueryJobStatus();
        }

        public static void QueryJobStatus()
        {
            // Gets all JobInfo entries using the "JobInfo" on RestQuery controller
            const string RestActionPath = "http://sangam-prod0.playmsn.com:88/JobManagerPortal/Prod0/Sangam-Int/RestApi/RestQuery?query=JobInfo";

            var request = (HttpWebRequest)WebRequest.Create(RestActionPath);

            request.Method = "GET";

            request.Credentials = new NetworkCredential("jixge", "", "fareast");

            // - OR -

            // request.Credentials = CredentialCache.DefaultNetworkCredentials;

            request.PreAuthenticate = true;

            request.Timeout = 150000;

            request.CachePolicy = new RequestCachePolicy(RequestCacheLevel.BypassCache);

            string output = string.Empty;

            try

            {

                using (var response = request.GetResponse())

                {

                    using (var stream = new StreamReader(response.GetResponseStream(), Encoding.GetEncoding(1252)))

                    {

                        output = stream.ReadToEnd();

                    }

                }

            }

            catch (WebException ex)

            {

                if (ex.Status == WebExceptionStatus.ProtocolError)

                {

                    using (var stream = new StreamReader(ex.Response.GetResponseStream()))

                    {

                        output = stream.ReadToEnd();

                    }

                }

                else if (ex.Status == WebExceptionStatus.Timeout)

                {

                    output = "Request timeout is expired.";

                }

            }

            Console.WriteLine(output);

            Console.ReadLine();

        }
    }
}
