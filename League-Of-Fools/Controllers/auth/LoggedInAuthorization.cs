using League_Of_Fools.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace RegisterAndLoginApp.Controllers
{
    internal class LoggedInAuthorization : Attribute, IAuthorizationFilter
    {
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            string? username = context.HttpContext.Session.GetString("username");
            if (username == null)
            {
                MyLogger.GetInstance().Error(this.GetType().Name, "User was Logged In, redirecting to Login", "ERROR: User accessing logined in service without being logged in");
                context.Result = new RedirectResult("/Login");
            }
        }
    }
}