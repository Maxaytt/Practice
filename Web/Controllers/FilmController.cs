using Microsoft.AspNetCore.Mvc;

namespace Web.Controllers
{
    public class FilmController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
           
    }
}
