using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Ispan_AspNetWeb_firstPractice.Controllers
{
    public class GreetingController : Controller
    {
        // GET: Greeting
        public ActionResult Index()
        {
            return View();
        }
        public string sayHello()
        {
            //http://localhost:61327/Greeting/sayHello
            string Hello = "Hello World";
            return Hello;
        }
        public string letto() {
            // http://localhost:61327/Greeting/letto
            // 取1~49不重覆的array，總共取49個數字。
            string result = string.Empty;
            var rdm = new Random();
            int genStart = 1; //從1開始取
            int genEnd = 49; //取到49
            int takeTotal = 49; //總共取6個數字
            List<int> takeArray = new List<int>();
            // 取1~49不重覆的array，總共取49個數字。
            for (int i = 0; i < takeTotal; i++)
            {
                int rdmNum = rdm.Next(genStart, genEnd + 1);
                if (!takeArray.Contains(rdmNum))
                {
                    takeArray.Add(rdmNum);
                }
                else
                {
                    i--;
                }
            }
            result = String.Join(", ", takeArray);
            return result;
        }

        public string addToCart()
        {
            // http://localhost:61327/Greeting/addToCart/?pid=1
            string id = Request.QueryString["pid"];
            if (id == "0")
                return "XBox 加入購物車成功";
            if (id == "1")
                return "PS5 加入購物車成功";
            if (id == "2")
                return "Switch 加入購物車成功";
            return "找不到該產品資料";
        }
    }
}