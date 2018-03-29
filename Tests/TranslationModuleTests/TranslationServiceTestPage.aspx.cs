using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Sitefinity.GenericContent.Model;
using Telerik.Sitefinity.Modules.News;
using Telerik.Sitefinity.Multisite;
using Telerik.Sitefinity.Services;
using Telerik.Sitefinity.Translations;

namespace SitefinityWebApp.Tests.TranslationModuleTests
{
    public partial class TranslationServiceTestPage : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
        }

        string GetFirstNewsItemStatus()
        {
            // Get a list of news items for translation
            var newsManager = NewsManager.GetManager();
            var newsItem = newsManager.GetNewsItems().Where(i => i.Status == ContentLifecycleStatus.Master).FirstOrDefault();
            
            // Send the items to the translation agency
            return TranslationService.Current.GetItemStatus(newsItem, CultureInfo.GetCultureInfo("fr"));
        }

        /// <summary>
        /// Marks all news from the default provider for translation.
        /// They can later be send to the translation agency from Administration > Translations
        /// </summary>
        void MarkAllNewsForTranslation()
        {
            // Get a list of news items for translation
            var newsManager = NewsManager.GetManager();
            var newsItemsToTranslate = newsManager.GetNewsItems().Where(i => i.Status == ContentLifecycleStatus.Master);
            
            // Send the items to the translation agency
            TranslationService.Current.MarkForTranslation(
                newsItemsToTranslate,
                CultureInfo.GetCultureInfo("en"),
                new CultureInfo[] { CultureInfo.GetCultureInfo("fr"), CultureInfo.GetCultureInfo("de") });
        }

        /// <summary>
        /// Sends all news from the default provider to the translation agency configured as a first connector in the connectors list.
        /// </summary>
        void SendAllNewsForTranslation()
        {
            // Creates a translation project selecting the first connector 
            var projectInfo = new ProjectInfo(
                    "Translating News", // Title
                    DateTime.UtcNow, // Start date 
                    DateTime.UtcNow.AddDays(5), // Due date
                    "Translating all news from EN to FR and DE", // Description
                    TranslationService.Current.Connectors.First().Name, // connector name
                    string.Empty, // PO reference
                    string.Empty // the actual language of the source
                    );

            // Get a list of news items for translation
            var newsManager = NewsManager.GetManager();
            var newsItemsToTranslate = newsManager.GetNewsItems().Where(i => i.Status == ContentLifecycleStatus.Master);
            
            // Send the items to the translation agency
            TranslationService.Current.SendForTranslation(
                newsItemsToTranslate,
                CultureInfo.GetCultureInfo("en"),
                new CultureInfo[] { CultureInfo.GetCultureInfo("fr"), CultureInfo.GetCultureInfo("de") },
                projectInfo);
        }

        /// <summary>
        /// Marks all news from the default provider for translation.
        /// They can later be send to the translation agency from Administration > Translations
        /// </summary>
        void MarkSiteForTranslation(CultureInfo srcLang, Guid? siteId = null)
        {
            if (siteId == null)
                siteId = SystemManager.CurrentContext.CurrentSite.Id;

            using (SiteRegion.FromSiteId(siteId.Value))
            {
            }
        }


        protected void SendAllNewsForTranslationButton_Click(object sender, EventArgs e)
        {
            this.SendAllNewsForTranslation();
        }

        protected void MarkAllNewsForTranslationButton_Click(object sender, EventArgs e)
        {
            this.MarkAllNewsForTranslation();
        }

        protected void GetFirstNewsItemStatusButton_Click(object sender, EventArgs e)
        {
            this.FirstNewsItemStatusLabel.Text = this.GetFirstNewsItemStatus();
        }

        protected void MarkSiteForTranslationButton_Click(object sender, EventArgs e)
        {

        }
    }
}