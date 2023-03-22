using Ispan_AspCoreWeb_SecProctice.Models;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Text.Json;

namespace Ispan_AspCoreWeb_SecProctice.Controllers
{
    public class AController : Controller
    {
        IWebHostEnvironment _enviro;
        public AController(IWebHostEnvironment p)
        {
            _enviro = p;
        }
        public string SayHello() {
            return "Hello from AController";
        }
        public IActionResult Index()
        {
            return View();
        }

        public ActionResult fileUploadDemo()
        {
            return View();
        }

        [HttpPost]
        public ActionResult fileUploadDemo(IFormFile photo) {

            string path = _enviro.WebRootPath + "/images/001.jpg";
            photo.CopyTo(new FileStream(path, FileMode.Create));
            return View();

        }

        public IActionResult showCountBySession()
        {
            int count = 0;
            if (HttpContext.Session.Keys.Contains("count"))
            {
                count = (int)HttpContext.Session.GetInt32("count");
            }
            count++;
            HttpContext.Session.SetInt32("count", count);
            ViewBag.count = count;
            return View();
        }
        public string DemoObj2Json()
        {
            dbDemoContext db = new dbDemoContext();
            var datas = new Customer()
            {
                FId = 1,
                FName = "John",
                FPhone = "0912345678",
                FEmail = "email@email.com"
            };
            //string json = JsonSerializer.Serialize(datas);
            return Newtonsoft.Json.JsonConvert.SerializeObject(datas);
        }
        public string DemoJson2Obj()
        {
            string json = DemoObj2Json();
            Customer x = JsonSerializer.Deserialize<Customer>(json);
            return x.FName.ToString();
            //return Newtonsoft.Json.JsonConvert.DeserializeXNode()

        }

    }
}
