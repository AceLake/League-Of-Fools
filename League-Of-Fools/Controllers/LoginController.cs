using Microsoft.AspNetCore.Mvc;
using League_Of_Fools.Models;
using League_Of_Fools.Service;

namespace League_Of_Fools.Controllers
{
    public class LoginController : Controller
    {
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
                return View("AccountHome", user);
            }
        }
        public IActionResult AccountHome()
        {
            return View(User);
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
    }
}
