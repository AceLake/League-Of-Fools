using League_Of_Fools.Service;
using League_Of_Fools.Services;
using Microsoft.AspNetCore.Mvc;

namespace League_Of_Fools.Controllers
{
    public class ChampionsController : Controller
    {

        private readonly IChampionService _championService;

        public ChampionsController(IChampionService championService)
        {
            MyLogger.GetInstance().Info(this.GetType().Name, "Init Champions Service");
            _championService = championService;
        }

        public async Task<IActionResult> Index()
        {
            MyLogger.GetInstance().Info(this.GetType().Name, "In Index - Getting all Chamions");
            var champions = await _championService.GetAll();
            return View(champions);
        }

        public async Task<IActionResult> Details(int Key)
        {
            MyLogger.GetInstance().Info(this.GetType().Name, "In Details - Getting all Chamions");
            var champions = await _championService.GetAll();
            foreach (var champion in champions)
            {
                if(Key == champion.Key)
                {
                    MyLogger.GetInstance().Info(this.GetType().Name, "In Details - Champion Found - ID=" + champion.Id);
                    var champ = await _championService.GetChampionById(champion.Id);
                    return View(champ);
                }
            }
            MyLogger.GetInstance().Info(this.GetType().Name, "In Details - No Champion Found");
            return View(); 
        }

    }
}
