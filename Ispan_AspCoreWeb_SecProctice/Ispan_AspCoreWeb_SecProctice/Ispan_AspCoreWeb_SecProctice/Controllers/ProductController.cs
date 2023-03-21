using Ispan_AspCoreWeb_SecProctice.Models;
using Ispan_AspCoreWeb_SecProctice.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace Ispan_AspCoreWeb_SecProctice.Controllers
{
    public class ProductController : Controller
    {
        IWebHostEnvironment _enviro;
        public ProductController(IWebHostEnvironment p) {
            _enviro = p;
        }

        public IActionResult List(CQcustomer vm)
        {
            dbDemoContext db = new dbDemoContext();
            #nullable disable   // 關閉 nullable 語法和警告
            IEnumerable<Product> datas = null;
            if (string.IsNullOrEmpty(vm.txtKeyword))
            {
                datas = from c in db.Products
                        select c;
            }
            else
            {
                datas = db.Products.Where(p => p.FName.Contains(vm.txtKeyword));
            }

            return View(datas);
        }
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("List");
            }
            dbDemoContext db = new dbDemoContext();
            #nullable disable   // 關閉 nullable 語法和警告
            Product cust = db.Products.FirstOrDefault(p => p.FId == id);
            if (cust == null) return RedirectToAction("List");
            return View(cust);
        }
        [HttpPost]
        public IActionResult Edit(Product pIn)
        {
            dbDemoContext db = new dbDemoContext();
            Product prod = db.Products.FirstOrDefault(p => p.FId == pIn.FId);
            if (prod != null)
            {
                prod.FId = pIn.FId;
                prod.FName = pIn.FName;
                prod.FPrice = pIn.FPrice;
                prod.FQty = pIn.FQty;
                // 這裡沒有~
                db.SaveChanges();
            }

            return RedirectToAction("List");
        }
        public IActionResult Delete(int? id)
        {
            dbDemoContext db = new dbDemoContext();

            Product prod = db.Products.FirstOrDefault(w => w.FId == id);

            if (prod != null)
            {
                db.Products.Remove(prod);
                db.SaveChanges();
            }
            else
            {
                return RedirectToAction("List");
            }
            return RedirectToAction("List");
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(Product vm)
        {
            dbDemoContext db = new dbDemoContext();
            db.Products.Add(vm);
            db.SaveChanges();
            return RedirectToAction("List");
        }

        
    }
}
