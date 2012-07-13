using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace fluff_ping
{
	public partial class _Default : System.Web.UI.Page
	{
		public const string UrlBase = "url";

		protected void Page_Load(object sender, EventArgs e)
		{
			var keys = HttpContext.Current.Request.QueryString.Keys.Cast<string>().Where(
				x => (!string.IsNullOrEmpty(x) && 
					x.StartsWith(UrlBase, StringComparison.InvariantCultureIgnoreCase))).ToArray();

			StringBuilder log = new StringBuilder(2048);
			foreach (var key in keys)
			{
				var url = HttpContext.Current.Request.QueryString[key];
				if (string.IsNullOrEmpty(url))
					continue;

				if (url.StartsWith("www"))
					url = "http://" + url;

				var result = SendRequest(url);

                if (log.Length > 0)
                    log.Append(", ");

                log.Append(
                    "{ \"url\": \"" + result.Url + 
                    "\", \"status\": \"" + result.Status + 
                    "\", \"time\": \"" + result.Time + "\"}");
			}
			if (keys.Length > 0)
			ReturnJSON("{ \"results\": [" + log.ToString() + "] }");
		}

        private PingResult SendRequest(string url)
		{
			try
			{
				var request = WebRequest.Create(url);

			    var watch = new Stopwatch();
                watch.Start();
				var response = (HttpWebResponse)request.GetResponse();
			    watch.Stop();
			    return new PingResult
			    {
			        Url = url, 
                    Status = response.StatusCode, 
                    Description =  response.StatusDescription,
                    Time = watch.ElapsedMilliseconds
			    };
			}
			catch (Exception e)
			{
                return new PingResult
                {
                    Url = url,
                    Status = HttpStatusCode.NotFound,
                    Description = e.Message,
                    Time = -1
                };
			}
		}

		private void ReturnJSON(string json)
		{
			Response.Clear();
			Response.ContentType = "application/json; charset=utf-8";
			Response.Write(json);
			Response.End();
		}
	}
}
