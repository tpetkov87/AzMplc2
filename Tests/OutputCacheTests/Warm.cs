using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using Telerik.Sitefinity.Abstractions;
using Telerik.Sitefinity.Multisite;
using Telerik.Sitefinity.Services;
using Telerik.Sitefinity.Web;

namespace SitefinityWebApp.Tests.OutputCacheTests
{
    public class Warm
    {
        public static void Selection(IEnumerable<string> urls)
        {
            foreach (var url in urls)
            {
                var uri = new Uri(url);
                var host = uri.Authority;
                var pageLocalUrl = ResolveLocalUrl(uri);
                MakeRequest(pageLocalUrl, host);
            }
        }

        public static void All()
        {
            foreach (var site in SystemManager.CurrentContext.GetSites())
                WarmupSite(site);
        }

        private static void WarmupSite(ISite site)
        {
            using (new SiteRegion(site, SiteContextResolutionTypes.ByFolder))
            {
                var siteUri = site.GetUri();
                var host = siteUri.Authority;

                var absolutePath = string.Concat("~", siteUri.AbsolutePath);

                var siteMap = SitefinitySiteMap.GetCurrentProvider();
                WarmupChildPagesRecursive((PageSiteNode)siteMap.RootNode, host, absolutePath);
            }
        }

        private static void WarmupChildPagesRecursive(PageSiteNode parentNode, string host, string absolutePath)
        {
            var siteMap = SitefinitySiteMap.GetCurrentProvider();
            foreach (var node in parentNode.ChildNodes)
            {
                var pageSiteNode = (PageSiteNode)node;
                var pageUrl = pageSiteNode.IsHomePage() ? absolutePath : pageSiteNode.Url;

                var localUrl = ResolveLocalUrl(pageUrl);

                MakeRequest(localUrl, host);

                WarmupChildPagesRecursive(pageSiteNode, host, absolutePath);
            }
        }

        private static void MakeRequest(string url, string host)
        {
            var request = (HttpWebRequest)WebRequest.Create(url);
            request.Host = host;
            request.UserAgent = "WarmupSpike";
            request.Headers.Add("WarmupCode", "1234"); // 

            try
            {
                var response = (HttpWebResponse)request.GetResponse();
                string pageContent = null;
                using (var responseStream = response.GetResponseStream())
                {
                    using (var reader = new StreamReader(responseStream))
                    {
                        pageContent = reader.ReadToEnd();
                    }
                }

                Log.Write(string.Format("The page '{0}' has been warmed up", GetLiveUrl(url, host)), System.Diagnostics.TraceEventType.Information);
            }
            catch (Exception err)
            {
                if (Exceptions.HandleException(err, ExceptionPolicyName.IgnoreExceptions))
                    throw;
            }
        }

        private static string ResolveLocalUrl(string url)
        {
            return ResolveLocalUrl(new Uri(UrlPath.ResolveAbsoluteUrl(url)));
        }

        private static string ResolveLocalUrl(Uri uri)
        {
            var uriBuilder = new UriBuilder(uri);
            uriBuilder.Host = "127.0.0.1";
            uriBuilder.Port = 80;
            return uriBuilder.Uri.AbsoluteUri;
        }

        private static string GetLiveUrl(string url, string domain)
        {
            var uriBuilder = new UriBuilder(url);
            uriBuilder.Host = domain;
            uriBuilder.Port = 80;
            return uriBuilder.Uri.AbsoluteUri;
        }
    }
}