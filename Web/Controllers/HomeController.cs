using Domain.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using Microsoft.EntityFrameworkCore;
using Infrastructure;
using Domain.ViewModel;
using Mapster;

namespace Web.Controllers;

[Route("controller")]
[Authorize]
public class HomeController : Controller
{
    private AppDbContext dbContext { get; set; }
    public HomeController(AppDbContext _dbContext)
    {
        dbContext = _dbContext;
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
    public async Task<ActionResult> Index()
    {
        var films = await dbContext.Films
            .Include(p => p.Image)
            .ToListAsync();

        var vmList = films.Adapt<List<FilmVm>>();

        return View(vmList);
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
            
            return new List<Film> {
            new Film { Name = "Film 1 �����������������������������������������������", ImageUrl = "https://independent-thinkers.co.uk/wp-content/uploads/2022/02/Free-Online-Courses-with-Certificates.jpg" },
            new Film { Name = "Film 2 ooooooooooooooooooooooooooooooooooooooooooooooooo", ImageUrl = "/images/film2.jpg" },
            new Film { Name = "Film 3", ImageUrl = "/images/film3.jpg" },
            new Film { Name = "Film 4", ImageUrl = "/images/film3.jpg" }};
        }
    }
}
