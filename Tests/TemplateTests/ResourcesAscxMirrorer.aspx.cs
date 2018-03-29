using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using System.Text.RegularExpressions;

namespace SitefinityWebApp.TemplateTests
{
    public partial class AscxMirrorer : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string physicalPath = Request.PhysicalPath;
            physicalPath = physicalPath.Substring(0, physicalPath.LastIndexOf('\\') + 1);
            DirectoryInfo dir = new DirectoryInfo(physicalPath + "ascx-backup");
            this.TargetPath.Text = physicalPath + "ascx-backup";
            
            string applicationPath = Request.PhysicalApplicationPath;
            applicationPath = applicationPath.Substring(0, applicationPath.LastIndexOf('\\'));
            applicationPath = applicationPath.Substring(0, applicationPath.LastIndexOf('\\') + 1);
            this.ResourcesPath.Text = applicationPath + "Telerik.Sitefinity.Resources";
        }

        protected void OutputWriteLine(string output)
        {
            this.ResultPlaceHolder.Controls.Add(new Literal() { Text = string.Format("{0}{1}", output, "<br />") } );
        }

        protected void MirrorButton_Click(object sender, EventArgs e)
        {
            DirectoryInfo directorySource = new DirectoryInfo(this.ResourcesPath.Text);
            DirectoryInfo directoryTarget = new DirectoryInfo(this.TargetPath.Text);

            string csproj = string.Empty;
            using (TextReader input = new StreamReader(this.ResourcesPath.Text + @"\Telerik.Sitefinity.Resources.csproj"))
            {
                csproj = input.ReadToEnd();
            }

            string pattern = @"<EmbeddedResource\sInclude=" + '"' + @"([\w+.*\\]+[a-zA-Z0_9]+.ascx)" + '"';

            // Instantiate the regular expression object.
            Regex r = new Regex(pattern, RegexOptions.IgnoreCase);

            // Match the regular expression pattern against a text string.
            Match m = r.Match(csproj);

            List<string> files = new List<string>();

            int matchCount = 0;
            while (m.Success)
            {
                //OutputWriteLine("Match" + (++matchCount));
                for (int i = 1; i <= 2; i++)
                {
                    Group g = m.Groups[i];
                    //OutputWriteLine("Group" + i + "='" + g + "'");
                    CaptureCollection cc = g.Captures;
                    for (int j = 0; j < cc.Count; j++)
                    {
                        Capture c = cc[j];
                        string filesRelativePath = c.ToString();
                        //filesRelativePath = filesRelativePath.Substring(filesRelativePath.LastIndexOf('\\')+1);
                        files.Add(filesRelativePath);
                    }
                }
                m = m.NextMatch();
            }

            CopyFilesRecursively(directorySource.FullName, directorySource, directoryTarget, files);
        }

        private void CopyFilesRecursively(string sourceDir, DirectoryInfo source, DirectoryInfo target, List<string> files)
        {
            if (source.FullName.ToLower() == target.FullName.ToLower())
            {
                return;
            }

            // Check if the target directory exists, if not, create it.
            if (Directory.Exists(target.FullName) == false)
            {
                Directory.CreateDirectory(target.FullName);
            }

            string ascxNamespace = string.Empty;
            ascxNamespace = source.FullName.Substring(sourceDir.Length);
            ascxNamespace = ascxNamespace.Replace('\\', '.');
            if (ascxNamespace.Length > 0 && ascxNamespace[0] == '.')
            {
                ascxNamespace = ascxNamespace.Substring(1);
            }

            if (ascxNamespace.EndsWith("."))
            {
                ascxNamespace.Remove(ascxNamespace.Length - 1, 1);
            }

            // Copy each file into the target directory.
            foreach (FileInfo fi in source.GetFiles())
            {
                string filePath = fi.FullName;
                filePath = filePath.Substring(filePath.LastIndexOf("Telerik.Sitefinity.Resources\\") + "Telerik.Sitefinity.Resources\\".Length);
                if (fi.Name.EndsWith(".ascx") && (files.Contains(filePath)))
                {
                    OutputWriteLine(string.Format(@"Copying {0}\{1}", target.FullName, fi.Name));

                    fi.CopyTo(Path.Combine(target.ToString(), string.Format("{0}.{1}", ascxNamespace, fi.Name)), true);
                }
            }

            // Copy each subdirectory using recursion.
            foreach (DirectoryInfo diSourceSubDir in source.GetDirectories())
            {
                CopyFilesRecursively(sourceDir, diSourceSubDir, target, files);
            }
        }
    }
}