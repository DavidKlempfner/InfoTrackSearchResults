using InfoTrackSearchResults.Constants;

namespace InfoTrackSearchResults.Helpers
{
    public static class UrlGenerator
    {
        public static string GenerateGoogleSearchUrl(string keyWordsSpaceDelimited, int maxNumOfResults = 100)
        {           
            string keyWordsPlusDelimited = ReplaceSpaceWithPlus(keyWordsSpaceDelimited);

            //TODO: Get C#6 and use string interpolation instead:
            string googleSearchUrl = string.Format(Strings.SearchUrlParameters, Strings.GoogleDomain, keyWordsPlusDelimited, maxNumOfResults);

            return googleSearchUrl;
        }

        private static string ReplaceSpaceWithPlus(string keyWordsSpaceDelimited)
        {
            string searchTermsPlusDelimited = keyWordsSpaceDelimited.Replace(Strings.Space, Strings.Plus);
            return searchTermsPlusDelimited;
        }
    }
}