using Ispan_AspNetWeb_firstPractice.Models;
using Ispan_AspNetWeb_firstPractice.Models.Entity;
using Ispan_AspNetWeb_firstPractice.ViewModels;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Ispan_AspNetWeb_firstPractice.Controllers
{
    public class ShoppingController : Controller
    {
        public ActionResult CartView()
        {
            List<cShoppingItem> cart =
                Session[cDictionary.SK_PURSEASED_PRODUCTS] as List<cShoppingItem>;
            if (cart == null)
                return RedirectToAction("List");
            return View(cart);
        }
        // GET: Shopping
        public ActionResult List()
        {
            var prd = from prod in (new dbDemoEntities1()).Products
                     select prod;
            return View(prd);
        }
        public ActionResult AddToCart(int? id)
        {
            if (id == null) return RedirectToAction("List");
            ViewBag.fId = id;
            return View();
        }
        [HttpPost]
        public ActionResult AddToCart(CAddToCartModel vm)
        {
            dbDemoEntities1 db = new dbDemoEntities1();
            Products prod = db.Products.FirstOrDefault(p => p.fId == vm.txtFId);
            if (prod == null) return RedirectToAction("List");
            tshopingCart cart = new tshopingCart();
            cart.fDate = DateTime.Now.ToString("yyyy-MM-dd-HH-mm-ss");
            cart.fCustomerId = 1;
            cart.fId = vm.txtFId;
            cart.fCount = vm.txtCount;
            cart.fPrice = prod.fPrice;
            cart.fProductId = prod.fId;
            db.tshopingCart.Add(cart);
            db.SaveChanges();
            return RedirectToAction("List");
        }

        public ActionResult AddToSession(int? id)
        {
            if (id == null) return RedirectToAction("List");
            ViewBag.fId = id;
            return View();
        }

        [HttpPost]
        public ActionResult AddToSession(CAddToCartModel vm)
        {
            dbDemoEntities1 db = new dbDemoEntities1();
            Products prod = db.Products.FirstOrDefault(p => p.fId == vm.txtFId);
            if (prod == null) return RedirectToAction("List");
            // Sesstion宣告
            List<cShoppingItem> cart = Session[cDictionary.SK_PURSEASED_PRODUCTS] as List<cShoppingItem>;

            // Sesstion插值
            if (cart == null)
            {
                cart = new List<cShoppingItem>();
                Session[cDictionary.SK_PURSEASED_PRODUCTS] = cart;
            }
            cShoppingItem x = new cShoppingItem()
            {
                fProductId = vm.txtFId,
                fCount = vm.txtCount,
                fPrice = (decimal)prod.fPrice,
                product  = prod
            };
            cart.Add(x);
            return View();
        }
    }
}