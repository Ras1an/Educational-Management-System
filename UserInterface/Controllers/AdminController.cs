using Microsoft.AspNetCore.Mvc;

namespace UserInterface.Controllers
{
    public class AdminController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }



    }
}
