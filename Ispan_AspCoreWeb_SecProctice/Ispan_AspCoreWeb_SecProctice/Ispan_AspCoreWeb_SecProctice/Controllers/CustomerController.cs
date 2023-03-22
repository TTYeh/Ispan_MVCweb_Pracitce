using Microsoft.AspNetCore.Mvc;
using Ispan_AspCoreWeb_SecProctice.Models;
using Ispan_AspCoreWeb_SecProctice.ViewModels;

namespace Ispan_AspCoreWeb_SecProctice.Controllers
{
    public class CustomerController : SuperController
    {
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("List");
            }
            dbDemoContext db = new dbDemoContext();
            #nullable disable   // 關閉 nullable 語法和警告
            Customer cust = db.Customers.FirstOrDefault(p => p.FId == id);
            if (cust == null) return RedirectToAction("List");
            return View(cust);
        }
        [HttpPost]
        public IActionResult Edit(Customer pIn)
        {
            dbDemoContext db = new dbDemoContext();
            Customer cust = db.Customers.FirstOrDefault(p => p.FId == pIn.FId);
            if (cust != null) {
                cust.FId = pIn.FId;
                cust.FName = pIn.FName;
                cust.FEmail = pIn.FEmail;
                cust.FAddress = pIn.FAddress;
                cust.FPhone = pIn.FPhone;
                cust.FPassword = pIn.FPassword;
                db.SaveChanges();
            }

            return RedirectToAction("List");
        }
        public IActionResult Delete(int? id)
        {
            dbDemoContext db = new dbDemoContext();

            Customer prod = db.Customers.FirstOrDefault(w => w.FId == id);
            
            if (prod != null)
            {
                db.Customers.Remove(prod);
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
        public IActionResult Create(Customer vm)
        {
            dbDemoContext db = new dbDemoContext();
            db.Customers.Add(vm);
            db.SaveChanges();
            return RedirectToAction("List");
        }
        public IActionResult List(CQcustomer vm)
        {
            dbDemoContext db = new dbDemoContext();

            IEnumerable < Customer > datas = null;
            if (string.IsNullOrEmpty(vm.txtKeyword))
            {
                datas = from c in db.Customers
                            select c;
            }
            else { 
                datas = db.Customers.Where(p => p.FName.Contains(vm.txtKeyword) ||
                    p.FPhone.Contains(vm.txtKeyword) || 
                    p.FEmail.Contains(vm.txtKeyword) ||
                    p.FAddress.Contains(vm.txtKeyword)
                    );
            }
            
            return View(datas);
        }
    }
}
