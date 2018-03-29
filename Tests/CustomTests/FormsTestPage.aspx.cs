using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Sitefinity.Libraries.Model;
using Telerik.Sitefinity.Model.ContentLinks;
using Telerik.Sitefinity.Modules.Forms;
using Telerik.Sitefinity.Modules.Libraries;
using Telerik.Sitefinity.Utilities.TypeConverters;

namespace SitefinityWebApp.CustomTests
{
    public partial class FormsTestPage : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            this.EnsureFormEntriesFilesUniqueUrl(FormTitleTextBox.Text.Trim());
        }

        public void EnsureFormEntriesFilesUniqueUrl(string formTitle)
        {
            var formsManager = FormsManager.GetManager();
            var form = formsManager.GetForms().Where(f => f.Title == formTitle).FirstOrDefault();
            if (form == null)
                throw new Exception("Wrong form title: {0}".Arrange(formTitle));


            List<PropertyDescriptor> contentLinkProps = null;
            var formEntries = formsManager.GetFormEntries(form);
            foreach (var entry in formEntries)
            {
                if (contentLinkProps == null)
                {
                    contentLinkProps = new List<PropertyDescriptor>();
                    foreach (PropertyDescriptor prop in TypeDescriptor.GetProperties(entry))
                    {
                        if (prop.PropertyType.Equals(typeof(ContentLink[])))
                        {
                            contentLinkProps.Add(prop);
                        }
                    }
                    if (contentLinkProps.Count == 0)
                        break;
                }

                foreach (var prop in contentLinkProps)
                {
                    var contentLinks = prop.GetValue(entry) as ContentLink[];
                    if (contentLinks != null)
                    {
                        foreach (var contentLink in contentLinks)
                        {
                            var itemType = TypeResolutionService.ResolveType(contentLink.ChildItemType);
                            if (itemType.Equals(typeof(Document)))
                            {
                                var librariesManager = LibrariesManager.GetManager(contentLink.ChildItemProviderName);
                                var document = librariesManager.GetDocument(contentLink.ChildItemId);
                                var urlSuffix = "-" + entry.Id.ToString().Replace("-", "");
                                if (!document.UrlName.EndsWith(urlSuffix))
                                {
                                    var master = librariesManager.Lifecycle.GetMaster(document) as Document;
                                    AddUrlSuffix(librariesManager, master, urlSuffix);

                                    var live = librariesManager.Lifecycle.GetLive(master) as Document;
                                    if (live != null)
                                        AddUrlSuffix(librariesManager, live, urlSuffix);

                                    librariesManager.SaveChanges();
                                }
                            }
                        }
                    }
                }
            }
        }

        private void AddUrlSuffix(LibrariesManager manager, MediaContent mediaContent, string suffix)
        {
            mediaContent.UrlName += suffix;
            manager.ClearItemUrls(mediaContent);
            manager.RecompileItemUrls(mediaContent);
        }
    }
}