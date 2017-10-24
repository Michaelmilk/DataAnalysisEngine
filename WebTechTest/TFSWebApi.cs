using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace WebTechTest
{
    public class TFSWebApi
    {
        public static async void GetWorkItem()
        {
            try
            {
                var personalaccesstoken = "ul5wsrjwmcwxuitjc4nh6k3mx37ufgli522ri47sss3qscocw6vq";

                using (HttpClient client = new HttpClient())
                {
                    client.DefaultRequestHeaders.Accept.Add(
                        new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic",
                        Convert.ToBase64String(
                            System.Text.ASCIIEncoding.ASCII.GetBytes(
                                string.Format("{0}:{1}", "", personalaccesstoken))));

                    using (HttpResponseMessage response = client.GetAsync(
                                "https://satorionboardingrequest.visualstudio.com/DefaultCollection/_apis/wit/workitems/54?api-version=1.0").Result)
                    {
                        response.EnsureSuccessStatusCode();
                        string responseBody = await response.Content.ReadAsStringAsync();
                        Console.WriteLine(responseBody);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }

        public static async void GetUpdateHistorys()
        {
            try
            {
                var personalaccesstoken = "ul5wsrjwmcwxuitjc4nh6k3mx37ufgli522ri47sss3qscocw6vq";

                using (HttpClient client = new HttpClient())
                {
                    client.DefaultRequestHeaders.Accept.Add(
                        new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic",
                        Convert.ToBase64String(
                            System.Text.ASCIIEncoding.ASCII.GetBytes(
                                string.Format("{0}:{1}", "", personalaccesstoken))));

                    using (HttpResponseMessage response = client.GetAsync(
                                "https://satorionboardingrequest.visualstudio.com/DefaultCollection/_apis/wit/workItems/54/updates").Result)
                    {
                        response.EnsureSuccessStatusCode();
                        string responseBody = await response.Content.ReadAsStringAsync();
                        Console.WriteLine(responseBody);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }
        //all projects(Area):https://satorionboardingrequest.visualstudio.com/DefaultCollection/_apis/projects?api-version=1.0
        //query:https://satorionboardingrequest.visualstudio.com/DefaultCollection/PublishRequestForm/_apis/wit/queries?api-version=1.0&$depth=2
        //all work items by query id:https://satorionboardingrequest.visualstudio.com/DefaultCollection/PublishRequestForm/_apis/wit/wiql/6d83cc71-fee4-40fc-82fd-3913ca89ef42?api-version=1.0


        //public static void test()
        //{
        //    int id = 5;

        //    VssConnection connection = Context.Connection;
        //    WorkItemTrackingHttpClient workItemTrackingClient = connection.GetClient<WorkItemTrackingHttpClient>();

        //    WorkItem workitem = workItemTrackingClient.GetWorkItemAsync(id, expand: WorkItemExpand.All).Result;

        //    Console.WriteLine(workitem.Id);
        //    Console.WriteLine("Fields: ");
        //    foreach (var field in workitem.Fields)
        //    {
        //        Console.WriteLine("  {0}: {1}", field.Key, field.Value);
        //    }

        //    Console.WriteLine("Relations: ");
        //    foreach (var relation in workitem.Relations)
        //    {
        //        Console.WriteLine("  {0} {1}", relation.Rel, relation.Url);
        //    }

        //    return workitem;
        //}
    }
}
