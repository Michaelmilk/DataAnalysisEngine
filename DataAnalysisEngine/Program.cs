using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

namespace DataAnalysisEngine
{
    class Program
    {
        static void Main(string[] args)
        {
            //Mix.ReplaceViewNameWithPlaceholder();
            //bool isdMatch = Mix.IsMatchType(1.99, typeof (double));
            //Console.WriteLine(isdMatch);

            //Mix.JsonPathSample();

            //FlattenJsonWithoutArrayIndex();
            //FlattenJsonWithArrayIndex();

            //Mix.ParseJsonObject();

            //FlattenAllJsonPaths();

            //FlattenJsonByPath();

            Mix.NewTonJsonSample();
        }

        static public bool CheckJsonPathExistence()
        {
            return false;
        }

        public static void FlattenJsonByPath()
        {
            string line = string.Empty;
            using (var rd = new StreamReader(@".\sampleJson"))
            {
                line = rd.ReadLine();
            }

            //var jsonPaths = JsonFlatten.FlattenJsonByPath(line, "nodeKey.Events.Name");
            var jsonPaths = JsonFlatten.FlattenJsonByPath(line, "nodeKey.__SubjectId__");

            if (jsonPaths.Value.GetType() == typeof(List<string>))
            {
                var values = jsonPaths.Value as List<string>;
                values.ForEach(t => Console.WriteLine(t));
            }

            if (jsonPaths.Value.GetType() == typeof(string))
            {
                var value = jsonPaths.Value as string;
                Console.WriteLine(value);
            }
            //jsonPaths.ForEach(t => Console.WriteLine("{0}: {1}", t.Key, t.Value));
        }

        public static void FlattenAllJsonPaths()
        {
            string line = string.Empty;
            using (var rd = new StreamReader(@".\sampleJson"))
            {
                line = rd.ReadLine();
            }

            var jsonPaths = JsonFlatten.FlattenAllJsonPaths(line, false);

            jsonPaths.ForEach(t => Console.WriteLine(t));
        }

        /// <summary>
        /// installations.installationid: 6
        /// installations.installationstatus.installationstatusid: 4
        /// installations.installationstatus.installationstatus: FAIL
        /// installations.isactive: True
        /// installations.installationid: 7
        /// installations.installationstatus.installationstatusid: 1
        /// installations.installationstatus.installationstatus: NEW
        /// installations.isactive: False
        /// </summary>
        public static void FlattenJsonWithoutArrayIndex()
        {
            string line = string.Empty;
            using (var rd = new StreamReader(@".\sampleJson"))
            {
                line = rd.ReadLine();
            }

            var dict = JsonFlatten.Flatten(line, false);
            using (var wr = new StreamWriter(@"D:\E\FlattenedJsonWithoutArrayIndex"))
            {
                foreach (var kvp in dict)
                {
                    Console.WriteLine(kvp.Key + ": " + kvp.Value);
                    wr.WriteLine(kvp.Key + ": " + kvp.Value);
                }
            }
        }


        /// <summary>
        /// installations.0.installationid: 6
        /// installations.0.installationstatus.installationstatusid: 4
        /// installations.0.installationstatus.installationstatus: FAIL
        /// installations.0.isactive: True
        /// installations.1.installationid: 7
        /// installations.1.installationstatus.installationstatusid: 1
        /// installations.1.installationstatus.installationstatus: NEW
        /// installations.1.isactive: False
        /// </summary>
        public static void FlattenJsonWithArrayIndex()
        {
            string line = string.Empty;
            using (var rd = new StreamReader(@".\sampleJson"))
            {
                line = rd.ReadLine();
            }

            var dict = JsonFlatten.Flatten(line, true);
            using (var wr = new StreamWriter(@"D:\E\FlattenedJson"))
            {
                foreach (var kvp in dict)
                {
                    Console.WriteLine(kvp.Key + ": " + kvp.Value);
                    wr.WriteLine(kvp.Key + ": " + kvp.Value);
                }
            }
        }


        /// <summary>
        /// installationid0: 6
        /// installationstatusid0: 4
        /// installationstatus0: FAIL
        /// isactive0: True
        /// installationid1: 7
        /// installationstatusid1: 1
        /// installationstatus1: NEW
        /// isactive1: False
        /// </summary>
        public static void FlattenJson2()
        {
            string line = string.Empty;
            using (var rd = new StreamReader(@".\sampleJson"))
            {
                line = rd.ReadLine();
            }

            var dict = JsonFlatten.Flatten(line, true);
            using (var wr = new StreamWriter(@"D:\E\FlattenedJson2"))
            {
                foreach (var kvp in dict)
                {
                    int i = kvp.Key.LastIndexOf(".");
                    string key = (i > -1 ? kvp.Key.Substring(i + 1) : kvp.Key);
                    Match m = Regex.Match(kvp.Key, @"\.([0-9]+)\.");
                    if (m.Success)
                        key += m.Groups[1].Value;
                    foreach (var value in m.Groups)
                    {
                        var t = value;
                    }
                    Console.WriteLine(key + ": " + kvp.Value);
                    wr.WriteLine(key + ": " + kvp.Value);
                }
            }
        }
    }
}
