using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ClubApp.Api.Common
{
    public class HttpRequestDetails
    {
        public string Route { get; set; }

        public string Controller { get; set; }

        public string Action { get; set; }

        public string Method { get; set; }

        public string QueryString { get; set; }

        public string Body { get; set; }
    }
}
