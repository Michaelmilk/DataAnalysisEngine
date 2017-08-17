using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

namespace TfsReport
{
    //public class WorkItemReport
    //{
    //    public string Id { get; set; }
    //    public string Title { get; set; }
    //    public string Complexity { get; set; }
    //    public string CreatedDate { get; set; }
    //    public string DqSignOffDate { get; set; }
    //    public string ConflationSignOffDate { get; set; }
    //    public string EpSignOffDate { get; set; }

    //    public WorkItemReport(string id, string title, string createdTime)
    //    {
    //        this.Id = id;
    //        this.Title = title;
    //        this.CreatedDate = createdTime;
    //    }
    //}

    //public class WorkItemReportGenerator
    //{
    //    //cf81121f-a961-46e6-996e-611a694a482a
    //    private static readonly string workItemRequestUrlByQueryId =
    //        "https://satorionboardingrequest.visualstudio.com/DefaultCollection/_apis/wit/wiql/{0}?api-version=2.0";
    //    private static readonly string workItemDetailRequestUrlByIds =
    //        "https://satorionboardingrequest.visualstudio.com/DefaultCollection/_apis/wit/workitems?ids={0}&api-version=1.0";
    //    private static readonly string revisionRequestUrlByFiledFilter =
    //        "https://satorionboardingrequest.visualstudio.com/DefaultCollection/_apis/wit/reporting/workItemRevisions?api-version=2.0&fields={0}";

    //    private static readonly List<string> driApproverFields = new List<string>()
    //    {
    //        "OnboardingPublishForm.SourceComplexity",
    //        "OnboardingPublishForm.DQDRIApprover",
    //        "OnboardingPublishForm.ConflationDRIApprover",
    //        "OnboardingPublishForm.EPDRIApprover"
    //    };

    //    private static readonly List<string> requiredrevisionFields = new List<string>()
    //    {
    //        "OnboardingPublishForm.SourceComplexity",
    //        "OnboardingPublishForm.DQDRIApprover",
    //        "OnboardingPublishForm.ConflationDRIApprover",
    //        "OnboardingPublishForm.EPDRIApprover",
    //        "System.ChangedBy",
    //        "System.ChangedDate"
    //    };


    //    public static void GenerateCompletedWorkItemsReport()
    //    {
    //        try
    //        {
    //            var personalAccessToken = "ul5wsrjwmcwxuitjc4nh6k3mx37ufgli522ri47sss3qscocw6vq";

    //            using (HttpClient client = new HttpClient())
    //            {
    //                client.DefaultRequestHeaders.Accept.Add(
    //                    new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

    //                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic",
    //                    Convert.ToBase64String(
    //                        System.Text.ASCIIEncoding.ASCII.GetBytes(
    //                            string.Format("{0}:{1}", "", personalAccessToken))));

    //                List<WorkItemReport> workItemReports = new List<WorkItemReport>();

    //                //get work item ids under query
    //                List<string> completedWorkItemIds = new List<string>();
    //                var requestUrl = string.Format(workItemRequestUrlByQueryId, "cf81121f-a961-46e6-996e-611a694a482a");
    //                var workItemReponse = GetResponse(requestUrl, client).Result;
    //                JToken jToken = JToken.Parse(workItemReponse);
    //                completedWorkItemIds = jToken.SelectTokens("workItems[*].id").Select(t => t.ToString()).ToList();
    //                //completedWorkItemIds.ForEach(Console.WriteLine);

    //                //get work item details
    //                int idCount = completedWorkItemIds.Count;
    //                if (idCount < 200)
    //                {
    //                    var formattedIdParam = string.Join(",", completedWorkItemIds.ToArray());
    //                    requestUrl = string.Format(workItemDetailRequestUrlByIds, formattedIdParam);
    //                    var workItemDetailsResponse = GetResponse(requestUrl, client).Result;

    //                    jToken = JToken.Parse(workItemDetailsResponse);
    //                    //var title = jToken.SelectToken("fields").Value<string>("System.Title");
    //                    //var createdDate = jToken.SelectToken("fields").Value<DateTime>("System.CreatedDate");
    //                    //var formattedCreatedDate = string.Format("{0:M/d/yyyy}", createdDate);
    //                    var workItemDetails = jToken.SelectTokens("value[*]").ToList();
    //                    //items.ForEach(Console.WriteLine);
    //                    Console.WriteLine(workItemDetails.Count);
    //                    workItemDetails.ForEach(t =>
    //                    {
    //                        workItemReports.Add(new WorkItemReport(t["id"].ToString(), t["fields"].Value<string>("System.Title"),
    //                            $"{t["fields"].Value<DateTime>("System.CreatedDate"):M/d/yyyy}"));
    //                    });
    //                }
    //                else
    //                {
    //                    //todo
    //                }


