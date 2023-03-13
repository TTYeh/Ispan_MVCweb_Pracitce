using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Ispan_AspNetWeb_firstPractice.Controllers
{
    public class ProductController : Controller
    {
        // GET: Product
        public ActionResult List()
        {
            dbDemoEntities1 db = new dbDemoEntities1();
            var datas = from p in db.Products
                        select p;
            return View(datas);
        }
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Create(Products p)
        {
            dbDemoEntities1 db = new dbDemoEntities1();
            db.Products.Add(p);
            db.SaveChanges();
            return RedirectToAction("List");
        }
        public ActionResult Delete(int? id)
        {
            if (id == null) return RedirectToAction("List");
            
            dbDemoEntities1 db = new dbDemoEntities1();
            Products prod = db.Products.FirstOrDefault(p => p.fId == id);
            if (prod == null) return RedirectToAction("List");
            db.Products.Remove(prod);
            db.SaveChanges();
            return RedirectToAction("List");
        }
    }
}