﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace TechTest
{
    public class EntitySpaceUrlDetail
    {
        public string Environment { get; set; }
        public string CustomerId { get; set; }
        public string CustomerEnvironment { get; set; }
        public string EntitySpaceName { get; set; }
        public string Version { get; set; }
        public const string EntitySpaceProdUrl = "https://cosmos08.osdinfra.net/cosmos/Knowledge/local/EntitySpace-Prod/";
        public const string EntitySpaceIntUrl = "https://cosmos08.osdinfra.net/cosmos/Knowledge/local/EntitySpace-Int/";
        public EntitySpaceUrlDetail()
        {
        }

        public EntitySpaceUrlDetail(string environment, string customerId, string customerEnvironment,
            string entitySpaceName, string version)
        {
            this.Environment = environment;
            this.CustomerId = customerId;
            this.CustomerEnvironment = customerEnvironment;
            this.EntitySpaceName = entitySpaceName;
            this.Version = version;
        }

        public static EntitySpaceUrlDetail EntitySpaceUrlDetailParser(string entitySpaceUrl)
        {
            string urlPrefix = string.Empty;
            string environment = string.Empty;
            if (entitySpaceUrl.StartsWith(EntitySpaceProdUrl))
            {
                urlPrefix = EntitySpaceProdUrl;
                environment = "PROD";
            }
            else if (entitySpaceUrl.StartsWith(EntitySpaceIntUrl))
            {
                urlPrefix = EntitySpaceIntUrl;
                environment = "INT";
            }
            else
            {
                throw new ArgumentException(
                    $"EntitySpaceUrl must start with {EntitySpaceProdUrl} or {EntitySpaceIntUrl}");
            }

            var entitySpaceUrlWithoutPrefix = entitySpaceUrl.Substring(urlPrefix.Length);

            var urlParams = entitySpaceUrlWithoutPrefix.Split('/');

            return new EntitySpaceUrlDetail(environment, urlParams[0], urlParams[1], urlParams[3], urlParams[4]);
        }

        public Dictionary<string, string> ToDictionary()
        {
            return this.GetType()
                .GetProperties(BindingFlags.Instance | BindingFlags.Public)
                .ToDictionary(prop => prop.Name, prop => prop.GetValue(this, null).ToString());
        }
    }

    public enum JobType
    {
        Inconsistency = 0,
        Coverage = 1,
        DataAnalysis = 2
    };

    public enum JobTypeScheduledBySangam
    {
        Inconsistency = 0,
        Coverage = 1
    }
    public enum JobStatus
    {
        Waiting = 0,
        Running = 1,
        Succeed = 2,
        Failed = 3,
        TimeOut = 4,
        Canceled = 5,
        UnKnown = 6
    }

    class PetOwner
    {
        public string Name { get; set; }
        public List<String> Pets { get; set; }
    }

    public class SyntaxTest
    {
        public static void EnumTest()
        {
            var jobTypes = Enum.GetValues(typeof(JobType));
            foreach (JobType jobType in jobTypes)
            {
                Console.WriteLine(Enum.IsDefined(typeof(JobTypeScheduledBySangam), (int)jobType));
            }
        }

        public static void SelectManyEx1()
        {
            PetOwner[] petOwners =
                { new PetOwner { Name="Higa, Sidney",
              Pets = new List<string>{ "Scruffy", "Sam" } },
          new PetOwner { Name="Ashkenazi, Ronen",
              Pets = new List<string>{ "Walker", "Sugar" } },
          new PetOwner { Name="Price, Vernette",
              Pets = new List<string>{ "Scratches", "Diesel" } } ,
            new PetOwner { Name="gggg, fffff",
              Pets = new List<string>() }};

            // Query using SelectMany().
            IEnumerable<string> query1 = petOwners.SelectMany(petOwner => petOwner.Pets);

            Console.WriteLine("Using SelectMany():");

            // Only one foreach loop is required to iterate 
            // through the results since it is a
            // one-dimensional collection.
            foreach (string pet in query1)
            {
                Console.WriteLine(pet);
            }

            // This code shows how to use Select() 
            // instead of SelectMany().
            IEnumerable<List<String>> query2 =
                petOwners.Select(petOwner => petOwner.Pets);

            Console.WriteLine("\nUsing Select():");

            // Notice that two foreach loops are required to 
            // iterate through the results
            // because the query returns a collection of arrays.
            foreach (List<String> petList in query2)
            {
                foreach (string pet in petList)
                {
                    Console.WriteLine(pet);
                }
                Console.WriteLine();
            }
        }

        public static void TimeSpanTest()
        {
            DateTime t1 = DateTime.UtcNow;
            Thread.Sleep(5000);
            DateTime t2 = DateTime.UtcNow;


            Console.WriteLine(TimeSpan.FromTicks((t2 - t1).Ticks).TotalSeconds);
            Console.WriteLine((t2 - t1).TotalSeconds);
        }

        public static void IScopeFormat()
        {
            string scriptFormat = @"
                EntitySpaceBaseStream =
                    SSTREAM ""{0}"";

                Result = SELECT DISTINCT TOP {1}
                    ExternalId
                FROM EntitySpaceBaseStream
                WHERE JsonPath == ""{2}"" AND Value == ""{3}"";

                OUTPUT Result TO CONSOLE;";
            string value = "\"AWI\"";//"'''Michael";
            if (value.Contains("\"") || value.Contains("'"))
            {
                value = value.Replace("\"", "\\\"");
            }
            Console.WriteLine(value);
            string iScopeScript = string.Format(scriptFormat, "/local/EntitySpace-Prod/Satori/Sources/BaseStreamFolder/XingFeed/v0_2/Fact.Base.ss", 10,
                "Person.familyName", value);

            Console.WriteLine(iScopeScript);
        }

        public static void ExtractSubstr()
        {
            var s = "Submitting the job 13a578f2-b324-41d5-8278-5ab07ceec09c ...";
            int pos = s.IndexOf(" ...", StringComparison.Ordinal);
            int pos2 = s.IndexOf("job ", StringComparison.Ordinal);
            Console.WriteLine("{0} : {1}", pos, pos2);
            var subStr = s.Substring(0, pos);
            subStr = subStr.Replace("Submitting the job ", "");
            Console.WriteLine(subStr);
        }

        private static string ConvertDictToStringParam(Dictionary<string, string> parameters)
        {
            return parameters.Aggregate(string.Empty, (current, parameter) => current + (" -params " + parameter.Key + $"=\\\"{parameter.Value}\\\""));
        }

        public static string ToCosmosParam(string value)
        {
            return string.Format(@"@""{0}""", value);
        }

        public static string ConvertDictToStringParam()
        {
            var parameters = new Dictionary<string, string>
            {
                {"OutputPath", ToCosmosParam("/users/jixge/scopeTest/DataAnalysis/outlook_sports3/")},
                {"JobKind", ToCosmosParam("DataAnalysis")},
                {"RelativeEntitySpaceStreamPath", ToCosmosParam("/local/EntitySpace-Prod/Satori/Sources/BaseStreamFolder/Outlook_Sports/v0_1/Fact.Base.ss")}
            };
            var str = parameters.Aggregate(string.Empty, (current, parameter) => current + (" -params " + parameter.Key + $"=\\\"{parameter.Value}\\\""));
            var vc = "https://cosmos08.osdinfra.net/cosmos/Knowledge/";
            int priority = 1000;
            int vcPercentAllocation = 5;
            //string cmdParam = $"submit -i DataAnalysis.script –vc {vc} -p {priority} -vcp {vcPercentAllocation} {ConvertDictToStringParam()}";
            string cmdParam = $"submit -i DataAnalysis.script –vc {vc} -p {priority} -vcp {vcPercentAllocation} {ConvertDictToStringParam(parameters)}";
            //cmdParam = cmdParam.Replace("@\"")
            Console.WriteLine(cmdParam);
            Console.WriteLine(str);
            return str;
        }

        public static void StringToEnum()
        {
            string jobStatusStr = "Succeed";
            JobStatus jobStatus;
            Enum.TryParse<JobStatus>(jobStatusStr, true, out jobStatus);
            Console.WriteLine(jobStatus);
        }

        public static void DeserializeObjectAndCastType()
        {

        }

        public static void SerializeDictionary()
        {
            string url =
                "https://cosmos08.osdinfra.net/cosmos/Knowledge/local/EntitySpace-Prod/Satori/Sources/BaseStreamFolder/XingFeed/v0_2/Fact.Base.ss";

            var entitySpaceUrlDetail = EntitySpaceUrlDetail.EntitySpaceUrlDetailParser(url);
            var inParams = new Dictionary<string, EntitySpaceUrlDetail>()
            {
                {"EntitySpaceUrlDetail", entitySpaceUrlDetail}
            };

            var str = JsonConvert.SerializeObject(inParams);
            Console.WriteLine(str);
        }

        public static void ObjectTodictionary()
        {
            string url =
                "https://cosmos08.osdinfra.net/cosmos/Knowledge/local/EntitySpace-Prod/Satori/Sources/BaseStreamFolder/XingFeed/v0_2/Fact.Base.ss";

            var entitySpaceUrlDetail = EntitySpaceUrlDetail.EntitySpaceUrlDetailParser(url);
            var dict = entitySpaceUrlDetail.ToDictionary();
            var str = JsonConvert.SerializeObject(dict);
            Console.WriteLine(str);
            foreach (var key in dict)
            {
                Console.WriteLine("{0}:{1}", key.Key, key.Value);
            }
        }

        public static void UTCTimeTest()
        {
            string startDate = DateTime.UtcNow.Subtract(new TimeSpan(8, 0, 0, 0)).ToString("d");
            string startDate2 = DateTime.UtcNow.ToString("d");
            string startDate3 = DateTime.UtcNow.ToString();
            Console.WriteLine(startDate);
            Console.WriteLine(startDate2);
            Console.WriteLine(startDate3);
        }

        public static void EntitySpaceUrlParser()
        {
            string url =
                "https://cosmos08.osdinfra.net/cosmos/Knowledge/local/EntitySpace-Prod/Satori/Sources/BaseStreamFolder/XingFeed/v0_2/Fact.Base.ss";
            string urlPrefix = "https://cosmos08.osdinfra.net/cosmos/Knowledge/local/EntitySpace-Prod/";

            string urlWithouPrefix = url.Substring(urlPrefix.Length);

            Console.WriteLine(urlWithouPrefix);

            var urlParams = urlWithouPrefix.Split('/').ToList();

            urlParams.ForEach(t => Console.WriteLine(t));
        }

        public static void StringFormat()
        {
            var matchTag = false ? "" : "!";
            string conflationMapIScopeScriptFormat = @"
                EntitySpaceBaseStream =
                  AND {0}REGEX().IsMatch(Value);

                OUTPUT Result TO CONSOLE;";
            var s = string.Format(conflationMapIScopeScriptFormat, matchTag);
            s.Contains("aa");
            Console.WriteLine(s);
        }

        public static void AnyTest()
        {
            List<string> existing = new List<string> { "a", "b", "c" };
            List<string> current = new List<string> { "aa" };
            if (existing.Any(current.Contains))
            {
                Console.WriteLine("in");
            }
            Console.WriteLine("out");
        }

        public static void StopwatchTest()
        {
            Stopwatch stopwatch = Stopwatch.StartNew();
            while (true)
            {
                if (stopwatch.ElapsedMilliseconds > 5000)
                {
                    stopwatch.Stop();
                    break;
                }
                Console.WriteLine(stopwatch.ElapsedMilliseconds);
            }
            Console.WriteLine("---------------");
            Console.WriteLine(stopwatch.ElapsedMilliseconds);
        }

        public static void ReplaceStringTest()
        {
            string s = "http://knowledge.microsoft.com/Sports/sports_team/98aa610362565b4b91b29b28071ccef7";
            Console.WriteLine(s.Replace("placeholder", "SportEnstarData"));
        }

        public static void UrlRegexTest()
        {
            Regex regex = new Regex(@"^http(s)?://([\w-]+.)+[\w-]+(/[\w- ./?%&=])?$");
            if (regex.IsMatch("bloomberg.com/research/stocks/private/people.asp?privcapId=45980913"))
            {
                Console.WriteLine("match");
            }
        }

        public static void IndexOfTest()
        {
            string intro = "Find the first occurrence of a character using different " +
                   "values of StringComparison.";
            string resultFmt = "Comparison: {0,-28} Location: {1,3}";

            // Define a string to search for.
            // U+00c5 = LATIN CAPITAL LETTER A WITH RING ABOVE
            string CapitalAWithRing = "\u00c5";

            // Define a string to search. 
            // The result of combining the characters LATIN SMALL LETTER A and COMBINING 
            // RING ABOVE (U+0061, U+030a) is linguistically equivalent to the character 
            // LATIN SMALL LETTER A WITH RING ABOVE (U+00e5).
            string cat = "A Cheshire c" + "\u0061\u030a" + "t";

            int loc = 0;
            StringComparison[] scValues = {
                StringComparison.CurrentCulture,
                StringComparison.CurrentCultureIgnoreCase,
                StringComparison.InvariantCulture,
                StringComparison.InvariantCultureIgnoreCase,
                StringComparison.Ordinal,
                StringComparison.OrdinalIgnoreCase };

            // Clear the screen and display an introduction.
            Console.Clear();
            Console.WriteLine(intro);

            // Display the current culture because culture affects the result. For example, 
            // try this code example with the "sv-SE" (Swedish-Sweden) culture.

            Thread.CurrentThread.CurrentCulture = new CultureInfo("en-US");
            Console.WriteLine("The current culture is \"{0}\" - {1}.",
                               Thread.CurrentThread.CurrentCulture.Name,
                               Thread.CurrentThread.CurrentCulture.DisplayName);

            // Display the string to search for and the string to search.
            Console.WriteLine("Search for the string \"{0}\" in the string \"{1}\"",
                               CapitalAWithRing, cat);
            Console.WriteLine();

            // Note that in each of the following searches, we look for 
            // LATIN CAPITAL LETTER A WITH RING ABOVE in a string that contains 
            // LATIN SMALL LETTER A WITH RING ABOVE. A result value of -1 indicates 
            // the string was not found.
            // Search using different values of StringComparison. Specify the start 
            // index and count. 

            Console.WriteLine("Part 1: Start index and count are specified.");
            foreach (StringComparison sc in scValues)
            {
                loc = cat.IndexOf(CapitalAWithRing, 0, cat.Length, sc);
                Console.WriteLine(resultFmt, sc, loc);
            }

            // Search using different values of StringComparison. Specify the 
            // start index. 
            Console.WriteLine("\nPart 2: Start index is specified.");
            foreach (StringComparison sc in scValues)
            {
                loc = cat.IndexOf(CapitalAWithRing, 0, sc);
                Console.WriteLine(resultFmt, sc, loc);
            }

            // Search using different values of StringComparison. 
            Console.WriteLine("\nPart 3: Neither start index nor count is specified.");
            foreach (StringComparison sc in scValues)
            {
                loc = cat.IndexOf(CapitalAWithRing, sc);
                Console.WriteLine(resultFmt, sc, loc);
            }
        }
    }
}