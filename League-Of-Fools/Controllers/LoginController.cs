using Microsoft.AspNetCore.Mvc;
using League_Of_Fools.Models;
using League_Of_Fools.Service;
using RegisterAndLoginApp.Controllers;
using Microsoft.AspNetCore.Http;

namespace League_Of_Fools.Controllers
{
    public class LoginController : Controller
    {
        private ISummonerService _summonerService;
        private IAccountService _accountService;
        //inject both dependencys
        public LoginController(ISummonerService summonerService, IAccountService accountService)
        {
            _summonerService = summonerService;
            _accountService = accountService;   
        }
        /// <summary>
        /// login screen
        /// </summary>
        /// <returns></returns>
        public IActionResult Index()
        {
            return View();
        }
        /// <summary>
        /// processes your login attempt, can either success or fail
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public IActionResult ProcessLogin(string username, string password)
        {
            //attempt login
            AccountModel user = _accountService.LoginAccount(username, password);
            //if null no user was found or error
            if (user == null)
            {
                //sent the user back to the loginpage with an error message
                ViewBag.Error = "Please try again";
                return View("Index");
                
            }
            else
            {
                //if a user was foud store the user ID into the users cookies
                HttpContext.Session.SetString("username", user.ID);
                //direct the user to thier account home
                return View("AccountHome", user);
            }
        }
        /// <summary>
        /// take the user to the home of the account
        /// user has to be logged in or will be directed back to the home screen
        /// </summary>
        /// <returns></returns>
        [LoggedInAuthorization]
        public IActionResult AccountHome()
        {
            //get the user by the user ID stored in cookie
            AccountModel user = _accountService.getUserByID((string)HttpContext.Session.GetString("username"));
            return View(user);
        }
        /// <summary>
        /// brings you to the create account screen
        /// </summary>
        /// <returns></returns>
        public IActionResult ProcessRegister()
        {
            return View("Register");
        }
        /// <summary>
        /// processes the regester attempt
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public IActionResult RegisterResults(string username, string password)
        {
            //create a new account
            _accountService.AddAccountAsync(new AccountModel(username, password));
            //logs user into that account
            _accountService.LoginAccount(username, password);

            return View("Index");
        }
        /// <summary>
        /// adds a player to the users list
        /// must be logged into account or will be redirected to login
        /// </summary>
        /// <param name="gameName"></param>
        /// <param name="tagLine"></param>
        /// <param name="regionalRoutingValue"></param>
        /// <param name="platformRoutingValue"></param>
        /// <returns></returns>
        [LoggedInAuthorization]
        public IActionResult AddPlayer(string gameName, string tagLine, string regionalRoutingValue, string platformRoutingValue)
        {
            //get the account by user id stored in cookies
            AccountModel user =_accountService.getUserByID((string)HttpContext.Session.GetString("username"));
            //make a temporary sommoner model to preform add to DB with only nesisary paramiters
            SummonerModel temp_summoner = new SummonerModel(gameName, tagLine, regionalRoutingValue, platformRoutingValue);

            //add the user to the list
            _accountService.AddUserToList(temp_summoner, user);
            //goes back to account home
            return Redirect("AccountHome");
        }
        /// <summary>
        /// removes a player to the users list
        /// must be logged into account or will be redirected to login
        /// </summary>
        /// <param name="gameName"></param>
        /// <param name="tagLine"></param>
        /// <param name="regionalRoutingValue"></param>
        /// <param name="platformRoutingValue"></param>
        [LoggedInAuthorization]
        public IActionResult RemovePlayer(string gameName, string tagLine, string regionalRoutingValue, string platformRoutingValue)
        {
            //get the account by user id stored in cookies
            AccountModel user = _accountService.getUserByID((string)HttpContext.Session.GetString("username"));
            //make a temporary sommoner model to preform add to DB with only nesisary paramiters
            SummonerModel temp_summoner = new SummonerModel(gameName, tagLine, regionalRoutingValue, platformRoutingValue);

            //add the user to the list
            _accountService.RemoveUserFromList(temp_summoner, user);
            return Redirect("AccountHome");
        }
        /// <summary>
        /// searches for an summoner account
        /// </summary>
        /// <param name="gameName"></param>
        /// <param name="tagLine"></param>
        /// <param name="regionalRoutingValue"></param>
        /// <param name="platformRoutingValue"></param>
        /// <returns></returns>
        public async Task<IActionResult> AccountSearch(string gameName, string tagLine, string regionalRoutingValue, string platformRoutingValue)
        {
            //creates a temporary summoner to preform a search on
            SummonerModel temp_summoner = new SummonerModel(gameName, tagLine, regionalRoutingValue, platformRoutingValue);

            //finds the user in the DB
            SummonerModel summoner = await _summonerService.GetSummonerByNameAndTagLine(temp_summoner);
            //if we could not find the user return the summoner not found page
            if (summoner == null)
            {
                return View("SummonerNotFound");
            }
            //show the showeser page
            return View("ShowUser", summoner);
        }
    }
}
