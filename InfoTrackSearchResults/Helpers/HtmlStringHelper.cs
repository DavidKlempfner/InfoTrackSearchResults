using InfoTrackSearchResults.Constants;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;

namespace InfoTrackSearchResults.Helpers
{
    public static class HtmlStringHelper
    {
        private static void RemoveStringBeforeFirstHtmlTagDelimeter(List<string> webPageContentSplitByHtmlTagDelimeter)
        {
            webPageContentSplitByHtmlTagDelimeter.RemoveAt(0);
        }

        private static string ExtractStringBeforeHtmlEndTagDelimeter(string htmlString, string htmlEndTagDelimeter)
        {
            return htmlString.Substring(0, htmlString.IndexOf(htmlEndTagDelimeter));
        }

        public static List<string> ExtractUrlsFromWebPageContent(string webPageContent)
        {
            List<string> webPageContentSplitByHtmlTagDelimeter = webPageContent.Split(new string[] { Strings.HtmlTagDelimeter }, System.StringSplitOptions.RemoveEmptyEntries).ToList();
            RemoveStringBeforeFirstHtmlTagDelimeter(webPageContentSplitByHtmlTagDelimeter);

            List<string> urlsFromWebPageContent = webPageContentSplitByHtmlTagDelimeter.Select(htmlString => ExtractStringBeforeHtmlEndTagDelimeter(htmlString, Strings.HtmlEndTagDelimeter)).ToList();
            urlsFromWebPageContent = urlsFromWebPageContent.Select(urlWithHtmlTags => StripHtml(urlWithHtmlTags)).ToList();
            return urlsFromWebPageContent;
        }

        private static string StripHtml(string input)
        {
            return Regex.Replace(input, "<.*?>", "");
        }
    }
}