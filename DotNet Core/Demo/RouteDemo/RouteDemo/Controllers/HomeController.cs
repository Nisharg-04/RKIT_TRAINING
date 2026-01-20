using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace RouteDemo.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return Content("Home Index");
        }

        public IActionResult About()
        {
            return Content("Home About");
        }

        public IActionResult Details(int id)
        {
            return Content($"Home Details {id}");
        }
    }

}
