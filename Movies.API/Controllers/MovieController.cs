using Microsoft.AspNetCore.Mvc;

namespace Movies.API.Controllers
{
    public class MovieController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
