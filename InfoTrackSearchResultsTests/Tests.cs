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
            const string KeyWordsSpaceDelimited = "some key words";
            const int MaxNumOfResults = 20;

            //Act
            string actualUrl = UrlGenerator.GenerateGoogleSearchUrl(KeyWordsSpaceDelimited, MaxNumOfResults);

            //Assert
            const string ExpectedUrl = "https://www.google.com.au/search?q=some+key+words&num=20";
            Assert.AreEqual(actualUrl, ExpectedUrl);
        }

        [TestMethod]
        public void GivenWebPageContentExpectCorrectUrlsAreExtracted()
        {
            //Arrange
            const string SomeRandomWebPageContent = "<!doctype html><html><span class=\"Z98Wse\">Ad</span><cite>https://www.infotrack.com.au/Property_<b>Search</b></cite><span class=\"CiacGf\"></span><cite>www.saiglobal.com/<b>title</b>-<b>search</b></cite></div><table border=\"0\" cellpadding=\"0\" cellspacing=\"0\"><tr><td valign=\"top\"></html>";
            
            //Act
            List<string> urls = HtmlStringHelper.ExtractUrlsFromWebPageContent(SomeRandomWebPageContent);

            //Assert
            Assert.AreEqual("https://www.infotrack.com.au/Property_Search", urls[0]);
            Assert.AreEqual("www.saiglobal.com/title-search", urls[1]);
        }

        [TestMethod]
        public void GivenWebPageContentWithInfoTrackResultsExpectCorrectRankings()
        {
            //Arrange
            const string SomeRandomWebPageContent = "<!doctype html><html><span class=\"Z98Wse\">Ad</span><cite>https://www.infotrack.com.au/Property_<b>Search</b></cite><span class=\"CiacGf\"></span><cite>www.saiglobal.com/<b>title</b>-<b>search</b></cite></div><table border=\"0\" cellpadding=\"0\" cellspacing=\"0\"><tr><td valign=\"top\"><cite>https://www.infotrack.com.au/SomePage</cite></html>";
            const string InfoTrackUrl = "https://www.infotrack.com.au";
            const string ExpectedRankings = "1, 3";

            //Act
            string actualRankings = ResultsGenerator.GetRankingsFormattedAsString(SomeRandomWebPageContent, InfoTrackUrl);

            //Assert
            Assert.AreEqual(ExpectedRankings, actualRankings);
        }

        [TestMethod]
        public void GivenWebPageContentWithNoInfoTrackResultsExpectCorrectRankings()
        {
            //Arrange
            const string SomeRandomWebPageContent = "<!doctype html><html><span class=\"Z98Wse\">Ad</span><cite>https://www.website1.com.au/Property_<b>Search</b></cite><span class=\"CiacGf\"></span><cite>www.saiglobal.com/<b>title</b>-<b>search</b></cite></div><table border=\"0\" cellpadding=\"0\" cellspacing=\"0\"><tr><td valign=\"top\"><cite>https://www.website2.com.au/SomePage</cite></html>";
            const string InfoTrackUrl = "https://www.infotrack.com.au";

            //Act
            string actualRankings = ResultsGenerator.GetRankingsFormattedAsString(SomeRandomWebPageContent, InfoTrackUrl);

            //Assert
            Assert.AreEqual("0", actualRankings);
        }
    }
}
