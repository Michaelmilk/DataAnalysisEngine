using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechTest
{
    public class StringTests
    {
        private static Dictionary<string, string> deprecatedProperties { get; set; }
        public static void IsGuidTest()
        {
            Guid guid;
            var isGuid = Guid.TryParse("3a0f853e-5769-4290-a121-41937cd103ec", out guid);
            Console.WriteLine(isGuid);
            Console.WriteLine(guid);
        }

        public static void LastCharOfStr()
        {
            var s1 = "https://cosmos08.osdinfra.net/cosmos/EntityPlatform";
            var s2 = "https://cosmos08.osdinfra.net/cosmos/STCISegmentsVC1.ProdLong/";

            if (s1.LastOrDefault() == '/')
            {
                var vc = s1.Substring(0, s1.Length - 1);
                var slashPos = vc.LastIndexOf("/");
                var vcNameWithoutPrefix = vc.Substring(slashPos + 1);
                Console.WriteLine(vcNameWithoutPrefix);
            }
            else
            {
                var slashPos = s1.LastIndexOf("/");
                var vcNameWithoutPrefix = s1.Substring(slashPos + 1);
                Console.WriteLine(vcNameWithoutPrefix);
            }

            if (s2.LastOrDefault() == '/')
            {
                var vc = s2.Substring(0, s2.Length - 1);
                var slashPos = vc.LastIndexOf("/");
                var vcNameWithoutPrefix = vc.Substring(slashPos + 1);
                Console.WriteLine(vcNameWithoutPrefix);
            }
        }

        //public static void ValidateDeprecatedProperties(MsoTriple triple, Dictionary<string, bool> deprecatedPropertyDict)
        //{
        //    //Get existing deprecated property to check the existence of its replaced property.
        //    var existingDeprecatedProperties = deprecatedPropertyDict.Keys.Where(t => deprecatedProperties[t] == triple.Property).ToList();
        //    if (existingDeprecatedProperties.Count != 0)
        //    {
        //        existingDeprecatedProperties.ForEach(t => deprecatedPropertyDict[t] = true);
        //    }

        //    if (this.deprecatedProperties.ContainsKey(triple.Property) && !deprecatedPropertyDict.ContainsKey(triple.Property))
        //    {
        //        deprecatedPropertyDict[triple.Property] = false;
        //    }
        //}

        public static void VcTest()
        {
            var virtualCluster = "https://cosmos08.osdinfra.net/cosmos/Knowledge.prod/";
            var len = virtualCluster.Length;
            //var virtualCluster = virtualCluster;
            if (virtualCluster.LastOrDefault() == '/')
            {
                virtualCluster = virtualCluster.Substring(0, len - 1);
            }
            var slashPos = virtualCluster.LastIndexOf("/", StringComparison.Ordinal);
            var vcNameWithoutPrefix = virtualCluster.Substring(slashPos + 1);

            Console.WriteLine($"/shares/{vcNameWithoutPrefix}this.VcRelativeFolder");
        }

        public static void StringOrder()
        {
            var urls = new List<string>()
            {
                "WS:https://www.drugs.com/mtm/neuac.html",
                "WS:https://www.drugs.com/mtm/onexton.html",
                "WS:https://www.drugs.com/mtm/z-clinz-10.html",
                "WS:https://www.drugs.com/mtm/z-clinz-5.html",
                "WS:https://www.drugs.com/acanya.html",
                "WS:https://www.drugs.com/benzaclin.html",
                "WS:https://www.drugs.com/mtm/acanya.html",
                "WS:https://www.drugs.com/mtm/benzoyl-peroxide-and-clindamycin-topical.html",
                "WS:https://www.drugs.com/mtm/duac.html",
                "https://www.drugs.com/mtm/benzoyl-peroxide-and-clindamycin-topical.html"
            };


            urls = urls.OrderBy(t => t).ToList();
            urls.ForEach(Console.WriteLine);
        }

        public static void WhereOrder()
        {
            string s = @"
                [2018-03-05 23:56:29Z] Starting job 7f088894-4f99-40de-8277-bceabda1e43d@@@Triage_analysis@@@00f39dba@@@3-5-2018_11-55-16_PM
[2018-03-05 23:56:29Z] Command line parsed successfully.
[2018-03-05 23:56:29Z] Job initialization complete
[2018-03-05 23:56:29Z] Executing job with commandline: SCOPESCRIPT PARAM_RelativeTriageAnalysisOutputFolder=""localSatoriOnboardingToolstcazr-264WrapStarProdWrapStar-FullWrapStar_RedfinTriageAnalysis195.8.0VS195.7.0"" PARAM_DebugFolderRelativePath=""sharesEntityPlatformlocalEntitySpace-ProdWrapStarProdBaseStreamFolderWrapStar-Fullv195_8WrapStar_RedfinDebug"" PARAM_StandardViewRelativeStreamPath=""sharesEntityPlatformlocalEntitySpace-ProdWrapStarProdBaseStreamFolderWrapStar-Fullv195_7WrapStar_RedfinView.Combined.ss"" PARAM_TriagedViewRelativeStreamPath=""sharesEntityPlatformlocalEntitySpace-ProdWrapStarProdBaseStreamFolderWrapStar-Fullv195_8WrapStar_RedfinView.Combined.ss"" VC=vc:cosmos08Knowledge RETRIES=2 SCOPEPARAM=-vcp 5
[2018-03-05 23:56:30Z] Submitting script...
[2018-03-05 23:56:30Z] Setting up working directory: D:dataScopeCloudWorker7f088
[2018-03-05 23:56:30Z] Module copied from D:dataScopeCloudWorker7f088a13fee69-c1d3-41e7-abd3-88ebf60cccc1
[2018-03-05 23:56:30Z] Script loaded to memory: D:dataScopeCloudWorker7f088script.script
[2018-03-05 23:56:30Z] Replacing @@RelativeTriageAnalysisOutputFolder@@ with ""localSatoriOnboardingToolstcazr-264WrapStarProdWrapStar-FullWrapStar_RedfinTriageAnalysis195.8.0VS195.7.0""
[2018-03-05 23:56:30Z] Replacing @@DebugFolderRelativePath@@ with ""sharesEntityPlatformlocalEntitySpace-ProdWrapStarProdBaseStreamFolderWrapStar-Fullv195_8WrapStar_RedfinDebug""
[2018-03-05 23:56:30Z] Replacing @@StandardViewRelativeStreamPath@@ with ""sharesEntityPlatformlocalEntitySpace-ProdWrapStarProdBaseStreamFolderWrapStar-Fullv195_7WrapStar_RedfinView.Combined.ss""
[2018-03-05 23:56:30Z] Replacing @@TriagedViewRelativeStreamPath@@ with ""sharesEntityPlatformlocalEntitySpace-ProdWrapStarProdBaseStreamFolderWrapStar-Fullv195_8WrapStar_RedfinView.Combined.ss""
[2018-03-05 23:56:30Z] Submission script written to working directory.
[2018-03-05 23:56:30Z] Command to run: D:dataScopeCloudWorker7f088scope.exe submit -i submission_script.script -vc https:cosmos08.osdinfra.netcosmosKnowledge -f ""jixge$AEther-7f088894-4f99-40de-8277-bceabda1e43d@@@Triage_analysis@@@00f39dba@@@3-5-2018_11-55-16_PM"" -jobId 3b93223d-d037-44ce-aa19-d0dacf428ad0 -vcp 5 -property ""JobOwner=""STCA Core Ranking"""" -p 1000 -certificateIssuer APCA-BN2
[2018-03-05 23:56:30Z] Waiting for submission to complete...
[2018-03-05 23:57:03Z] Scope job submitted with guid: 3b93223d-d037-44ce-aa19-d0dacf428ad0
[2018-03-05 23:59:05Z] Starting job 7f088894-4f99-40de-8277-bceabda1e43d@@@Triage_analysis@@@00f39dba@@@3-5-2018_11-55-16_PM
[2018-03-05 23:59:05Z] Command line parsed successfully.
[2018-03-05 23:59:05Z] Job initialization complete
[2018-03-05 23:59:05Z] Executing job with commandline: SCOPESCRIPT PARAM_RelativeTriageAnalysisOutputFolder=""localSatoriOnboardingToolstcazr-264WrapStarProdWrapStar-FullWrapStar_RedfinTriageAnalysis195.8.0VS195.7.0"" PARAM_DebugFolderRelativePath=""sharesEntityPlatformlocalEntitySpace-ProdWrapStarProdBaseStreamFolderWrapStar-Fullv195_8WrapStar_RedfinDebug"" PARAM_StandardViewRelativeStreamPath=""sharesEntityPlatformlocalEntitySpace-ProdWrapStarProdBaseStreamFolderWrapStar-Fullv195_7WrapStar_RedfinView.Combined.ss"" PARAM_TriagedViewRelativeStreamPath=""sharesEntityPlatformlocalEntitySpace-ProdWrapStarProdBaseStreamFolderWrapStar-Fullv195_8WrapStar_RedfinView.Combined.ss"" VC=vc:cosmos08Knowledge RETRIES=2 SCOPEPARAM=-vcp 5
[2018-03-05 23:59:06Z] Submitting script...
[2018-03-05 23:59:06Z] Setting up working directory: D:dataScopeCloudWorker7f088
[2018-03-05 23:59:06Z] Module copied from D:dataScopeCloudWorker7f088a13fee69-c1d3-41e7-abd3-88ebf60cccc1
[2018-03-05 23:59:07Z] Script loaded to memory: D:dataScopeCloudWorker7f088script.script
[2018-03-05 23:59:07Z] Replacing @@RelativeTriageAnalysisOutputFolder@@ with ""localSatoriOnboardingToolstcazr-264WrapStarProdWrapStar-FullWrapStar_RedfinTriageAnalysis195.8.0VS195.7.0""
[2018-03-05 23:59:07Z] Replacing @@DebugFolderRelativePath@@ with ""sharesEntityPlatformlocalEntitySpace-ProdWrapStarProdBaseStreamFolderWrapStar-Fullv195_8WrapStar_RedfinDebug""
[2018-03-05 23:59:07Z] Replacing @@StandardViewRelativeStreamPath@@ with ""sharesEntityPlatformlocalEntitySpace-ProdWrapStarProdBaseStreamFolderWrapStar-Fullv195_7WrapStar_RedfinView.Combined.ss""
[2018-03-05 23:59:07Z] Replacing @@TriagedViewRelativeStreamPath@@ with ""sharesEntityPlatformlocalEntitySpace-ProdWrapStarProdBaseStreamFolderWrapStar-Fullv195_8WrapStar_RedfinView.Combined.ss""
[2018-03-05 23:59:07Z] Submission script written to working directory.
[2018-03-05 23:59:07Z] Command to run: D:dataScopeCloudWorker7f088scope.exe submit -i submission_script.script -vc https:cosmos08.osdinfra.netcosmosKnowledge -f ""jixge$AEther-7f088894-4f99-40de-8277-bceabda1e43d@@@Triage_analysis@@@00f39dba@@@3-5-2018_11-55-16_PM"" -jobId ba343b53-0f59-4033-b27c-acf2b94fa6bf -vcp 5 -property ""JobOwner=""STCA Core Ranking"""" -p 1000 -certificateIssuer APCA-BN2
[2018-03-05 23:59:07Z] Waiting for submission to complete...
[2018-03-05 23:59:45Z] Scope job submitted with guid: ba343b53-0f59-4033-b27c-acf2b94fa6bf
[2018-03-06 00:01:20Z] Job status checked: (aether: Running) (scope: Running)
[2018-03-06 00:16:34Z] Job status checked: (aether: Running) (scope: Running)
[2018-03-06 00:32:36Z] Job status checked: (aether: Running) (scope: Running)
[2018-03-06 00:35:32Z] Job status checked: (aether: Completed) (scope: CompletedSuccess)
[2018-03-06 00:35:32Z] Job is complete, preparing outputs.
[2018-03-06 00:35:32Z] Job results upload completed.
[2018-03-06 00:35:33Z] Computation Cost for job is: {
  ""PNHoursInMiliseconds"": 162832193.6887,
  ""CosmosDataWritten"": 1306680654.0
}.";

            var stdOutInfos = s.Split(new[] { "\r\n" }, StringSplitOptions.None)
                .ToList();

            var scopeJobGuidInfo =
                stdOutInfos.Where(
                    t => t.IndexOf("Scope job submitted with guid:", StringComparison.Ordinal) > 0).ToList();

            scopeJobGuidInfo.ForEach(Console.WriteLine);

            Console.WriteLine(stdOutInfos.LastOrDefault(
                t => t.IndexOf("Scope job submitted with guid:", StringComparison.Ordinal) > 0));

            var url = @"https:\\sdfsddfsfsdf";
            Console.WriteLine(url.IndexOf("http", StringComparison.CurrentCultureIgnoreCase));
        }

        public static bool IsNewViewVersion(string mappingFileViewVersion, string latestViewVersion)
        {
            mappingFileViewVersion = mappingFileViewVersion.Replace(".", "");
            latestViewVersion = latestViewVersion.Replace(".", "");

            return int.Parse(latestViewVersion) > int.Parse(mappingFileViewVersion);
        }

        public static void IndexOfCaseInsensitive()
        {
            var comment = "No issue.";
            var pos = comment.IndexOf("no issue", StringComparison.OrdinalIgnoreCase);
            Console.WriteLine(pos);
        }
    }


}
