using Microsoft.AspNetCore.Mvc;

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
    }
}
