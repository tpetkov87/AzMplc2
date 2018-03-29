using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Telerik.Sitefinity.Localization.UrlLocalizationStrategies;

namespace SitefinityWebApp.CustomizationTests
{
    public class CustomUrlLocalizationStrategy : SubFolderUrlLocalizationStrategy
    {
        protected override string BuildLanguageSetting(ILanguageCultures language)
        {
            return language.UICulture.Name.Replace('-', '/').ToLowerInvariant();
        }
    }

    public class CustomUrlLocalizationStrategy1 : IUrlLocalizationStrategy
    {
        public void Initialize(IUrlLocalizationContext context)
        {
            this.initialized = true;
        }

        public string UnResolveUrl(string url, out System.Globalization.CultureInfo culture, out System.Globalization.CultureInfo uiCulture)
        {
            culture = System.Globalization.CultureInfo.CurrentCulture;
            uiCulture = System.Globalization.CultureInfo.CurrentUICulture;
            return url;
        }

        public string ResolveUrl(string url)
        {
            return url;
        }

        public string ResolveUrl(string url, System.Globalization.CultureInfo targetCulture)
        {
            return url;
        }

        private bool initialized;

    }
}