    //                //get work item DRI sign off time
    //                requestUrl = string.Format(revisionRequestUrlByFiledFilter,
    //                    string.Join(",", requiredrevisionFields.ToArray()));
    //                var workItemRevisionResponse = GetResponse(requestUrl, client).Result;
    //                jToken = JToken.Parse(workItemRevisionResponse);
    //                //var revisionsById = jToken.SelectTokens("values[*]").Where(t =>
    //                //{
    //                //    bool isCompletedId = completedWorkItemIds.Contains((string)t["id"]);
    //                //    bool isContainsRequiredField = false;
    //                //    driApproverFields.ForEach(f =>
    //                //    {
    //                //        isContainsRequiredField |= t["fields"].Value<string>(f) != null;
    //                //    });
    //                //    return isCompletedId && isContainsRequiredField;
    //                //}).GroupBy(t => t["id"].ToString()).ToList();

    //                var revisionsById = jToken.SelectTokens("values[*]").Where(t =>
    //                {
    //                    bool isCompletedId = completedWorkItemIds.Contains((string)t["id"]);
    //                    bool isContainsRequiredField = false;
    //                    driApproverFields.ForEach(f =>
    //                    {
    //                        isContainsRequiredField |= t["fields"].Value<string>(f) != null;
    //                    });
    //                    return isCompletedId && isContainsRequiredField;
    //                }).OrderBy(t => (int)t["rev"]).ToList();
                    
    //                workItemReports.ForEach(t =>
    //                {
    //                    Dictionary<string, string> driTypeToSignTimeDict = new Dictionary<string, string>();
    //                    var revisionFacets = revisionsById.Where(r => r["id"].ToString() == t.Id).ToList();
    //                    driApproverFields.ForEach(d =>
    //                    {
    //                        var revisionFacet = revisionFacets.FirstOrDefault(f => f["fields"].Value<string>(d) != null);
    //                        if (revisionFacet?["fields"].Value<string>(d) != null)
    //                        {
    //                            driTypeToSignTimeDict.Add(d,
    //                                    $"{revisionFacet["fields"].Value<DateTime>("System.ChangedDate"):M/d/yyyy}");
    //                        }
    //                    });

    //                    t.Complexity = driTypeToSignTimeDict.ContainsKey("OnboardingPublishForm.SourceComplexity")
    //                        ? driTypeToSignTimeDict["OnboardingPublishForm.SourceComplexity"] : "Todo";
    //                    t.DqSignOffDate = driTypeToSignTimeDict.ContainsKey("OnboardingPublishForm.DQDRIApprover")
    //                        ? driTypeToSignTimeDict["OnboardingPublishForm.DQDRIApprover"] : "";
    //                    t.ConflationSignOffDate = driTypeToSignTimeDict.ContainsKey("OnboardingPublishForm.ConflationDRIApprover")
    //                        ? driTypeToSignTimeDict["OnboardingPublishForm.ConflationDRIApprover"] : "";
    //                    t.EpSignOffDate = driTypeToSignTimeDict.ContainsKey("OnboardingPublishForm.EPDRIApprover")
    //                        ? driTypeToSignTimeDict["OnboardingPublishForm.EPDRIApprover"] : "";
    //                });

    //                //revisionsById.ForEach(Console.WriteLine);

    //                Console.WriteLine("-------------------------------------------------------------------------------------------------------------------------------------------");
    //                Console.WriteLine(string.Format("{0,3} | {1,-90} | {2,10} | {3, 10} | {4, 10} | {5, 10} | {6, 10}",
    //                    "Id", "Title", "Complexity", "Created", "DQ", "Conflation", "EP"));
    //                //Console.WriteLine("Id   |   Title   |   Complexity  |   Created |   DQ sign off   |   Conflation sign off |   EP sign off");
    //                Console.WriteLine("-------------------------------------------------------------------------------------------------------------------------------------------");
                    
