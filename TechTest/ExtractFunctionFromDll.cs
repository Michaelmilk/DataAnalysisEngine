using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace TechTest
{
    public class ExtractFunctionFromDll
    {
        public static void ExtractFunctionsFromDll()
        {
            Assembly SampleAssembly;
            //SampleAssembly = Assembly.LoadFrom("BlingResource.dll");
            //SampleAssembly = Assembly.LoadFrom("SatoriFunctoids.dll");

            var dllBytes = File.ReadAllBytes("SatoriFunctoids.dll");

            SampleAssembly = Assembly.Load(dllBytes);

            var types = SampleAssembly.GetTypes().ToList();

            using (var wr = new StreamWriter("methodInfo1.txt"))
            {
                foreach (var type in types)
                {
                    var methods = type.GetMethods().ToList();
                    methods.ForEach(t =>
                    {
                        string output = string.Empty;
                        //Console.WriteLine(t.Name);
                        var customizedAttributes = t.CustomAttributes.ToList();
                        if (customizedAttributes.Count != 0)
                        {
                            var attributeNames = customizedAttributes.Select(a => a.AttributeType.Name);
                            if (attributeNames.Contains("FunctoidAttribute"))
                            {
                                output += t.ReturnType + " ";
                                output += t.Name + " (";
                                var parammeters = t.GetParameters().ToList();
                                parammeters.ForEach(k =>
                                {
                                    output += k.ParameterType + " " + k.Name + ", ";
                                    //Console.WriteLine("type: {0}, value: {1}", k.ParameterType, k.Name);
                                });
                                if (parammeters.Count != 0)
                                {
                                    output = output.Substring(0, output.Length - 2);
                                }
                                output += ")";

                                output = output.Replace("System.Collections.Generic.", "");
                                output = output.Replace("System.", "");
                                output = output.Replace("`1[", "<");
                                output = output.Replace("`2[", "<");
                                output = output.Replace("]", ">");
                                output = output.Replace("[>", "[]");

                                Regex regex = new Regex("Nullable<(.*?)>");
                                var v = regex.Match(output);
                                string s = v.Groups[1].ToString();
                                Console.WriteLine(s);
                                output = output.Replace($"Nullable<{s}>", $"{s}?");
                                output = output.Replace("Nullable<", "");
                                Console.WriteLine(output);
                                wr.WriteLine(output);
                            }
                        }
                    });
                }
            }
                

            //// Obtain a reference to a method known to exist in assembly.
            //MethodInfo Method = SampleAssembly.GetTypes()[0].GetMethod("Method1");
            //// Obtain a reference to the parameters collection of the MethodInfo instance.
            //ParameterInfo[] Params = Method.GetParameters();
            //// Display information about method parameters.
            //// Param = sParam1
            ////   Type = System.String
            ////   Position = 0
            ////   Optional=False
            //foreach (ParameterInfo Param in Params)
            //{
            //    Console.WriteLine("Param=" + Param.Name.ToString());
            //    Console.WriteLine("  Type=" + Param.ParameterType.ToString());
            //    Console.WriteLine("  Position=" + Param.Position.ToString());
            //    Console.WriteLine("  Optional=" + Param.IsOptional.ToString());
            //}



        }

        public static void ExtractSubBetweenSpecificString()
        {
            string response = 
                "<ExceptionMessage>Could not find mapped types: http://knowledge.microsoft.com/mso/people#person11111 in ontology</ExceptionMessage>";
            Regex regex = new Regex(".*<ExceptionMessage>(.*?)</ExceptionMessage>.*");
            var match = regex.Match(response);
            Console.WriteLine(match.Groups[1].ToString());
        }
    }
}
