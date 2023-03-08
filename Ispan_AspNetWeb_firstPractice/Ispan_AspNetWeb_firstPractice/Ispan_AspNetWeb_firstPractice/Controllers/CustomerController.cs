using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Ispan_AspNetWeb_firstPractice.Models;

namespace Ispan_AspNetWeb_firstPractice.Controllers
{
    public class CustomerController : Controller
    {
        public ActionResult Delete(int? id) {
            if (id != null)
            {
                new CCustomerRepo().Delete((int)id);
            }
            return RedirectToAction("ListAll");
        }
        public ActionResult Save()
        {
            var data = new CCustomerEntity() 
            {
                fName = Request.Form["cName"],
                fPhone = Request.Form["cPhone"],
                fEmail = Request.Form["cEmail"],
                fAddress = Request.Form["cAddress"],
                fPassword = Request.Form["cPassword"]
            };
            var res = new CCustomerRepo().Create(data);
            return RedirectToAction("ListAll");
        }
        public ActionResult ListAll()
        {
            var data = new CCustomerRepo().GetAll();
            return View(data);
        }
        public ActionResult Create() 
        {

            return View();
        }
    }
}