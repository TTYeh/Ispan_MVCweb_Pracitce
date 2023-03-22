using Ispan_AspCoreWeb_SecProctice.Models;
using Ispan_AspCoreWeb_SecProctice.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Diagnostics;

namespace Ispan_AspCoreWeb_SecProctice.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            if (HttpContext.Session.GetString(CDictionary.SK_LOGIN_USER) != null)
            {
                string json = HttpContext.Session.GetString(CDictionary.SK_LOGIN_USER);
                Customer user = JsonConvert.DeserializeObject<Customer>(json);
                return View(user);
            }
            else {
                return RedirectToAction("Login");
                
            }
            
            return View();
        }
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Login(CLoginViewModel vm)
        {
            Customer user = new dbDemoContext().Customers.FirstOrDefault(p => p.FEmail == vm.txtAccount && p.FPassword == vm.txtPassword);
            if (user != null) {
                string json = JsonConvert.SerializeObject(user);
                HttpContext.Session.SetString(CDictionary.SK_LOGIN_USER, json);
                return RedirectToAction("Index");
            }
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}