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
        if (film == null)
            return NotFound();

        return Ok(film);
    }


    [HttpGet]

    public IActionResult Create()
    {
        return View();
    }


    [HttpPost]
    public IActionResult Create(CreateEditFilmVm film)
    {
        //film.Id = Guid.NewGuid();
        //_dbContext.Films.Add(film);
        Film filmForDatabase = new Film();
        Image imageForDatabse = new Image();

        imageForDatabse.Id = film.Id;
        imageForDatabse.Caption = film.Name;
        imageForDatabse.ContentType = film.ImageFile.ContentType;

        filmForDatabase.Id = new Guid();
        filmForDatabase.Name = film.Name;
        filmForDatabase.Image = imageForDatabse;
        filmForDatabase.ContentType = film.VideoFile.ContentType;


        //Converting IFormFile to byte[] and saving into imageForDatabase's property content
        if (film.ImageFile != null)
        {
            using (var item = new MemoryStream())
            {
                film.ImageFile.CopyTo(item);
                imageForDatabse.Content = item.ToArray();
            }
        }

        //Converting IFormFile to byte[] and saving into FilmForDatabase's property content
        if (film.VideoFile != null)
        {
            using (var item = new MemoryStream())
            {
                film.VideoFile.CopyTo(item);
                filmForDatabase.Content = item.ToArray();
            }
        }


        _dbContext.Images.Add(imageForDatabse);
        _dbContext.Films.Add(filmForDatabase);
        _dbContext.SaveChanges();

        return RedirectToAction("Index", "Home");
    }

    [HttpGet("Edit/{id:guid}")]
    public IActionResult Edit(Guid id)
    {
        var film = _dbContext.Films.Find(id);
        CreateEditFilmVm ViewModel = new CreateEditFilmVm 
        {
            Name = film.Name
        };
        if (film == null)
            return NotFound();
        ViewModel.Id = id;
        return View(ViewModel);
    }


    [HttpPost("EditAndAdd")]
    public IActionResult EditAndAdd(CreateEditFilmVm ViewModel)
    {
        /*
         var existingFilm = _dbContext.Films.Find(id);
         if(existingFilm == null)
         return NotFound();

          existingFilm.Name = film.Name;
          existingFilm.Content = film.Content;
          existingFilm.Questions = film.Questions;

          _dbContext.Films.Update(existingFilm);
          _dbContext.SaveChanges();   
        */
        var existingFilm = _dbContext.Films.Include(p=>p.Image).First(p=>p.Id == ViewModel.Id);
        
        if (ViewModel.Name != null)
        {
            existingFilm.Name = ViewModel.Name;
            existingFilm.Image.Caption=ViewModel.Name;
        }
        if(ViewModel.ImageFile != null)
        {
            using (var item = new MemoryStream())
            {
                ViewModel.ImageFile.CopyTo(item);
                existingFilm.Image.Content = item.ToArray();
            }
        }

        if (ViewModel.VideoFile != null)
        {
            using (var item = new MemoryStream())
            {
                ViewModel.VideoFile.CopyTo(item);
                existingFilm.Content = item.ToArray();
            }
        }
        _dbContext.Films.Update(existingFilm);
        _dbContext.SaveChanges();
        return RedirectToAction("Index", "Home");
    }
          
    [HttpDelete("Delete/{id:guid}")]
    public IActionResult Delete(Guid id)
    {
      var film = _dbContext.Films.Find(id);
      if(film == null)
        return NotFound();

      _dbContext.Films.Remove(film);
      _dbContext.SaveChanges();

      return Ok();
    }

    [HttpGet("GetFilmAsResource/{id:guid}")]
    public IActionResult GetFilmAsResource(Guid id)
    {
      var film = _dbContext.Films.Find(id);
      if (film == null)
         return NotFound();
      return File(film.Content, film.ContentType, film.Name);
    }

    [HttpGet("GetImageAsResource/{id:guid}")]
    public IActionResult GetImageAsResource(Guid id)
    {
        var image = _dbContext.Images.Find(id);
        if (image == null)
            return NotFound();
        return File(image.Content, image.ContentType, image.Caption);
    }

    [HttpGet("PlayFilm/{id:guid}")]
    public IActionResult PlayFilm(Guid id) 
    {
        var film = _dbContext.Films.Find(id);

        if (film == null) 
            return NotFound();

        PlayFilmVm viewModel = new PlayFilmVm();
        viewModel.Id = id;
        viewModel.Name = film.Name;
        viewModel.ContentType = film.ContentType;
        return View(viewModel);
    }
}    

