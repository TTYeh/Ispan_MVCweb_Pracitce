using Ispan_AspCoreWeb_SecProctice.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Newtonsoft.Json;

namespace Ispan_AspCoreWeb_SecProctice.Controllers
{
    public class SuperController:Controller
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            base.OnActionExecuting(context);
            if (! HttpContext.Session.Keys.Contains(CDictionary.SK_LOGIN_USER) != null)
            {
                context.Result = new RedirectToRouteResult(new RouteValueDictionary(new
                {
                    controller = "Home",
                    action = "Login"
                }));
            }


        }
    }
}
