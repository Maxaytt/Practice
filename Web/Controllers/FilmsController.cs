using Infrastructure;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Domain.Models;
using System;
using System.Linq;

namespace Web.Controllers;

[Route("[controller]")]
public class FilmsController : ControllerBase
{
private readonly AppDbContext _dbContext;

public FilmsController(AppDbContext dbContext)
{
   _dbContext = dbContext;
}
            
[HttpGet]
 public IActionResult GetAll()
{
 var films = _dbContext.Films.ToList();
 return Ok(films);
}    

[HttpGet("{id}")]
public IActionResult GetById(Guid id)
{
 var film = _dbContext.Films.Find(id);
 if (film==null)
   return NotFound();

 return Ok(film);   
}

[HttpPost]
[Authorize]
public IActionResult Create([FromBody] Film film)
{
  film.Id = Guid.NewGuid();
  _dbContext.Films.Add(film);
  _dbContext.SaveChanges();

 return CreatedAtAction(nameof(GetById),new {id = film.Id},film);
}

[HttpPut("{id}")]
[Authorize]
public IActionResult Update(Guid id,[FromBody] Film film)
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

  return NoContent();
}
          
[HttpDelete("{id}")]
[Authorize]
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
}    

