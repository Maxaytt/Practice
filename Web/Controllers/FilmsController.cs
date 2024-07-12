using Infrastructure;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Domain.Models;
using System;
using System.Linq;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Domain.ViewModel;

namespace Web.Controllers;

[Route("[controller]")]
public class FilmsController : Controller
{
private readonly AppDbContext _dbContext;

public FilmsController(AppDbContext dbContext)
{
   _dbContext = dbContext;
}
 /*        
[HttpGet]
 public IActionResult GetAll()
{
 var films = _dbContext.Films.ToList();
 return Ok(films);
}    
*/
[HttpGet("{id:guid}")]
public IActionResult GetById(Guid id)
{
 var film = _dbContext.Films.Find(id);
 if (film==null)
   return NotFound();

 return Ok(film);   
}


[HttpGet]

public IActionResult Create()
{
  return View();
}


[HttpPost]

public IActionResult Create(FilmUploadVM film)
{
  //film.Id = Guid.NewGuid();
  //_dbContext.Films.Add(film);
  _dbContext.SaveChanges();

return RedirectToAction("Index", "Home");
}

[HttpGet("Edit/{id:guid}")]
       
public IActionResult Edit(Guid id)
{
var film = _dbContext.Films.Find(id);
if (film == null)
  return NotFound();

  return View(film);
}


[HttpPut("{id:guid}")]

public IActionResult Edit(Guid id,[FromBody] Film film)
{
 if (id != film.Id)
   return BadRequest();


 var existingFilm = _dbContext.Films.Find(id);
 if(existingFilm == null)
 return NotFound();

  existingFilm.Name = film.Name;
  existingFilm.Content = film.Content;
  existingFilm.Questions = film.Questions;

  _dbContext.Films.Update(existingFilm);
  _dbContext.SaveChanges();   

  return RedirectToAction("Index", "Home");
}
          
[HttpDelete("{id:guid}")]

public IActionResult Delete(Guid id)
{
  var film = _dbContext.Films.Find(id);
  if(film == null)
  return NotFound();

  _dbContext.Films.Remove(film);
  _dbContext.SaveChanges();

  return NoContent();
}

[HttpGet("{id:guid}")]
public IActionResult GetFilmAsResource(Guid id)
{
  var film = _dbContext.Films.Find(id);
  if (film == null)
     return NotFound();
  return File(film.Content, "application/octet-stream", film.Name);
}

[HttpGet("{id:guid}")]
public IActionResult PlayFilm(Guid id)
{
    var film = _dbContext.Films.Find(id);
    if (film == null)
    return NotFound();
    FilmVm viewModel = new FilmVm();
    viewModel.Id = id;
    viewModel.Name = film.Name;
  return View(viewModel);
}
}    

