using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Maps.DataPlatform.DataServices;

namespace TechTest
{
    public class FileOperation
    {
        public static readonly string NamespacesFolder = "Namespaces";

        public static readonly string VirtualPropertiesFolder = "VirtualProperties";

        public static void TestDirectory()
        {
            DirectoryInfo namespacesDirectoryInfo;
            DirectoryInfo virtualPropertiesDirectoryInfo;
            DeduceFolderStructure(@"D:\code\Ontology\Ontology\", out namespacesDirectoryInfo, out virtualPropertiesDirectoryInfo);

            FileInfo[] filesToProcess = namespacesDirectoryInfo.GetFiles(
                "*.xml",
                SearchOption.AllDirectories);

            filesToProcess.ToList().ForEach(t => Console.WriteLine(t.Name));
        }

        private static void DeduceFolderStructure(
           string folderPath,
           out DirectoryInfo namespacesDirectoryInfo,
           out DirectoryInfo virtualPropertiesDirectoryInfo)
        {
            virtualPropertiesDirectoryInfo = null;
            DirectoryInfo directoryInfo = new DirectoryInfo(folderPath);
            DirectoryInfo[] namespacesDirectoryInfos = directoryInfo.GetDirectories(NamespacesFolder,
                SearchOption.AllDirectories);
            DirectoryInfo[] virtualPropDirectoryInfos = directoryInfo.GetDirectories(VirtualPropertiesFolder,
                SearchOption.AllDirectories);
            if (virtualPropDirectoryInfos.Length == 1)
            {
                virtualPropertiesDirectoryInfo = virtualPropDirectoryInfos[0];
            }

            namespacesDirectoryInfo = namespacesDirectoryInfos.Length == 0 ? directoryInfo : namespacesDirectoryInfos[0];

            if (namespacesDirectoryInfo == directoryInfo && virtualPropertiesDirectoryInfo != null)
            {
                throw new MetadataException(
                    "Could not deduce folder structure for Mso. It should either contain Namespaces and VirtualProperties both or should not contain VirtualProperties folder");
            }
        }
    }
}
