using Domain.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Web.Controllers
{
    [Route("controller")]
    [Authorize]
    public class HomeController : Controller
    {
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
