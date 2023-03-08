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
        // GET: Customer
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