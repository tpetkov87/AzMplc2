using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Web;
using Telerik.Sitefinity.Services;
using Telerik.Sitefinity.Web;

namespace SitefinityWebApp.Tests.OutputCacheTests
{
    public class OutputCacheLogger
    {
        public static void Clear()
        {
            SystemManager.GetCacheManager(CacheManagerInstance.Global).Remove(OutputCacheLogEntries);
        }

        public static void HandleOperationEndEvent(IContextOperationEndEvent @event)
        {
            var httpContext = SystemManager.CurrentHttpContext;
            if (httpContext == null)
                return;

            var cacheStatus = httpContext.Items["OutputCacheStatus"] as string;

            if (cacheStatus == null)
            {
                if (httpContext.Items[PageRouteHandler.HttpContextDataPageId] != null)
                {
                    if (httpContext.Items[PageRouteHandler.AddCacheDependencies] != null)
                        cacheStatus = "For Caching";
                    else
                        cacheStatus = "No Server Cache";
                }
            }

            if (cacheStatus != null)
            {
                OutputCacheLogger.Log.Add(new PageLogEntry()
                {
                    Url = httpContext.Request.Url.AbsolutePath,
                    ServerCache = cacheStatus.ToString(),
                    HttpStatus = @event.Status,
                    Headers = GetHeaders(httpContext.Request.Headers),
                    Domain = httpContext.Request.Url.Authority
                });
            }
        }

        public static IList<PageLogEntry> GetLog()
        {
            return Log;
        }

        private static string GetHeaders(NameValueCollection headers)
        {
            StringBuilder strBuilder = new StringBuilder();
            foreach (var key in headers.AllKeys)
            {
                if (key.Equals("Cache-Control", StringComparison.OrdinalIgnoreCase) ||
                    key.Equals("If-Modified-Since", StringComparison.OrdinalIgnoreCase) ||
                    key.Equals("If-None-Match", StringComparison.OrdinalIgnoreCase))
                    strBuilder.AppendFormat("{0}={1}; ", key, headers[key]);
            }

            return strBuilder.ToString();
        }


        private static IList<PageLogEntry> Log
        {
            get
            {
                var cacheManager = SystemManager.GetCacheManager(CacheManagerInstance.Global);
                var list = cacheManager.GetData(OutputCacheLogEntries) as IList<PageLogEntry>;
                if (list == null)
                {
                    lock (syncLock)
                    {
                        list = cacheManager.GetData(OutputCacheLogEntries) as IList<PageLogEntry>;
                        if (list == null)
                        {
                            list = new List<PageLogEntry>();
                            cacheManager.Add(OutputCacheLogEntries, list);
                        }
                    }
                }

                return list;
            }
        }

        private const string OutputCacheLogEntries = "SF_OutputCacheLogEntries";
        private static object syncLock = new object();
    }

    public class PageLogEntry
    {
        public PageLogEntry()
        {
            this.Date = DateTime.Now;
        }

        public string Url
        {
            get;
            set;
        }

        public string Headers
        {
            get;
            set;
        }

        public string ServerCache
        {
            get;
            set;
        }

        public string HttpStatus
        {
            get;
            set;
        }

        public string Domain
        {
            get;
            set;
        }

        public DateTime Date
        {
            get;
            private set;
        }
    }
}