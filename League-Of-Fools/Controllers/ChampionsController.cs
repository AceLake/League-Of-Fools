using League_Of_Fools.Service;
using Microsoft.AspNetCore.Mvc;

namespace League_Of_Fools.Controllers
{
    public class ChampionsController : Controller
    {

        private readonly IChampionService _championService;

        public ChampionsController(IChampionService championService)
        {
            _championService = championService;
        }

        public async Task<IActionResult> Index()
        {
            var champions = await _championService.GetAll();
            return View(champions);
        }

        public async Task<IActionResult> Details(int Key)
        {
            var champions = await _championService.GetAll();
            foreach (var champion in champions)
            {
                if(Key == champion.Key)
                {
                    return View(champion);
                }
            }
            return View(); 
        }

    }
}
