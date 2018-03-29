using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SitefinityWebApp.Tests.OutputCacheTests
{
    public class PageLogEntryAggregate
    {
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

        public DateTime LastDate
        {
            get;
            set;
        }

        public string Domain
        {
            get;
            set;
        }

        public int Count
        {
            get;
            set;
        }

    } 

}
