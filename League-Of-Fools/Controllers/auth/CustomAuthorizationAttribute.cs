using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace RegisterAndLoginApp.Controllers
{
    internal class CustomAuthorizationAttribute : Attribute, IAuthorizationFilter
    {
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            int? username = context.HttpContext.Session.GetInt32("username");
            if (username == null)
            {
                context.Result = new RedirectResult("/Login");
            }
        }
    }
}