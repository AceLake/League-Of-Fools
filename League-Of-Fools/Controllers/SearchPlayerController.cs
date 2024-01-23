﻿using League_Of_Fools.Models;
using League_Of_Fools.Service;
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
            return View();
        }
        public async Task<IActionResult> ProssesSearch(SummonerModel summonerName)
        {
            SummonerModel summoner = await _summonerService.GetSummonerByName(summonerName.Name);
            return View(summoner);
        }
    }
}