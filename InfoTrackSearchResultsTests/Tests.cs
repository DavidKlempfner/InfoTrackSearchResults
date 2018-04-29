using InfoTrackSearchResults.Helpers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace InfoTrackSearchResultsTests
{
    [TestClass]
    public class Tests
    {
        [TestMethod]
        public void GivenKeyWordsAndMaxNumOfResultsExpectCorrectUrl()
        {
            //Arrange
            string keyWordsSpaceDelimited = "some key words";
            int maxNumResults = 20;

            //Act
            string actualUrl = UrlGenerator.GenerateGoogleSearchUrl(keyWordsSpaceDelimited, maxNumResults);

            //Assert
            string expectedUrl = "https://www.google.com.au/search?q=some+key+words&num=20";
            Assert.AreEqual(actualUrl, expectedUrl);
        }

        [TestMethod]
        public void GivenWebPageContentExpectCorrectUrlsAreExtracted()
        {
            //Arrange
            string someRandomWebPageContent = "<!doctype html><html><span class=\"Z98Wse\">Ad</span><cite>https://www.infotrack.com.au/Property_<b>Search</b></cite><span class=\"CiacGf\"></span><cite>www.saiglobal.com/<b>title</b>-<b>search</b></cite></div><table border=\"0\" cellpadding=\"0\" cellspacing=\"0\"><tr><td valign=\"top\"></html>";
            
            //Act
            List<string> urls = HtmlStringHelper.ExtractUrlsFromWebPageContent(someRandomWebPageContent);

            //Assert
            Assert.AreEqual("https://www.infotrack.com.au/Property_Search", urls[0]);
            Assert.AreEqual("www.saiglobal.com/title-search", urls[1]);
        }

        [TestMethod]
        public void GivenWebPageContentWithInfoTrackResultsExpectCorrectRankings()
        {
            //Arrange
            string someRandomWebPageContent = "<!doctype html><html><span class=\"Z98Wse\">Ad</span><cite>https://www.infotrack.com.au/Property_<b>Search</b></cite><span class=\"CiacGf\"></span><cite>www.saiglobal.com/<b>title</b>-<b>search</b></cite></div><table border=\"0\" cellpadding=\"0\" cellspacing=\"0\"><tr><td valign=\"top\"><cite>https://www.infotrack.com.au/SomePage</cite></html>";
            string infoTrackUrl = "https://www.infotrack.com.au";
            string expectedRankings = "1, 3";

            //Act
            string actualRankings = ResultsGenerator.GetRankingsFormattedAsString(someRandomWebPageContent, infoTrackUrl);

            //Assert
            Assert.AreEqual(expectedRankings, actualRankings);
        }

        [TestMethod]
        public void GivenWebPageContentWithNoInfoTrackResultsExpectCorrectRankings()
        {
            //Arrange
            string someRandomWebPageContent = "<!doctype html><html><span class=\"Z98Wse\">Ad</span><cite>https://www.website1.com.au/Property_<b>Search</b></cite><span class=\"CiacGf\"></span><cite>www.saiglobal.com/<b>title</b>-<b>search</b></cite></div><table border=\"0\" cellpadding=\"0\" cellspacing=\"0\"><tr><td valign=\"top\"><cite>https://www.website2.com.au/SomePage</cite></html>";
            string infoTrackUrl = "https://www.infotrack.com.au";

            //Act
            string actualRankings = ResultsGenerator.GetRankingsFormattedAsString(someRandomWebPageContent, infoTrackUrl);

            //Assert
            Assert.IsTrue(string.IsNullOrEmpty(actualRankings));
        }
    }
}
