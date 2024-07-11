using Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Web.Controllers
{
    public class FilmController : Controller
    {
        AppDbContext dbContext;
        [HttpGet]
        public IActionResult GetFilmAsResource(Guid id)
        {
            dbContext.Films.Where(n => n.Id == id).First();
            return View();
        }
           
    }
}
