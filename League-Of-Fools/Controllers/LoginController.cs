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

        public IActionResult ProcessLogin(UserModel user)
        {
            SecurityService securityService = new SecurityService();

            if (securityService.IsValid(user))
            {//add a popup that says logged in
                return RedirectToAction("Index","Home");
            }
            else
            {
                return View("LoginFailure", user);
            }
        }

        public IActionResult ProcessRegister(UserModel user)
        {
            return View();
        }

        public IActionResult RegisterResults(UserModel user)
        {

            SecurityService securityService = new SecurityService();
            SecurityDAO securityDAO = new SecurityDAO();

            // Here I check if the user is already in the database 
            // if they are it doesnt register the user
            // if they are not then it creates the user
            if (securityService.IsValid(user))
            {
                return View("LoginFailure", user);
            }
            else
            {
                securityDAO.Create(user);
                return View("RegisterSuccess", user);
            }
            
        }
    }
}