    //                foreach (var workItemReport in workItemReports)
    //                {
    //                    Console.WriteLine(string.Format("{0,3} | {1,-90} | {2,10} | {3, 10} | {4, 10} | {5, 10} | {6, 10}", workItemReport.Id,
    //                        workItemReport.Title, workItemReport.Complexity, workItemReport.CreatedDate,
    //                        workItemReport.DqSignOffDate, workItemReport.ConflationSignOffDate,
    //                        workItemReport.EpSignOffDate));
    //                    //Console.WriteLine(
    //                     //   $"{workItemReport.Id}\t{workItemReport.Title}\t{workItemReport.Complexity}\t{workItemReport.CreatedDate}\t{workItemReport.DqSignOffDate}\t{workItemReport.ConflationSignOffDate}\t{workItemReport.EpSignOffDate}");
    //                }
    //                Console.WriteLine("-------------------------------------------------------------------------------------------------------------------------------------------");

    //                //revisionsById.ForEach(t =>
    //                //{
    //                //    var sortedRevisions = t.OrderBy(f => (int)f["recv"]).ToList();
    //                //    sortedRevisions.ForEach(s =>
    //                //    {
    //                //        driApproverFields.ForEach(d =>
    //                //        {
    //                //            var revisionFacet = s.FirstOrDefault(f => f["fields"].Value<string>(d) != null);
    //                //            if (revisionFacet?["fields"].Value<string>(d) != null)
    //                //            {
    //                //                driTypeToSignTimeDict.Add(d,
    //                //                    $"{revisionFacet["fields"].Value<DateTime>("System.ChangedDate"):M/d/yyyy}");
    //                //            }
    //                //        });
    //                //    });
    //                //});

    //                ////Dictionary<string, string> driTypeToSignTimeDict = new Dictionary<string, string>();

    //                //driApproverFields.ForEach(t =>
    //                //{
    //                //    var revisionFact = revisionsById.FirstOrDefault(f => f["fields"].Value<string>(t) != null);
    //                //    if (revisionFact?["fields"].Value<string>(t) != null)
    //                //    {
    //                //        //driTypeToSignTimeDict.Add(revisionFact["fields"].Value<string>(t),
    //                //        //    revisionFact["fields"].Value<string>("System.ChangedDate"));
    //                //        driTypeToSignTimeDict.Add(t,
    //                //            $"{revisionFact["fields"].Value<DateTime>("System.ChangedDate"):M/d/yyyy}");
    //                //    }
    //                //});

    //                //foreach (var driTypeToSignTime in driTypeToSignTimeDict)
    //                //{
    //                //    Console.WriteLine("{0} : {1}", driTypeToSignTime.Key, driTypeToSignTime.Value);
    //                //}


    //            }
    //        }
    //        catch (Exception ex)
    //        {
    //            Console.WriteLine(ex.ToString());
    //        }
    //    }


    //    public static async Task<string> GetResponse(string url, HttpClient client)
    //    {
    //        using (HttpResponseMessage response = client.GetAsync(url).Result)
    //        {
    //            response.EnsureSuccessStatusCode();
    //            return await response.Content.ReadAsStringAsync();
    //        }
    //    }
    //}

    class Program
    {
        static void Main(string[] args)
        {
            //GetAllCompletedWorkItemTitleAndCreatedTime();
            //GetAllCompletedWorkItem();
            WorkItemReportGenerator.GenerateCompletedWorkItemsReport();
            Console.ReadKey();
            //Console.WriteLine("publish Xing.com source to de-de market (social network user segment)".Length);
        }

        

