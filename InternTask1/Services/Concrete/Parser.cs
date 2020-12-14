using InternTask1.Models;
using InternTask1.Services.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;

namespace InternTask1.Services.Concrete
{
    public class Parser: BaseParser
    {
        private static readonly Regex regHref = new Regex(@"<a[^>]+href=""([^""]+)""");
        private static readonly Regex regUrlStandart = new Regex(@"^http");

        private readonly string websiteUrl;

        private StringBuilder errors;
        public override StringBuilder Errors { get { return errors; } }
        public IEnumerable<Website> WebsitesInfo { get; private set; }
        public Parser(string url)
        {
            websiteUrl = UrlStandartization(url);
            errors = new StringBuilder();
            WebsitesInfo = GetUrlNStatusCode(websiteUrl, Configuration.NestingDegree);
        }
        private IEnumerable<Website> GetUrlNStatusCode(string url, int nestingDegree)
        {
            var urlsNStatusCode = new List<Website>();
            try
            {
                Uri uri = new Uri(url, UriKind.RelativeOrAbsolute);
                int statusCode = GetStatusCode(uri);
                urlsNStatusCode.Add(new Website(url, statusCode));
                if (nestingDegree != 0 && statusCode >= 200 && statusCode < 300)
                {
                    string html = new WebClient().DownloadString(uri);
                    foreach (Match match in regHref.Matches(html))
                    {
                        Uri uri1 = new Uri(match.Groups[1].ToString(), UriKind.RelativeOrAbsolute);
                        if (uri1.IsAbsoluteUri)
                            urlsNStatusCode.AddRange(GetUrlNStatusCode(match.Groups[1].ToString(), 
                                nestingDegree - 1));
                        else
                            urlsNStatusCode.AddRange(GetUrlNStatusCode(websiteUrl + match.Groups[1].ToString(), 
                                nestingDegree - 1));
                    }
                }
            }
            catch (Exception e) 
            {
                errors.Append($"{url}:\n{e.Message}\n");
            }
            return urlsNStatusCode;
        }

        private int GetStatusCode(Uri uri)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(uri);
            using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
                return (int)response.StatusCode;
        }

        public override IEnumerable<Website> ShieldedUrls() =>
            WebsitesInfo.Select(ws => new Website($"\"{ws.Url}\"", ws.StatusCode)).ToList();
        
        private string UrlStandartization(string url)
        {
            if (!regUrlStandart.IsMatch(url))
                url = $"http://{url}";
            if (url.EndsWith("/") || url.EndsWith("\\"))
                url = url.Substring(0, url.Length - 1);
            return url;
        }
    }
}
