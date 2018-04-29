using System.Net;

namespace InfoTrackSearchResults.Helpers
{
    public static class WebPageDownloader
    {
        public static string GetWebPageContent(string url)
        {
            using (WebClient webClient = new WebClient())
            {
                //TODO: Get input from user about which browser they use and then add HttpRequestHeader.UserAgent to webClient.
                string webPageContent = webClient.DownloadString(url);
                return webPageContent;
            }
        }
    }
}