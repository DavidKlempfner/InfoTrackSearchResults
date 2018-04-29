using System.Web.Mvc;
using InfoTrackSearchResults.Helpers;
using InfoTrackSearchResults.Models;

namespace InfoTrackSearchResults.Controllers
{
    public class HomeController : Controller
    {        
        public ActionResult Index()
        {            
            return View();
        }

        [HttpPost]
        public ViewResult GetResults(InputFields inputFields)
        {
            if (ModelState.IsValid)
            {
                string googleSearchUrl = UrlGenerator.GenerateGoogleSearchUrl(inputFields.KeyWords);
                string webPageContent = WebPageDownloader.GetWebPageContent(googleSearchUrl);
                string rankings = ResultsGenerator.GetRankingsFormattedAsString(webPageContent, inputFields.Url);
                Results results = new Results { Rankings = rankings };
                return View("Results", results);
            }
            return View("Index");
        }
    }
}