using Microsoft.AspNetCore.Mvc;

namespace Ispan_AspCoreWeb_SecProctice.Controllers
{
    public class AController : Controller
    {
        public string SayHello() {
            return "Hello from AController";
        }
        public IActionResult Index()
        {
            return View();
        }
    }
}
