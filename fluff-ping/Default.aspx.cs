using System;
using System.Collections.Generic;
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

				var status = SendRequest(url);
				log.Append("{ \"url\": \"" + url + "\", \"status\": \"" + status + "\"}");
				//log.AppendFormat("{\"url\":\"{0}\", \"status\":\"{1}\"}", );
			}
			if (keys.Length > 0)
			ReturnJSON("{[" + log.ToString() + "]}");
		}

		private HttpStatusCode SendRequest(string url)
		{
			try
			{
				var request = WebRequest.Create(url);
				var response = (HttpWebResponse)request.GetResponse();
				return response.StatusCode;
			}
			catch (Exception e)
			{
				return HttpStatusCode.NotFound;
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
