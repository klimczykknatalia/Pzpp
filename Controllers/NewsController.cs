using Microsoft.AspNetCore.Mvc;

namespace Pzpp.Controllers
{
    public class NewsController : Controller
    {
        // GET: /News/
        public IActionResult Index()
        {
            return View();
        }

        // GET: /News/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: /News/Create
        [HttpPost]
        public IActionResult Create(string title, string content)
        {
            // logika zapisu np. do bazy danych (tymczasowo możesz tylko wyświetlić dane)
            ViewBag.Title = title;
            ViewBag.Content = content;
            return View("Created");
        }
    }
}