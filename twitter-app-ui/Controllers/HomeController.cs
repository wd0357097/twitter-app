using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Configuration;
using twitter_app_ui.Models;
using twitter_data.Interface;

namespace twitter_app_ui.Controllers
{
    public class HomeController : Controller
    {
        private readonly IConfiguration _configuration;
        private readonly IAppStream _data;
        private IMemoryCache _cache;// This is not ideal, but without storing to a database, this is the best we can do for now

        public HomeController(IConfiguration configuration, IMemoryCache cache, IAppStream data)
        {
            _configuration = configuration;
            _cache = cache;
            _data = data;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public async Task StartProcessingAsync(CancellationToken cancellationToken)
        {
            // get the reporting data
            _data.ReportingData += (s, e) =>
            {
                var model = new ReportDataViewModel
                {
                    TotalNumberOfTweets = e.TotalNumberOfTweets,
                    AverageNumberOfTweetsPerHour = e.AverageNumberOfTweets(e.TimeCounter.TotalHours),
                    AverageNumberOfTweetsPerSecond = e.AverageNumberOfTweets(e.TimeCounter.TotalSeconds),
                    AverageNumberOfTweetsPerMinute = e.AverageNumberOfTweets(e.TimeCounter.TotalMinutes),
                    PercentOfTweetsThatContainEmojis = e.PercentOfTweetsThatContainsEmojis,
                    PercentOfTweetsThatContainPhotoUrl = e.PercentOfTweetsThatContainPhotoUrl,
                    PercentOfTweetsThatContainUrl = e.PercentOfTweetsThatContainUrl,
                    Top10EmojisInTweets = e.EmojisInTweets,
                    Top10HashTags = e.HashTagsInTweets,
                    Top10UrlTweets = e.UrlsInTweets,
                };
                _cache.Set("ReportData", model);// store it in cache
            };
            // start the stream
            await _data.StartStreamAsync(_configuration.GetSection("twitter-stream").Value, cancellationToken);
            _cache.Set("ReportData", "");// clear the cache
        }
        [HttpGet]
        public JsonResult GetReportData()
        {
            var model = _cache.Get("ReportData");
            return Json(model);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
