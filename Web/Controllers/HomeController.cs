using Domain.Models;
using Microsoft.AspNetCore.Mvc;
using Domain.Models;
using System.Diagnostics;
using Microsoft.EntityFrameworkCore;
using Infrastructure;
using Domain.ViewModel;

namespace Web.Controllers
{
    [Route("controller")]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        private AppDbContext dbContext { get; set; }


        public HomeController(ILogger<HomeController> logger, AppDbContext _dbContext)
        {
            dbContext = _dbContext;
            dbContext.Films.Include(p => p.Image);
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

        private List<FilmVm> GetFilms()
        {
            
            List<FilmVm> films = new List<FilmVm>();

            var collection = dbContext.Films
                .Include(p => p.Image)
                .Select(p => new { p.Id,
                    p.ImageId, 
                    p.Name, 
                    p.Image.ContentType, 
                    ImageContentType = p.Image.ContentType, 
                    ImageCaption = p.Image.Caption });

            foreach (var film in collection)
            {
                films.Add(new FilmVm
                {
                    FilmId = film.Id,
                    ImageId = film.ImageId,
                    Caption = film.ImageCaption,
                    ImageContentType = film.ImageContentType,
                    Name = film.Name,
                });
            }
            
            return films;
        }
    }


}
