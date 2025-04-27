using Microsoft.AspNetCore.Mvc;

namespace Pzpp.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            // przekierowanie na listę kontaktów
            return RedirectToAction("Index", "Contact");
        }
    }
}
