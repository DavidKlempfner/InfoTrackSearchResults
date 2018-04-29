using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Xml;
using System.Linq;
using InfoTrackSearchResults.Constants;
using System.Text.RegularExpressions;

namespace InfoTrackSearchResults.Helpers
{
    public static class ResultsGenerator
    {
        public static string GetRankingsFormattedAsString(string webPageContent, string url)
        {
            List<int> rankings = GetRankings(webPageContent, url);

            string rankingsSeparatedByCommas = string.Join(", ", rankings);

            return rankingsSeparatedByCommas;
        }

        private static List<int> GetRankings(string webPageContent, string url)
        {
            List<string> urlsFromWebPageContent = HtmlStringHelper.ExtractUrlsFromWebPageContent(webPageContent);

            List<int> indexesOfMatchingUrls = GetIndexesOfMatchingStrings(urlsFromWebPageContent, url);

            List<int> rankings = ConvertIndexesToRankings(indexesOfMatchingUrls);

            return rankings;
        }

        private static List<int> GetIndexesOfMatchingStrings(List<string> input, string stringToMatch)
        {
            List<int> indexes = input.Select((s, i) => new { String = s, Index = i })
                .Where(x => x.String.ToUpper().Contains(stringToMatch.ToUpper()))
                .Select(x => x.Index).ToList();

            return indexes;
        }

        private static List<int> ConvertIndexesToRankings(List<int> listOfIndexes)
        {
            return listOfIndexes.Select(x => ++x).ToList();
        }
    }
}