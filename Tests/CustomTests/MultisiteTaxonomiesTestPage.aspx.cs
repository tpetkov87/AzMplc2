using System;
using System.Collections.Generic;
using System.Linq;
using Telerik.Microsoft.Practices.Unity;
using Telerik.Sitefinity.Abstractions;
using Telerik.Sitefinity.Multisite;
using Telerik.Sitefinity.Services;
using Telerik.Sitefinity.Taxonomies;
using Telerik.Sitefinity.Taxonomies.Model;

namespace SitefinityWebApp.Tests.CustomTests
{
    public partial class MultisiteTaxonomiesTestPage : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            this.MultisiteTaxonomiesShowCase();
        }

        public void MultisiteTaxonomiesShowCase()
        {
            var taxonomyManager = TaxonomyManager.GetManager();
            var multisiteManager = MultisiteManager.GetManager();

            var sites = multisiteManager.GetSites().ToArray();
            var site1 = sites[0];
            var site2 = sites[1];
            var site3 = sites[2];

            // get categories used in all sites by default
            var categories = taxonomyManager.GetTaxonomies<HierarchicalTaxonomy>().Single(t => t.Name == "Categories");

            //get categories used in site 1
            var categoriesSite1 = taxonomyManager.GetSiteTaxonomy<HierarchicalTaxonomy>(categories.Id, site1.Id);

            //split categories for site 2
            var catergoriesSite2 = taxonomyManager.SplitSiteTaxonomy<HierarchicalTaxonomy>(categories, site2.Id);

            // copy all items below food category from categories in site 1 to categories in site 2
            this.CopyFoodCategories(categoriesSite1, catergoriesSite2, taxonomyManager);
            taxonomyManager.SaveChanges();

            // share categories used in site 2 with site 3
            taxonomyManager.UseTaxonomyInSite(catergoriesSite2, site3.Id);
            taxonomyManager.SaveChanges();

            // "Categories" taxonomy used in site3 is now the same as the one used in site2 (categoriesSite2.Id == categoriesSite3.Id)
            var categoriesSite3 = taxonomyManager.GetSiteTaxonomy<HierarchicalTaxonomy>(categories.Id, site3.Id);
        }

        public void CopyFoodCategories(HierarchicalTaxonomy sourceTaxonomy, HierarchicalTaxonomy targetTaxonomy, TaxonomyManager taxonomyManager)
        {
            // get food category
            var foodCategory = sourceTaxonomy.Taxa.Where(t => t.Name == "food").First() as HierarchicalTaxon;

            // get all children of the food category
            var foodChildren = taxonomyManager.GetTaxa<HierarchicalTaxon>().Where(t => t.Parent.Id == foodCategory.Id);

            // copy food category to the target taxonomy
            var targetFoodCategory = taxonomyManager.CopyTaxon(foodCategory, targetTaxonomy);
            
            foreach (var child in foodChildren)
            {
                // copy  category to the target taxonomy
                var targetChild = taxonomyManager.CopyTaxon(child, targetTaxonomy);

                targetChild.Parent = targetFoodCategory;
            }            
        }

        #region Old stuff

        protected virtual void UnLinkButtonOnClick(object sender, EventArgs e)
        {
            var rootTaxonomyName = this.TaxonomyNameUnlinkTextBox.Text;
            if (string.IsNullOrEmpty(rootTaxonomyName))
            {
                this.StatusLiteral.Text = "Please provide taxonomy name";
                return;
            }

            TaxonomyManager taxonomyManager = TaxonomyManager.GetManager();
            var rootTaxonomy = taxonomyManager.GetTaxonomies<FlatTaxonomy>().SingleOrDefault(t => t.Name == rootTaxonomyName && t.RootTaxonomyId == null);
            if (rootTaxonomy == null)
            {
                this.StatusLiteral.Text = "Unknown taxonomy.";
                return;
            }

            var siteId = this.SiteIdUnlinkTextBox.Text;
            Guid siteGuid = Guid.Empty;
            try
            {
                siteGuid = new Guid(siteId);
            }
            catch (Exception)
            {
                this.StatusLiteral.Text = "Incorrect site Guid";
            }

            if (siteGuid == Guid.Empty)
                this.StatusLiteral.Text = "Incorrect site Guid";

            // Links the root to the current site again
            taxonomyManager.UseTaxonomyInSite(rootTaxonomy, siteGuid);
        }

        protected virtual void UnSplitButtonOnClick(object sender, EventArgs e)
        {
            var rootTaxonomyName = this.TaxonomyNameTextBox.Text;
            if (string.IsNullOrEmpty(rootTaxonomyName))
            {
                this.StatusLiteral.Text = "Please provide taxonomy name";
                return;
            }

            TaxonomyManager taxonomyManager = TaxonomyManager.GetManager();
            var rootTaxonomy = taxonomyManager.GetTaxonomies<FlatTaxonomy>().SingleOrDefault(t => t.Name == rootTaxonomyName && t.RootTaxonomyId == null);
            if (rootTaxonomy == null)
            {
                this.StatusLiteral.Text = "Unknown taxonomy.";
                return;
            }

            MultisiteManager multisiteManager = MultisiteManager.GetManager();
            var sites = multisiteManager.GetSites().Where(s => !s.IsDefault).ToList();
            
            foreach (var site in sites)
            {
                // Links the root to the current site again
                taxonomyManager.UseTaxonomyInSite(rootTaxonomy, site.Id);
            }

            this.StatusLiteral.Text = "Successfully removed splits for flat taxonomy with name " + rootTaxonomyName;
        }

        protected virtual void GenerateFlatButtonOnClick(object sender, EventArgs e)
        {
            TaxonomyManager taxonomyManager = TaxonomyManager.GetManager();
            var taxonomyCount = taxonomyManager.GetTaxonomies<FlatTaxonomy>().Where(t => t.RootTaxonomyId == null).Count();
            var rootTaxonomyName = MultisiteTaxonomiesTestPage.GeneratedFlatTaxonomyName + (taxonomyCount + 1);
            var rootTaxonomy = taxonomyManager.CreateTaxonomy<FlatTaxonomy>();
            rootTaxonomy.Name = rootTaxonomyName;
            rootTaxonomy.Title = rootTaxonomyName;
            rootTaxonomy.TaxonName = rootTaxonomyName;

            MultisiteManager multisiteManager = MultisiteManager.GetManager();
            var sites = multisiteManager.GetSites().Where(s => !s.IsDefault).ToList();
            foreach (var site in sites)
            {
                taxonomyManager.SplitSiteTaxonomy<FlatTaxonomy>(rootTaxonomy, site.Id);
            }

            this.StatusLiteral.Text = "Successfully created flat taxonomy with name " + rootTaxonomyName;
        }

        /// <summary>
        /// Examples for managing taxonomies in multisite context
        /// </summary>
        public void TaxonomyExamples()
        {
            var taxonomyManager = TaxonomyManager.GetManager();

            var originalTaxonomyId = Guid.NewGuid();

            var currentSiteId = SystemManager.CurrentContext.CurrentSite.Id;

            var tags = taxonomyManager.GetTaxonomies<FlatTaxonomy>().Single(t => t.Name == "Tags");

            // Creates a new taxonomy and relates it to the original one. It also creates a link between the new taxonomy and the current site.
            var siteTaxonomy = taxonomyManager.SplitSiteTaxonomy<FlatTaxonomy>(tags);
            
            // Creates a new taxonomy, relates it to the original one and copies the taxa across. It also creates a link between the new taxonomy and the current site.
            taxonomyManager.SplitSiteTaxonomy<FlatTaxonomy>(tags);

            // Gets a taxonomy visible in the current site by id. If this taxonomy is split for the current site then it returns the taxonomy with the specified id,
            // otherwise get the split taxonomy related to the specified one and linked with the current site
            siteTaxonomy = taxonomyManager.GetSiteTaxonomy<FlatTaxonomy>(originalTaxonomyId);

            var tagsRootTaxonomy = taxonomyManager.GetTaxonomies<FlatTaxonomy>().SingleOrDefault(t => t.Name == "Tags" && t.RootTaxonomy == null);

            // gets 'Tags' taxonomy visible in the current site. If this taxonomy is split for the current site then it returns the 'Tags' taxonomy,
            // otherwise get the split taxonomy (ex. 'Site1Tags') linked with the current site
            var tagsTaxonomy = taxonomyManager.GetSiteTaxonomy<FlatTaxonomy>(tagsRootTaxonomy.Id);
            
            // if we need to make multisite taxonomies operations with no site context the siteId should be passed to methods
            var multisiteManager = MultisiteManager.GetManager();
            var siteId = multisiteManager.GetSites().Single(s => s.Name == "Site 2").Id;

            // Creates a new taxonomy and relates it to the original one. It also creates a link between the new taxonomy and the specified site.
            siteTaxonomy = taxonomyManager.SplitSiteTaxonomy<FlatTaxonomy>(tags, siteId);

            // Links the root to the second site again
            taxonomyManager.UseTaxonomyInSite(tags, siteId);

            // Creates a new taxonomy, relates it to the original one and copies the taxa across. It also creates a link between the new taxonomy and the specified site.
            taxonomyManager.SplitSiteTaxonomy<FlatTaxonomy>(tags, siteId);

            // Gets a taxonomy visible in the specified site by id. If this taxonomy is split for the specified site then it returns the taxonomy with the specified id,
            // otherwise get the split taxonomy related to the specified one and linked with the specified site
            siteTaxonomy = taxonomyManager.GetSiteTaxonomy<FlatTaxonomy>(originalTaxonomyId, siteId);

            // gets 'Tags' taxonomy visible in the specified site. If this taxonomy is split for the specified site then it returns the 'Tags' taxonomy,
            // otherwise get the split taxonomy (ex. 'Site1Tags') linked with the current site
            tagsTaxonomy = taxonomyManager.GetSiteTaxonomy<FlatTaxonomy>(tagsRootTaxonomy.Id, siteId);

            var site2 = multisiteManager.GetSites().Single(s => s.Name == "Site 2");
            var site3 = multisiteManager.GetSites().Single(s => s.Name == "Site 3");

            //Share a taxonomy from site 2 with site 3
            ITaxonomy taxonomyToShare = taxonomyManager.GetSiteTaxonomy<Taxonomy>(tagsRootTaxonomy.Id, site2.Id);
            taxonomyManager.UseTaxonomyInSite(taxonomyToShare, site3.Id);
        }
        /*
        /// <summary>
        /// Assigns the tags taxonomy from site2 to site3.
        /// </summary>
        public void AssignTagsFromSite2ToSite3()
        {
            var taxonomyManager = TaxonomyManager.GetManager();

            //default tags taxonomy
            var tags = taxonomyManager.GetTaxonomies<FlatTaxonomy>().Single(t => t.Name == "Tags");


            var tagsRootTaxonomy = taxonomyManager.GetTaxonomies<FlatTaxonomy>().SingleOrDefault(t => t.Name == "Tags" && t.RootTaxonomy == null);

            // site 2 tags taxonomy
            var site2TagsTaxonomy = taxonomyManager.GetSiteTaxonomy<FlatTaxonomy>(tagsRootTaxonomy.Id);

            // get tags taxonomy for site 3
            var sit3TagsTaxonomy = taxonomyManager.GetSiteTaxonomy<FlatTaxonomy>(tagsRootTaxonomy.Id);

            var multisiteManager = MultisiteManager.GetManager();
            var site3 = multisiteManager.GetSites().Single(s => s.Name == "Site 3");

            ISiteTaxonomyLinker siteTaxonomyLinker = this.GetSiteTaxonomyLinker(taxonomyManager);

            // site2TagsTaxonomy is split for site 2 && site 3 uses the default tags taxonomy
            if (site2TagsTaxonomy.Id != tags.Id && sit3TagsTaxonomy.Id == tags.Id)
            {
                // share the split taxonomy for site 2 with site 3
                siteTaxonomyLinker.UseTaxonomyInSite(site2TagsTaxonomy.Id, site3.Id);
            }
            else if (sit3TagsTaxonomy.Id == tags.Id) // site 3 has a specific tags taxonomy
            {
                // Links the root to site3 again
                siteTaxonomyLinker.UseTaxonomyInSite(tags, site3.Id);

                // share the split taxonomy for site 2 with site 3
                siteTaxonomyLinker.UseTaxonomyInSite(site2TagsTaxonomy.Id, site3.Id);
            }
        }

        public Taxonomy SplitTagsTaxonomyForSite2()
        {
            var taxonomyManager = TaxonomyManager.GetManager();

            var tags = taxonomyManager.GetTaxonomies<FlatTaxonomy>().Single(t => t.Name == "Tags");

            ISiteTaxonomyLinker siteTaxonomyLinker = this.GetSiteTaxonomyLinker(taxonomyManager);

            FlatTaxonomy site2Tags;
            if (SystemManager.CurrentContext.CurrentSite.Name == "Site2")
            {
                site2Tags = siteTaxonomyLinker.SplitSiteTaxonomy<FlatTaxonomy>(tags.Id);
            }
            else
            {
                var multisiteManager = MultisiteManager.GetManager();

                var site2 = multisiteManager.GetSites().Single(s => s.Name == "Site 2");
                site2Tags = siteTaxonomyLinker.SplitSiteTaxonomy<FlatTaxonomy>(tags.Id, site2.Id);
            }

            var site2TagsCity = taxonomyManager.CreateTaxon<FlatTaxon>();

            site2TagsCity.Title = "Clothes";
            site2TagsCity.Taxonomy = site2Tags;
            site2TagsCity.UrlName = "Clothes";
            site2TagsCity.Name = "Clothes";

            return site2Tags;
        }

        public void DuplicateCategoriesTaxonomyForSite2()
        {
            var taxonomyManager = TaxonomyManager.GetManager();

            var categories = taxonomyManager.GetTaxonomies<HierarchicalTaxonomy>().Single(t => t.Name == "Category");

            ISiteTaxonomyLinker siteTaxonomyLinker = this.GetSiteTaxonomyLinker(taxonomyManager);

            Taxonomy site2Categories;
            if (SystemManager.CurrentContext.CurrentSite.Name == "Site 2")
            {
                //duplicate the default categories to current site
                site2Categories = siteTaxonomyLinker.SplitSiteTaxonomy<Taxonomy>(categories.Id, duplicateTaxa: true);
            }
            else
            {
                var multisiteManager = MultisiteManager.GetManager();
                var site2 = multisiteManager.GetSites().Single(s => s.Name == "Site 2");

                //duplicate the default categories to 'Site 2'
                site2Categories = siteTaxonomyLinker.SplitSiteTaxonomy<Taxonomy>(categories.Id, site2.Id, true);
            }

            // get all taxa of "Category" taxonomy of 'Site 2'
            var site2TagsTaxa = taxonomyManager.GetSiteTaxaByTaxonomy<HierarchicalTaxonomy, HierarchicalTaxon>(site2Categories.Id);
        }

        public IQueryable<TaxonomyStatistic> GetSite2CityStatistics()
        {
            var taxonomyManager = TaxonomyManager.GetManager();

            var cities = taxonomyManager.GetTaxonomies<FlatTaxonomy>().Single(t => t.Name == "Cities");

            if (SystemManager.CurrentContext.CurrentSite.Name == "Site 2")
            {
                //gets the "City" taxonomy statistics for current site
                return taxonomyManager.GetSiteTaxonomyStatistics(cities.Id);
            }
            else
            {
                var multisiteManager = MultisiteManager.GetManager();
                var site2 = multisiteManager.GetSites().Single(s => s.Name == "Site 2");

                //gets the "City" taxonomy statistics for Site 2
                return taxonomyManager.GetSiteTaxonomyStatistics(cities.Id, site2.Id);
            }
        }

        private ISiteTaxonomyLinker GetSiteTaxonomyLinker(TaxonomyManager taxonomyManager)
        {
            var paramterOverrides = new ParameterOverride[] { new ParameterOverride("manager", taxonomyManager) };
            ISiteTaxonomyLinker siteTaxonomyLinker = ObjectFactory.Container.Resolve<ISiteTaxonomyLinker>(paramterOverrides);
            return siteTaxonomyLinker;
        } */
        #endregion
        private const string GeneratedFlatTaxonomyName = "GeneratedTaxonomy";

       
    }
}

