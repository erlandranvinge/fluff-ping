using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;

namespace fluff_ping
{
    public class PingResult
    {
        public string Url { get; set; }
        public HttpStatusCode Status { get; set; }
        public string Description { get; set; }
        public long Time { get; set; }
    }
}