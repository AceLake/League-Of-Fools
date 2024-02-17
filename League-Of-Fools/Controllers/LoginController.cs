using Microsoft.AspNetCore.Mvc;
using League_Of_Fools.Models;
using League_Of_Fools.Service;
using RegisterAndLoginApp.Controllers;

namespace League_Of_Fools.Controllers
{
    public class LoginController : Controller
    {
        private ISummonerService _summonerService;
        private IAccountService _accountService;
        public LoginController(ISummonerService summonerService, IAccountService accountService)
        {
            _summonerService = summonerService;
            _accountService = accountService;   
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult ProcessLogin(string username, string password)
        {

            AccountModel user = _accountService.LoginAccount(username, password);

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
            //get the user by the cookie
            AccountModel user = _accountService.getUserByID((int)HttpContext.Session.GetInt32("username"));
            return View(user);
        }
        public IActionResult ProcessRegister()
        {
            return View("Register");
        }

        public IActionResult RegisterResults(string username, string password)
        {

            _accountService.AddAccount(new AccountModel(username, password));

            Console.WriteLine(_accountService.LoginAccount(username, password).ToString());

            return View("Index");
        }
        [CustomAuthorization]
        public IActionResult AddPlayer(string gameName, string tagLine, string regionalRoutingValue, string platformRoutingValue)
        {
            AccountModel user =_accountService.getUserByID((int)HttpContext.Session.GetInt32("username"));
            SummonerModel temp_summoner = new SummonerModel(gameName, tagLine, regionalRoutingValue, platformRoutingValue);

            _accountService.addUserToList(temp_summoner, user);
            return Redirect("AccountHome");
        }
        public async Task<IActionResult> AccountSearch(string gameName, string tagLine, string regionalRoutingValue, string platformRoutingValue)
        {
            SummonerModel temp_summoner = new SummonerModel(gameName, tagLine, regionalRoutingValue, platformRoutingValue);

            SummonerModel summoner = await _summonerService.GetSummonerByNameAndTagLine(temp_summoner);
            if (summoner == null)
            {
                return View("SummonerNotFound");
            }
            return View("ShowUser", summoner);
        }
    }
}
