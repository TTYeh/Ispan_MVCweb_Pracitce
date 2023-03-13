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
        public ActionResult Edit(int? id)
        {
            // 這個是Get
            if (id == null) return RedirectToAction("ListAll");
            var x = new CCustomerRepo().GetById((int)id);
            if (x == null) return RedirectToAction("ListAll");
            return View(x);
        }
        [HttpPost]
        public ActionResult Edit(CCustomerEntity x)
        {
            // 這是post 確認存檔的內容
            new CCustomerRepo().Update(x);
            return RedirectToAction("ListAll");
        }

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
            // 關鍵字會打回來這裡
            string keyword = Request.Form["txtKeyword"];
            List<CCustomerEntity> datas = null;
            if (string.IsNullOrEmpty(keyword))
            {
                datas = new CCustomerRepo().GetAll();
            }
            else 
            { 
                datas = new CCustomerRepo().queryByKeyword(keyword);
            }
            return View(datas);
        }
        //public ActionResult List()
        //{
        //    string keyword = Request.Form["txtKeyword"];
        //    List<CCustomerEntity> datas = null;
        //    if (string.IsNullOrEmpty(keyword))
        //    {
        //        datas = new CCustomerRepo().GetAll();
        //    }
        //    datas = new CCustomerRepo().queryByKeyword(keyword);
        //    return View(datas);
        //}
        public ActionResult Create() 
        {

            return View();
        }
    }
}