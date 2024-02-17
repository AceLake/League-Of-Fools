using Microsoft.AspNetCore.Mvc;
using League_Of_Fools.Models;
using League_Of_Fools.Service;
using RegisterAndLoginApp.Controllers;

namespace League_Of_Fools.Controllers
{
    public class LoginController : Controller
    {
        private ISummonerService _summonerService;
        public LoginController(ISummonerService summonerService)
        {
            _summonerService = summonerService;
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult ProcessLogin(string username, string password)
        {
            IAccountService accounts = new FakeAccountService();

            AccountModel user = accounts.LoginAccount(username, password);

            if (user == null)
            {
                ViewBag.Error = "Please try again";
                return View("Index");
                
            }
            else
            {
                HttpContext.Session.SetInt32("username", user.ID);
                return View("AccountHome", user);
            }
        }
        [CustomAuthorization]
        public IActionResult AccountHome()
        {
            IAccountService accounts = new FakeAccountService();
            //get the user by the cookie
            AccountModel user = accounts.getUserByID((int)HttpContext.Session.GetInt32("username"));
            return View(user);
        }
        public IActionResult ProcessRegister()
        {
            return View("Register");
        }

        public IActionResult RegisterResults(string username, string password)
        {
            IAccountService accounts = new FakeAccountService();

            accounts.AddAccount(new AccountModel(username, password));

            Console.WriteLine(accounts.LoginAccount(username, password).ToString());

            return View("Index");
        }
        public async Task<IActionResult> AccountSearch(string gameName, string tagLine, string regionalRoutingValue, string platformRoutingValue)
        {
            SummonerModel temp_summoner = new SummonerModel(gameName, tagLine, regionalRoutingValue, platformRoutingValue);

            SummonerModel summoner = await _summonerService.GetSummonerByNameAndTagLine(temp_summoner);
            return View("ShowUser", summoner);
        }
    }
}
