using Domain.Models;
using Microsoft.AspNetCore.Mvc;
using Domain.Models;
using System.Diagnostics;

namespace Web.Controllers
{
    [Route("controller")]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
       
        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
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
         
         [HttpGet]
        public ActionResult Index()
        {
            var films = GetFilms(); 
            return View(films);
        }

        private List<Film> GetFilms()
        {
            
            return new List<Film>
        {
            new Film { Name = "Film 1 �����������������������������������������������", ImageUrl = "https://independent-thinkers.co.uk/wp-content/uploads/2022/02/Free-Online-Courses-with-Certificates.jpg" },
            new Film { Name = "Film 2 ooooooooooooooooooooooooooooooooooooooooooooooooo", ImageUrl = "/images/film2.jpg" },
            new Film { Name = "Film 3", ImageUrl = "/images/film3.jpg" },
            new Film { Name = "Film 4", ImageUrl = "/images/film3.jpg" },

        };
        }
    }


}