        public static async void GetAllCompletedWorkItemIds()
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
                        "https://satorionboardingrequest.visualstudio.com/DefaultCollection/_apis/wit/wiql/cf81121f-a961-46e6-996e-611a694a482a?api-version=2.0")
                        .Result)
                    {
                        response.EnsureSuccessStatusCode();
                        string responseBody = await response.Content.ReadAsStringAsync();

                        JToken jToken = JToken.Parse(responseBody);
                        var completedWorkItemIds = jToken.SelectTokens("workItems[*].id").Select(t => t.ToString()).ToList();
                        completedWorkItemIds.ForEach(Console.WriteLine);
                        //Console.WriteLine(responseBody);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }

        public static async void GetAllCompletedWorkItemTitleAndCreatedTime()
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

                    //string url = https://satorionboardingrequest.visualstudio.com/DefaultCollection/_apis/wit/workitems/54?api-version=1.0
                    string url =
                        "https://satorionboardingrequest.visualstudio.com/DefaultCollection/_apis/wit/workitems?ids=42,43,44&api-version=1.0";

                    using (HttpResponseMessage response = client.GetAsync(url).Result)
                    {
                        response.EnsureSuccessStatusCode();
                        string responseBody = await response.Content.ReadAsStringAsync();

                        JToken jToken = JToken.Parse(responseBody);
                        //var title = jToken.SelectToken("fields").Value<string>("System.Title");
                        //var createdDate = jToken.SelectToken("fields").Value<DateTime>("System.CreatedDate");
                        //var formattedCreatedDate = string.Format("{0:M/d/yyyy}", createdDate);
                        //Console.WriteLine(title);
                        //Console.WriteLine(formattedCreatedDate);

                        var items = jToken.SelectTokens("value[*]").ToList();
                        //items.ForEach(Console.WriteLine);
                        Console.WriteLine(items.Count);
                        items.ForEach(t =>
                        {
                            Console.WriteLine(t["id"]);
                            Console.WriteLine(t["fields"].Value<string>("System.Title"));
                            Console.WriteLine(t["fields"].Value<DateTime>("System.CreatedDate"));
                        });
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }

        public static async void GetAllCompletedWorkItem()
        {
            try
            {
                var personalaccesstoken = "ul5wsrjwmcwxuitjc4nh6k3mx37ufgli522ri47sss3qscocw6vq";
                var requiredField = new List<string>()
                {
                    "OnboardingPublishForm.DQDRIApprover",
                    "OnboardingPublishForm.ConflationDRIApprover",
                    "OnboardingPublishForm.EPDRIApprover"
                };

                using (HttpClient client = new HttpClient())
                {
                    client.DefaultRequestHeaders.Accept.Add(
                        new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic",
                        Convert.ToBase64String(
                            System.Text.ASCIIEncoding.ASCII.GetBytes(
                                string.Format("{0}:{1}", "", personalaccesstoken))));

                    using (HttpResponseMessage response = client.GetAsync(
                        "https://satorionboardingrequest.visualstudio.com/DefaultCollection/_apis/wit/reporting/workItemRevisions?api-version=2.0&fields=OnboardingPublishForm.EPDRIApprover,OnboardingPublishForm.DQDRIApprover,OnboardingPublishForm.ConflationDRIApprover,System.ChangedBy,System.ChangedDate")
                        .Result)
                    {
                        response.EnsureSuccessStatusCode();
                        string responseBody = await response.Content.ReadAsStringAsync();

                        JToken jToken = JToken.Parse(responseBody);
                        //var title = jToken.SelectToken("fields.System.Title").ToString();
                        //var revisionById = jToken.SelectTokens("values[*]").Where(t => (string)t["id"] == "43").ToList();
                        var revisionById = jToken.SelectTokens("values[*]").Where(t =>
                        {
                            bool isvalidId = (string)t["id"] == "43";
                            bool isContainsRequiredField = false;
                            requiredField.ForEach(f =>
                            {
                                isContainsRequiredField |= t["fields"].Value<string>(f) != null;
                            });
                            return isvalidId && isContainsRequiredField;
                        }).OrderBy(t => (int)t["rev"]).ToList();

                        revisionById.ForEach(Console.WriteLine);

                        Dictionary<string, string> driTypeToSignTimeDict = new Dictionary<string, string>();

                        requiredField.ForEach(t =>
                        {
                            var revisionFact = revisionById.FirstOrDefault(f => f["fields"].Value<string>(t) != null);
                            if (revisionFact?["fields"].Value<string>(t) != null)
                            {
                                //driTypeToSignTimeDict.Add(revisionFact["fields"].Value<string>(t),
                                //    revisionFact["fields"].Value<string>("System.ChangedDate"));
                                driTypeToSignTimeDict.Add(t,
                                    $"{revisionFact["fields"].Value<DateTime>("System.ChangedDate"):M/d/yyyy}");
                            }
                        });

                        foreach (var driTypeToSignTime in driTypeToSignTimeDict)
                        {
                            Console.WriteLine("{0} : {1}", driTypeToSignTime.Key, driTypeToSignTime.Value);
                        }
                        //revisionById.ForEach(Console.WriteLine);
                        //Console.WriteLine(revisionById.ToString());
                        //Console.WriteLine(formattedCreatedDate);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }



        public static async void GetAllProjects()
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
                                "https://satorionboardingrequest.visualstudio.com/DefaultCollection/_apis/projects?api-version=1.0").Result)
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

    }
}
