using League_Of_Fools.Models;
using League_Of_Fools.Service;
using League_Of_Fools.Services;
using Microsoft.AspNetCore.Mvc;

namespace League_Of_Fools.Controllers
{
    public class SearchPlayerController : Controller
    {
        //private ILeagueApiService _leagueApiService = new LeagueApiService();
        private ISummonerService _summonerService;
        
        public SearchPlayerController(ISummonerService summonerService)
        {
            _summonerService = summonerService;
        }

        public IActionResult Index()
        {
            MyLogger.GetInstance().Info(this.GetType().Name, "In Index");
            return View();
        }
        public async Task<IActionResult> ProssesSearch(SummonerModel temp_summoner)
        {
            MyLogger.GetInstance().Info(this.GetType().Name, "In ProccessSearch");

            SummonerModel summoner = await _summonerService.GetSummonerByNameAndTagLine(temp_summoner);

            // if the summoner was not found it will be null
            if(summoner == null)
            {
                MyLogger.GetInstance().Info(this.GetType().Name, "In ProccessSearch - Summoner Not Found");
                return View("SummonerNotFound");
            }
            MyLogger.GetInstance().Info(this.GetType().Name, "In ProccessSearch - Summoner Found - GameName=" + temp_summoner.GameName);
            summoner.GameName = temp_summoner.GameName;
            summoner.TagLine = temp_summoner.TagLine;
            summoner.RegionalRoutingValue = temp_summoner.RegionalRoutingValue;
            summoner.PlatformRoutingValue = temp_summoner.PlatformRoutingValue;
            return View(summoner);
        }
    }
}
