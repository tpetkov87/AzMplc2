using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Sitefinity.Web.UI;
using System.IO;
using System.Text.RegularExpressions;
using Telerik.Sitefinity.Configuration;
using Telerik.Sitefinity.Web.Configuration;

namespace SitefinityWebApp.TemplateTests
{
    public partial class TemplateTests : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string physicalPath = Request.PhysicalPath;

            this.Page.Controls.Add(new Literal() { Text = "<table border='1'>" });
            this.Page.Controls.Add(new Literal() { Text = "<tr><td></td><td><b>ASP.NET Engine</b></td><td>Error message</td><td><b>Sitefinity engine</b></td></td><td>Sitefinity error</td></tr>" }); 

            physicalPath = physicalPath.Substring(0, physicalPath.LastIndexOf('\\') + 1);
            DirectoryInfo dir = new DirectoryInfo(physicalPath + "ascx-backup");
            int succeded = 0;
            int failed = 0;
            int aspSucceeded = 0;
            int aspFailed = 0;
            foreach (FileInfo file in dir.GetFiles())
            {
                string filePath = Request.FilePath;
                filePath = filePath.Substring(0, filePath.LastIndexOf('/') + 1);

                this.Page.Controls.Add(new Literal() { Text = "<tr>" });

                try
                {
                    LoadingWithAspNet(filePath + "ascx-backup/" + file.Name);
                    this.Page.Controls.Add(new Literal()
                    {
                        Text =
                            string.Format("<td>{0}</td><td>{1}</td><td></td>", Server.HtmlEncode(file.Name), "Success!")
                    });
                    aspSucceeded++;
                }
                catch (Exception ex)
                {
                    this.Page.Controls.Add(new Literal()
                    {
                        Text =
                            string.Format("<td>{0}</td><td>{1}</td><td>{2}</td>", Server.HtmlEncode(file.Name), "Failure!", Server.HtmlEncode(ex.ToString()))
                    });
                    aspFailed++;
                }

                // ControlUtilities.GetTemplate is the Sitefinity parser

                try
                {
                    LoadingWithSitefinityFunction(string.Format("{0}.{1}", "Telerik.Sitefinity.Resources", file.Name));
                    this.Page.Controls.Add(new Literal()
                    {
                        Text =
                            string.Format("<td>{0}</td><td></td>", "SF: Success!")
                    });
                    succeded++;
                }
                catch (Exception ex)
                {
                    this.Page.Controls.Add(new Literal()
                    {
                        Text =
                            string.Format("<td>{0}</td><td>{1}</td>", "SF: Failure!", Server.HtmlEncode(ex.ToString()))
                    });
                    failed++;
                }
            }
            this.Page.Controls.Add(new Literal() { Text = "</table>" });
            this.Page.Controls.Add(new Literal()
            {
                Text =
                    string.Format("<br>ASP {0} succeeded; {1} failed<br>", aspSucceeded, aspFailed)
            });
            this.Page.Controls.Add(new Literal()
            {
                Text =
                    string.Format("<br>Sitefinity {0} succeeded; {1} failed<br>", succeded, failed)
            });
        }

        public void LoadingWithSitefinityFunction(string templateName)
        {
            var tempTemplate = ControlUtilities.GetTemplate(null, templateName, Config.Get<ControlsConfig>().ResourcesAssemblyInfo, null);
            Control tempContainer = new Control();
            tempTemplate.InstantiateIn(tempContainer); //No such definition
        }

        public void LoadingWithAspNet(string path)
        {
            var tempTemplate = this.Page.LoadTemplate(path);
            //Control tempContainer = new Control();
            //tempTemplate.InstantiateIn(tempContainer);
        }
    }
}