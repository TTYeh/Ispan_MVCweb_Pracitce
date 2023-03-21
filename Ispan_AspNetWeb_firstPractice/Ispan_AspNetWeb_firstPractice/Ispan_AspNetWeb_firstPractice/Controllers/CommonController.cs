using Ispan_AspNetWeb_firstPractice.Models;
using Ispan_AspNetWeb_firstPractice.Models.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Ispan_AspNetWeb_firstPractice.Controllers
{
    public class CommonController : Controller
    {
        // GET: Common
        public ActionResult Home()
        {
            return View();
        }

        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(LoginInfo vm)
        {
            CCustomerEntity user = new CCustomerRepo().queryByEmail(vm.txtAccount);
            if (user == null) return RedirectToAction("Home");
            if (user != null)
            {
                if (user.fPassword == vm.txtPassword)
                {
                    Session[cDictionary.SK_USER_LOGIN] = user;
                    return RedirectToAction("Home");
                }
            }
            return View();
        }
}
}