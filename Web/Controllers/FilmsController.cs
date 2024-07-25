using Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Domain.Models;
using Domain.ViewModel;
using Microsoft.EntityFrameworkCore;

namespace Web.Controllers;

[Route("[controller]")]
public class FilmsController : Controller
{
    private readonly AppDbContext _dbContext;

    public FilmsController(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

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
        var imageForDatabse = new Image
        {
            Id = Guid.NewGuid(),
            Caption = film.Name,
            ContentType = film.ImageFile.ContentType,
        };
        var filmForDatabase = new Film
        {
            Id = Guid.NewGuid(),
            Name = film.Name,
            Image = imageForDatabse,
            ContentType = film.VideoFile.ContentType
        };

        if (film.ImageFile is not null)
        {
            if (hasVideoFileGoodExtension(film.VideoFile))
            {
                using var imageStream = new MemoryStream();
                film.ImageFile.CopyTo(imageStream);
                imageForDatabse.Content = imageStream.ToArray();
                _dbContext.Images.Add(imageForDatabse);
                _dbContext.SaveChanges();
            }
            else
            {
                film.Communicate = "Bad image format. Upload image in one of supported formats.";
                return View(film);
            }
        }
        else
        {
            film.Communicate = "The Image hasn't been uploaded";
            return View(film);
        }

        if (film.VideoFile is not null)
        {
            if (hasVideoFileGoodExtension(film.VideoFile))
            {
                using var filmStream = new MemoryStream();
                film.VideoFile.CopyTo(filmStream);
                filmForDatabase.Content = filmStream.ToArray();
                _dbContext.Films.Add(filmForDatabase);
                _dbContext.SaveChanges();
            }
            else
            {
                film.Communicate = "Bad film format. Upload video in one of supported formats.";
                return View(film);
            }
        }
        else
        {
            film.Communicate = "The Film hasn't been uploaded";
            return View(film);
        }

        return RedirectToAction("Index", "Home");
    }

    [HttpGet("Edit/{id:guid}")]
    public IActionResult Edit(Guid id)
    {
        var film = _dbContext.Films.Find(id);
        if (film is null) return NotFound($"film with id: {id} not found");

        var viewModel = new CreateEditFilmVm 
        {
            Name = film.Name
        };
        viewModel.Id = id;
        return View(viewModel);
    }


    [HttpPost("EditAndAdd")]
    public IActionResult EditAndAdd(CreateEditFilmVm ViewModel)
    {
        var existingFilm = _dbContext.Films
            .Include(f => f.Image)
            .First(f=>f.Id == ViewModel.Id);
        
        if (ViewModel.Name is not null)
        {
            existingFilm.Name = ViewModel.Name;
            existingFilm.Image.Caption = ViewModel.Name;
        }

        if(ViewModel.ImageFile is not null)
        {
            using var item = new MemoryStream();
            ViewModel.ImageFile.CopyTo(item);
            existingFilm.Image.Content = item.ToArray();
        }

        if (ViewModel.VideoFile is not null)
        {
            using var item = new MemoryStream();
            ViewModel.VideoFile.CopyTo(item);
            existingFilm.Content = item.ToArray();
        }
        _dbContext.Films.Update(existingFilm);
        _dbContext.SaveChanges();
        return RedirectToAction("Index", "Home");
    }
          
    [HttpDelete("Delete/{id:guid}")]
    public IActionResult Delete(Guid id)
    {
      var film = _dbContext.Films.Find(id);
      if(film is null)
        return NotFound();

      _dbContext.Films.Remove(film);
      _dbContext.SaveChanges();

      return Ok();
    }

    [HttpGet("GetFilmAsResource/{id:guid}")]
    public IActionResult GetFilmAsResource(Guid id)
    {
      var film = _dbContext.Films.Find(id);
      if (film is null)
         return NotFound();
      return File(film.Content, film.ContentType, film.Name);
    }

    [HttpGet("GetImageAsResource/{id:guid}")]
    public IActionResult GetImageAsResource(Guid id)
    {
        var image = _dbContext.Images.Find(id);
        if (image is null)
            return NotFound();
        return File(image.Content, image.ContentType, image.Caption);
    }

    [HttpGet("PlayFilm/{id:guid}")]
    public IActionResult PlayFilm(Guid id) 
    {
        var film = _dbContext.Films.Find(id);

        if (film == null) 
            return NotFound();

        var viewModel = new PlayFilmVm()
        {
            Id = id,
            Name = film.Name,
            ContentType = film.ContentType,
        };
        return View(viewModel);
    }

    //check if file extension is supported
    private bool hasVideoFileGoodExtension(IFormFile file)
    {
        if (file.ContentType == "video/mp4"
            || file.ContentType == "video/ogg"
            || file.ContentType == "video/webm")
            return true;
        else
            return false;
    }

    //check if file extension is supported
    private bool hasImageFileGoodExtension(IFormFile file)
    {
        if (file.ContentType == "image/jpg" || file.ContentType == "image/png"
            || file.ContentType == "image/jpeg"
            || file.ContentType == "image/webp"
            || file.ContentType == "image/avif"
            || file.ContentType == "image/svg+xml"
            || file.ContentType == "image/x-icon"
            || file.ContentType == "image/bmp")
            return true;
        else
            return false;
    }
}    

