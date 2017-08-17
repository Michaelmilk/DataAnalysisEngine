using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace TfsReport
{
    public class WorkItemReport
    {
        public string Id { get; set; }
        public string Title { get; set; }
        public string Complexity { get; set; }
        public string CreatedDate { get; set; }
        public string DqSignOffDate { get; set; }
        public string ConflationSignOffDate { get; set; }
        public string EpSignOffDate { get; set; }

        public WorkItemReport(string id, string title, string createdTime)
        {
            this.Id = id;
            this.Title = title;
            this.CreatedDate = createdTime;
        }
    }

    public class WorkItemReportGenerator
    {
        //cf81121f-a961-46e6-996e-611a694a482a
        private static readonly string workItemRequestUrlByQueryId =
            "https://satorionboardingrequest.visualstudio.com/DefaultCollection/_apis/wit/wiql/{0}?api-version=2.0";
        private static readonly string workItemDetailRequestUrlByIds =
            "https://satorionboardingrequest.visualstudio.com/DefaultCollection/_apis/wit/workitems?ids={0}&api-version=1.0";
        private static readonly string revisionRequestUrlByFiledFilter =
            "https://satorionboardingrequest.visualstudio.com/DefaultCollection/_apis/wit/reporting/workItemRevisions?api-version=2.0&fields={0}";

        private static readonly List<string> driApproverFields = new List<string>()
        {
            "OnboardingPublishForm.SourceComplexity",
            "OnboardingPublishForm.DQDRIApprover",
            "OnboardingPublishForm.ConflationDRIApprover",
            "OnboardingPublishForm.EPDRIApprover"
        };

        private static readonly List<string> requiredrevisionFields = new List<string>()
        {
            "OnboardingPublishForm.SourceComplexity",
            "OnboardingPublishForm.DQDRIApprover",
            "OnboardingPublishForm.ConflationDRIApprover",
            "OnboardingPublishForm.EPDRIApprover",
            "System.ChangedBy",
            "System.ChangedDate"
        };


        public static void GenerateCompletedWorkItemsReport()
        {
            try
            {
                var personalAccessToken = "ul5wsrjwmcwxuitjc4nh6k3mx37ufgli522ri47sss3qscocw6vq";

                using (HttpClient client = new HttpClient())
                {
                    client.DefaultRequestHeaders.Accept.Add(
                        new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic",
                        Convert.ToBase64String(
                            System.Text.ASCIIEncoding.ASCII.GetBytes(
                                string.Format("{0}:{1}", "", personalAccessToken))));

                    List<WorkItemReport> workItemReports = new List<WorkItemReport>();

                    //get work item ids under query
                    List<string> completedWorkItemIds = new List<string>();
                    var requestUrl = string.Format(workItemRequestUrlByQueryId, "cf81121f-a961-46e6-996e-611a694a482a");
                    var workItemReponse = GetResponse(requestUrl, client).Result;
                    JToken jToken = JToken.Parse(workItemReponse);
                    completedWorkItemIds = jToken.SelectTokens("workItems[*].id").Select(t => t.ToString()).ToList();

                    //get work item details
                    int idCount = completedWorkItemIds.Count;
                    List<JToken> workItemDetails = new List<JToken>();
                    //TFS's web api just support to query 200 work item one time
                    if (idCount < 200)
                    {
                        var formattedIdParam = string.Join(",", completedWorkItemIds.ToArray());
                        requestUrl = string.Format(workItemDetailRequestUrlByIds, formattedIdParam);
                        var workItemDetailsResponse = GetResponse(requestUrl, client).Result;

                        jToken = JToken.Parse(workItemDetailsResponse);
                        workItemDetails = jToken.SelectTokens("value[*]").ToList();
                    }
                    else
                    {
                        int chunkCount = idCount/200;
                        for (int i = 0; i < chunkCount; i++)
                        {
                            var currentIdsChunk = completedWorkItemIds.Skip(i*200).Take(200).ToList();
                            var formattedIdParam = string.Join(",", currentIdsChunk.ToArray());
                            requestUrl = string.Format(workItemDetailRequestUrlByIds, formattedIdParam);
                            var workItemDetailsResponse = GetResponse(requestUrl, client).Result;

                            jToken = JToken.Parse(workItemDetailsResponse);
                            workItemDetails.AddRange(jToken.SelectTokens("value[*]").ToList());
                        }
                    }

                    workItemDetails.ForEach(t =>
                    {
                        var entitySpaceOrViewName = string.Empty;
                        if (t["fields"].Value<string>("OnboardingPublishForm.EntitySpaceURL") != null)
                        {
                            var entitySpaceUrl = t["fields"].Value<string>("OnboardingPublishForm.EntitySpaceURL");
                            var urlParts = entitySpaceUrl.Split('/');
                            var entitySpaceName = string.Empty;
                            var entityViewName = string.Empty;
                            var urlParamStr = string.Empty;
                            var customerIdAndEnv = urlParts[5];
                            //Console.WriteLine(entitySpaceUrl);

                            //negative case: http://entityrepository.binginternal.com:88/ERPortal/PROD/Wikipedia-WikiAdhocExtraction/EntitySpaceOverview/Browse?name=WikiReadOutText
                            var startPos = entitySpaceUrl.IndexOf("?", StringComparison.Ordinal);
                            var endPos = entitySpaceUrl.IndexOf("#", StringComparison.Ordinal);
                            if (endPos > startPos)
                            {
                                urlParamStr = entitySpaceUrl.Substring(startPos + 1, endPos - startPos - 1);
                            }
                            else
                            {
                                urlParamStr = entitySpaceUrl.Substring(startPos + 1);
                            }

                            //if url contains view key, so it's a view url, otherwise it's a entityspace url
                            if (entitySpaceUrl.Contains("viewKey"))
                            {//http://entityrepository.binginternal.com:88/ERPortal/PROD/WrapStar-Prod/EntitySpaceOverview/EntitySpaceViewDetails?entitySpaceName=WrapStar-Full&viewKey=WrapStar-Full_wrapstar_organization_crunchbase#v175_1_0
                                var viewParams = urlParamStr.Split('&');
                                entitySpaceName = viewParams[0].Replace("entitySpaceName=", "");
                                entityViewName = viewParams[1].Replace("viewKey=", "");
                                entityViewName = entityViewName.Replace(entitySpaceName + "_", "");
                                entitySpaceOrViewName = customerIdAndEnv + "/" + entitySpaceName + "/" + entityViewName;
                            }
                            else
                            {//http://entityrepository.binginternal.com:88/ERPortal/PROD/Satori-Sources/EntitySpaceOverview/Browse?name=SportsEnstarData#metadata
                                entitySpaceName = urlParamStr.Replace("name=", "");
                                entitySpaceOrViewName = customerIdAndEnv + "/" + entitySpaceName;
                            }
                        }
                        workItemReports.Add(new WorkItemReport(t["id"].ToString(), entitySpaceOrViewName,
                            $"{t["fields"].Value<DateTime>("System.CreatedDate"):M/d/yyyy}"));
                    });

                    //get work item DRI sign off time
                    requestUrl = string.Format(revisionRequestUrlByFiledFilter,
                        string.Join(",", requiredrevisionFields.ToArray()));
                    var workItemRevisionResponse = GetResponse(requestUrl, client).Result;
                    jToken = JToken.Parse(workItemRevisionResponse);

                    var revisionsById = jToken.SelectTokens("values[*]").Where(t =>
                    {
                        bool isCompletedId = completedWorkItemIds.Contains((string)t["id"]);
                        bool isContainsRequiredField = false;
                        driApproverFields.ForEach(f =>
                        {
                            isContainsRequiredField |= t["fields"].Value<string>(f) != null;
                        });
                        return isCompletedId && isContainsRequiredField;
                    }).OrderBy(t => (int)t["rev"]).ToList();

                    workItemReports.ForEach(t =>
                    {
                        Dictionary<string, string> driTypeToSignTimeDict = new Dictionary<string, string>();
                        var revisionFacets = revisionsById.Where(r => r["id"].ToString() == t.Id).ToList();
                        driApproverFields.ForEach(d =>
                        {
                            var revisionFacet = revisionFacets.FirstOrDefault(f => f["fields"].Value<string>(d) != null);
                            if (revisionFacet?["fields"].Value<string>(d) != null)
                            {
                                driTypeToSignTimeDict.Add(d,
                                        $"{revisionFacet["fields"].Value<DateTime>("System.ChangedDate"):M/d/yyyy}");
                            }
                        });

                        t.Complexity = driTypeToSignTimeDict.ContainsKey("OnboardingPublishForm.SourceComplexity")
                            ? driTypeToSignTimeDict["OnboardingPublishForm.SourceComplexity"] : "Todo";
                        t.DqSignOffDate = driTypeToSignTimeDict.ContainsKey("OnboardingPublishForm.DQDRIApprover")
                            ? driTypeToSignTimeDict["OnboardingPublishForm.DQDRIApprover"] : "";
                        t.ConflationSignOffDate = driTypeToSignTimeDict.ContainsKey("OnboardingPublishForm.ConflationDRIApprover")
                            ? driTypeToSignTimeDict["OnboardingPublishForm.ConflationDRIApprover"] : "";
                        t.EpSignOffDate = driTypeToSignTimeDict.ContainsKey("OnboardingPublishForm.EPDRIApprover")
                            ? driTypeToSignTimeDict["OnboardingPublishForm.EPDRIApprover"] : "";
                    });
                    
                    using (StreamWriter writer = new StreamWriter("workItemReport.txt"))
                    {
                        Output(writer, workItemReports);
                        Output(Console.Out, workItemReports);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }

        private static void Output(TextWriter writer, IEnumerable<WorkItemReport> workItemReports)
        {
            writer.WriteLine("-------------------------------------------------------------------------------------------------------------------------------------------");
            writer.WriteLine(string.Format("{0,3} | {1,-90} | {2,10} | {3, 10} | {4, 10} | {5, 10} | {6, 10} |",
                "Id", "Title", "Complexity", "Created", "DQ", "Conflation", "EP"));
            //Console.WriteLine("Id   |   Entity Space / View   |   Complexity  |   Created |   DQ sign off   |   Conflation sign off |   EP sign off");
            writer.WriteLine("-------------------------------------------------------------------------------------------------------------------------------------------");

            foreach (var workItemReport in workItemReports)
            {
                writer.WriteLine(string.Format("{0,3} | {1,-90} | {2,10} | {3, 10} | {4, 10} | {5, 10} | {6, 10} |", workItemReport.Id,
                    workItemReport.Title, workItemReport.Complexity, workItemReport.CreatedDate,
                    workItemReport.DqSignOffDate, workItemReport.ConflationSignOffDate,
                    workItemReport.EpSignOffDate));
            }
            writer.WriteLine("-------------------------------------------------------------------------------------------------------------------------------------------");
        }

        private static async Task<string> GetResponse(string url, HttpClient client)
        {
            using (HttpResponseMessage response = client.GetAsync(url).Result)
            {
                response.EnsureSuccessStatusCode();
                return await response.Content.ReadAsStringAsync();
            }
        }
    }
}
