using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

namespace DataAnalysisEngine
{
    public class JsonPaser
    {
        public string GetValueByPath()
        {
            return null;
        }
    }

    public static class JsonFlatten
    {
        public static List<KeyValuePair<string, object>> Flatten(string json, bool withArryIndex)
        {
            List<KeyValuePair<string, object>> dict = new List<KeyValuePair<string, object>>();
            JToken token = JToken.Parse(json);
            GeneratePathToValuePairs(dict, token, "", withArryIndex);
            return dict;
        }

        private static void GeneratePathToValuePairs(List<KeyValuePair<string, object>> dict, JToken token,
            string prefix, bool withArrayIndex)
        {
            switch (token.Type)
            {
                case JTokenType.Object:
                    foreach (JProperty prop in token.Children<JProperty>())
                    {
                        GeneratePathToValuePairs(dict, prop.Value, Join(prefix, prop.Name), withArrayIndex);
                    }

                    break;
                case JTokenType.Array:
                    int index = 0;
                    foreach (JToken value in token.Children())
                    {
                        if (withArrayIndex)
                        {
                            GeneratePathToValuePairs(dict, value, Join(prefix, index.ToString()), withArrayIndex);
                        }
                        else
                        {
                            GeneratePathToValuePairs(dict, value, prefix, withArrayIndex);
                        }

                        index++;
                    }

                    break;
                default:
                    dict.Add(new KeyValuePair<string, object>(prefix, ((JValue)token).Value));
                    break;
            }
        }

        private static string Join(string prefix, string name)
        {
            return (string.IsNullOrEmpty(prefix) ? name : prefix + "." + name);
        }
    }
}
