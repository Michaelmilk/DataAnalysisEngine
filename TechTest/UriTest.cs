using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechTest
{
    public class UriTest
    {
        public static void TestUri()
        {
            var uri = new Uri("https://ekgstorage.blob.core.windows.net/deletion-test/180319/sql.csv?st=2018-03-16T04%3A26%3A00Z&se=2018-03-17T04%3A26%3A00Z&sp=rwdl&sv=2017-04-17&sr=b&sig=J%2Behgv9CzJfkpY%2B9Ccjol%2BhyTJH2e8CYYeUBalb1oeY%3D");
            Console.WriteLine(uri.AbsolutePath);
            Console.WriteLine(uri.AbsoluteUri);
            Console.WriteLine(uri.Fragment);
            Console.WriteLine(uri.LocalPath);
            Console.WriteLine(uri.OriginalString);
            Console.WriteLine(uri.PathAndQuery);
            Console.WriteLine(uri.Query);
            Console.WriteLine(uri.Segments);
            Console.WriteLine(uri.UserEscaped);
            Console.WriteLine(uri.UserInfo);

            var param = uri.AbsolutePath.Split('/').ToList();
            param.ForEach(t => Console.WriteLine(t));
        }
        
    }
}
