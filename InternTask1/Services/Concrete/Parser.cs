﻿using InternTask1.Models;
using InternTask1.Services.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;

namespace InternTask1.Services.Concrete
{
    public class Parser: IParser
    {
        private static readonly Regex regHref = new Regex(@"<a[^>]+href=""([^""]+)""");
        private static readonly Regex regUrlStandart = new Regex(@"^http");

        private readonly string websiteUrl;

        public  StringBuilder Errors { get; private set; }
        public IEnumerable<Website> WebsitesInfo { get; private set; }
        public void Initialize(string url)
        {
            Errors = new StringBuilder();
            WebsitesInfo = GetUrlNStatusCode(UrlStandartization(url), Configuration.NestingDegree);
        }
        private IEnumerable<Website> GetUrlNStatusCode(string url, int nestingDegree)
        {
            var urlsNStatusCode = new List<Website>();
            try
            {
                var uri = new Uri(url, UriKind.RelativeOrAbsolute);
                int statusCode = GetStatusCode(uri);
                urlsNStatusCode.Add(new Website(url, statusCode));
                if (nestingDegree != 0 && statusCode >= 200 && statusCode < 300)
                {
                    var html = new WebClient().DownloadString(uri);
                    foreach (Match match in regHref.Matches(html))
                    {
                        var nestedUri = new Uri(match.Groups[1].ToString(), UriKind.RelativeOrAbsolute);
                        if (nestedUri.IsAbsoluteUri)
                            urlsNStatusCode.AddRange(GetUrlNStatusCode(match.Groups[1].ToString(), 
                                nestingDegree - 1));
                        else
                            urlsNStatusCode.AddRange(GetUrlNStatusCode(websiteUrl + match.Groups[1].ToString(), 
                                nestingDegree - 1));
                    }
                }
            }
            catch (Exception ex) 
            {
                Errors.Append($"{url}:\n{ex.Message}\n");
            }
            return urlsNStatusCode;
        }

        private int GetStatusCode(Uri uri)
        {
            var request = (HttpWebRequest)WebRequest.Create(uri);
            using (var response = (HttpWebResponse)request.GetResponse())
                return (int)response.StatusCode;
        }

        public IEnumerable<Website> ShieldedUrls() =>
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
