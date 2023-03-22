using Ispan_AspCoreWeb_SecProctice.Models;
using Ispan_AspCoreWeb_SecProctice.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text.Json;
using JsonSerializer = System.Text.Json.JsonSerializer;

namespace Ispan_AspCoreWeb_SecProctice.Controllers
{
    public class ShoppingController : Controller
    {
        public IActionResult CartView() {
            if (!HttpContext.Session.Keys.Contains(CDictionary.SK_PURCHASED_PRODUCT_LIST))
                return RedirectToAction("List");

            string json = HttpContext.Session.GetString(CDictionary.SK_PURCHASED_PRODUCT_LIST);
            List<CShoppingCartItem> cart = JsonSerializer.Deserialize<List<CShoppingCartItem>>(json);
            if (CartView == null)
            {
                return RedirectToAction("List");
            }
            return View();
        }
        
        public IActionResult List()
        {
            dbDemoContext db = new dbDemoContext();
            var datas = from p in db.Products
                        select p;

            List<CProductWrap> list = new List<CProductWrap>();
            foreach (var t in datas)
            {
                CProductWrap w = new CProductWrap();
                w.product = t;
                list.Add(w);
            }
            return View(datas);
        }
        public ActionResult AddToCart(int? id)
        {
            if (id == null) return RedirectToAction("List");
            ViewBag.fId = id;
            return View();
        }


        public ActionResult AddToCart(CAddToCartViewModel vm)
        {
            dbDemoContext db = new dbDemoContext();
            Product prod = db.Products.FirstOrDefault(p => p.FId == vm.txtFId);
            if (prod == null) return RedirectToAction("List");

            List<CShoppingCartItem> cart = null;
            string json = "";
            if (HttpContext.Session.Keys.Contains(CDictionary.SK_PURCHASED_PRODUCT_LIST))
            {
                
                json = HttpContext.Session.GetString(CDictionary.SK_PURCHASED_PRODUCT_LIST);
                cart = JsonSerializer.Deserialize<List<CShoppingCartItem>>(json);
            }
            else {
                cart = new List<CShoppingCartItem>();
            }

            CShoppingCartItem item = new CShoppingCartItem();
            item.fPrice = (decimal)prod.FPrice;
            item.fProductId = vm.txtFId;
            cart.Add(item);
            json = JsonSerializer.Serialize(cart);
            HttpContext.Session.SetString(CDictionary.SK_PURCHASED_PRODUCT_LIST, json);
            return RedirectToAction("List");

        }
        
    }
}
