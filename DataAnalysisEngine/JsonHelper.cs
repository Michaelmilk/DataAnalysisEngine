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
        public static List<string> FlattenAllJsonPaths(string json, bool withArryIndex)
        {
            List<string> jsonPaths = new List<string>();
            JToken token = JToken.Parse(json);
            GenerateJsonPaths(jsonPaths, token, "", withArryIndex);
            return jsonPaths;
        }

        private static void GenerateJsonPaths(List<string> jsonPaths, JToken token,
            string prefix, bool withArrayIndex)
        {
            switch (token.Type)
            {
                case JTokenType.Object:
                    foreach (JProperty prop in token.Children<JProperty>())
                    {
                        jsonPaths.Add(Join(prefix, prop.Name));
                        GenerateJsonPaths(jsonPaths, prop.Value, Join(prefix, prop.Name), withArrayIndex);
                    }

                    break;
                case JTokenType.Array:
                    int index = 0;
                    foreach (JToken value in token.Children())
                    {
                        jsonPaths.Add(prefix);
                        if (withArrayIndex)
                        {
                            GenerateJsonPaths(jsonPaths, value, Join(prefix, index.ToString()), withArrayIndex);
                        }
                        else
                        {
                            GenerateJsonPaths(jsonPaths, value, prefix, withArrayIndex);
                        }

                        index++;
                    }

                    break;
                default:
                    break;
            }
        }

        public static KeyValuePair<string, object> FlattenJsonByPath(string json, string jsonPath)
        {
            JObject jObject = JObject.Parse(json);
            List<string> jsonPathSegments = jsonPath.Split('.').ToList();
            if (jsonPathSegments.Count == 0)
            {
                return new KeyValuePair<string, object>();
            }

            string currentJsonPath = string.Empty;
            bool isContainArray = false;
            foreach (var pathSeg in jsonPathSegments)
            {
                currentJsonPath += pathSeg;
                JToken token;
                if (isContainArray)
                {
                    token = jObject.SelectTokens(currentJsonPath).First();
                }
                else
                {
                    token = jObject.SelectToken(currentJsonPath);
                }

                if (token.Type == JTokenType.Array)
                {
                    currentJsonPath += "[*]";
                    isContainArray = true;
                }

                currentJsonPath += ".";

            }

            currentJsonPath = currentJsonPath.Remove(currentJsonPath.Length - 1);

            if (isContainArray)
            {
                var values = jObject.SelectTokens(currentJsonPath).Select(t => t.ToString()).ToList();

                return new KeyValuePair<string, object>(jsonPath, values);
            }
            else
            {
                var value = jObject.SelectToken(currentJsonPath);
                return new KeyValuePair<string, object>(jsonPath, value.ToString());
            }
        }

        private static void GeneratePathToValuePairsByJsonPath(List<KeyValuePair<string, string>> dict, JObject jObject,
            List<string> jsonPathSegments, int index, string currentJsonPath)
        {
            var token = jObject.SelectToken(currentJsonPath);
            switch (token.Type)
            {
                case JTokenType.Object:
                    currentJsonPath += $".{jsonPathSegments[index]}";
                    GeneratePathToValuePairsByJsonPath(dict, jObject, jsonPathSegments, index + 1, currentJsonPath);

                    break;
                case JTokenType.Array:
                    foreach (JToken value in token.Children())
                    {
                        foreach (JProperty prop in value.Children<JProperty>())
                        {
                            if (prop.Name == jsonPathSegments[index])
                            {
                                currentJsonPath += $".{jsonPathSegments[index]}";
                                GeneratePathToValuePairsByJsonPath(dict, prop.Value.Value<JObject>(), jsonPathSegments, index + 1, currentJsonPath);
                            }
                        }
                    }

                    break;
                default:
                    dict.Add(new KeyValuePair<string, string>(currentJsonPath, ((JValue)token).Value.ToString()));
                    break;
            }
        }
        
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
