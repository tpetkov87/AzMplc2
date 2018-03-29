using System;
using System.Collections.Generic;
using System.Linq;
using Telerik.Sitefinity;
using Telerik.Sitefinity.Data;
using Telerik.Sitefinity.DynamicModules.Model;
using Telerik.Sitefinity.GenericContent.Model;
using Telerik.Sitefinity.Lifecycle;
using Telerik.Sitefinity.Model;
using Telerik.Sitefinity.Modules.News;
using Telerik.Sitefinity.News.Model;
using Telerik.Sitefinity.Taxonomies;
using Telerik.Sitefinity.Taxonomies.Model;
using Telerik.Sitefinity.Utilities.TypeConverters;

namespace SitefinityWebApp.CustomTests
{
    public partial class TaxonomyTestPage : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            var newsManager = NewsManager.GetManager();
            var result = GetTagsForDataType<Telerik.Sitefinity.News.Model.NewsItem>(newsManager.Provider.Name);
            tagsLabel.Text = string.Join(", ", result.Select(t => t.Title.ToString()).ToArray());
        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            TaxonomyManager.RecalculateStatistics(typeof(Telerik.Sitefinity.News.Model.NewsItem));

            Telerik.Sitefinity.DynamicModules.DynamicModuleManager.GetManager();
            var dynType = TypeResolutionService.ResolveType("Telerik.Sitefinity.DynamicTypes.Model.Dyn1.Dt1");
            TaxonomyManager.RecalculateStatistics(dynType);
        }

 
        protected IQueryable<FlatTaxon> GetTagsUsedInNewsItems()
        {
            var newsManager = NewsManager.GetManager();
            return GetTagsForDataType<Telerik.Sitefinity.News.Model.NewsItem>(newsManager.Provider.Name);
        }

        private IQueryable<FlatTaxon> GetTagsForDataType<TDataItem>(string itemProviderName, ContentLifecycleStatus status = ContentLifecycleStatus.Live)
        {
            var dataItemType = typeof(TDataItem).FullName;

            var taxonomyManager = TaxonomyManager.GetManager();
            var tagsTaxonomyId = TaxonomyManager.TagsTaxonomyId;

            var stats = taxonomyManager.GetStatistics()
                .Where(s =>
                    s.TaxonomyId == tagsTaxonomyId &&
                    s.DataItemType == dataItemType &&
                    s.ItemProviderName == itemProviderName &&
                    s.StatisticType == status &&
                    s.MarkedItemsCount > 0);

            var tags = taxonomyManager.GetTaxa<FlatTaxon>().Where(t => t.Taxonomy.Id == tagsTaxonomyId);

            var newsTags = from t in tags
                           join s in stats on t.Id equals s.TaxonId
                           select t;

            return newsTags.Distinct();
        }

    }
}