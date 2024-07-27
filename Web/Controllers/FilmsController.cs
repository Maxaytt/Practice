using Domain.Models;
using Domain.ViewModel;
using Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

[Route("[controller]")]
public class FilmsController : Controller
{
    private readonly AppDbContext _dbContext;

    public FilmsController(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    [HttpGet]
    public IActionResult Create()
    {
        return View(new CreateEditFilmVm());
    }

    [HttpGet("{id:guid?}")]
    public IActionResult Create(Guid? id)
    {
        if (id.HasValue)
        {
            var film = _dbContext.Films
                .Include(f => f.Questions)
                .ThenInclude(q => q.Answers)
                .FirstOrDefault(f => f.Id == id);

            if (film == null)
                return NotFound();

            var model = new CreateEditFilmVm
            {
                Id = film.Id,
                Name = film.Name,
                Questions = film.Questions.Select(q => new QuestionVm
                {
                    Id = q.Id,
                    Text = q.Text,
                    Answers = q.Answers.Select(a => new AnswerVm
                    {
                        Id = a.Id,
                        Text = a.Text,
                        IsTrue = a.IsTrue
                    }).ToList()
                }).ToList()
            };

            return View(model);
        }

        return View(new CreateEditFilmVm());
    }


    [HttpPost]
    public IActionResult Create(CreateEditFilmVm model, IFormFile? videoFile, IFormFile? imageFile)
    {
        if (!ModelState.IsValid)
        {
            return View(model);
        }

        var imageForDatabase = new Image
        {
            Id = Guid.NewGuid(),
            Caption = model.Name,
            ContentType = imageFile?.ContentType ?? "image/jpeg"
        };

        if (imageFile != null)
        {
            using var imageStream = new MemoryStream();
            imageFile.CopyTo(imageStream);
            imageForDatabase.Content = imageStream.ToArray();
        }

        var filmForDatabase = new Film
        {
            Id = Guid.NewGuid(),
            Name = model.Name,
            Image = imageForDatabase,
            ImageId = imageForDatabase.Id,
            ContentType = videoFile?.ContentType ?? "video/mp4",
            Questions = model.Questions.Select(q => new Question
            {
                Id = Guid.NewGuid(),
                Text = q.Text,
                Answers = q.Answers.Select(a => new Answer
                {
                    Id = Guid.NewGuid(),
                    Text = a.Text,
                    IsTrue = a.IsTrue
                }).ToList()
            }).ToList()
        };

        if (videoFile != null)
        {
            using var videoStream = new MemoryStream();
            videoFile.CopyTo(videoStream);
            filmForDatabase.Content = videoStream.ToArray();
        }

        _dbContext.Images.Add(imageForDatabase);
        _dbContext.Films.Add(filmForDatabase);
        _dbContext.SaveChanges();

        return RedirectToAction("Index", "Home");
    }

    [HttpGet("AddQuestion/{filmId:guid}")]
    public IActionResult AddQuestion(Guid filmId)
    {
        var question = new QuestionVm
        {
            FilmId = filmId,
            Answers = Enumerable.Range(1, 4).Select(i => new AnswerVm { Id = Guid.NewGuid() }).ToList()
        };

        return View(question);
    }

    [HttpPost("AddQuestion")]
    public IActionResult AddQuestion(QuestionVm questionVm)
    {
        var film = _dbContext.Films
            .Include(f => f.Questions)
            .FirstOrDefault(f => f.Id == questionVm.FilmId);

        if (film == null)
            return NotFound();

        // Проверяем, что только один ответ отмечен как правильный
        if (questionVm.Answers.Count(a => a.IsTrue) != 1)
        {
            ModelState.AddModelError("", "There must be exactly one correct answer.");
            return View(questionVm);
        }

        var question = new Question
        {
            FilmId = questionVm.FilmId,
            Text = questionVm.Text,
            Answers = questionVm.Answers.Select(a => new Answer
            {
                Id = Guid.NewGuid(),
                Text = a.Text,
                IsTrue = a.IsTrue
            }).ToList()
        };

        film.Questions.Add(question);
        _dbContext.SaveChanges();

        return RedirectToAction("Create", new { id = questionVm.FilmId });
    }




    [HttpGet("Edit/{id:guid}")]
    public IActionResult Edit(Guid id)
    {
        var film = _dbContext.Films
            .Include(f => f.Questions)
            .ThenInclude(q => q.Answers)
            .FirstOrDefault(f => f.Id == id);

        if (film == null)
            return NotFound();

        var model = new CreateEditFilmVm
        {
            Id = film.Id,
            Name = film.Name,
            Questions = film.Questions.Select(q => new QuestionVm
            {
                Id = q.Id,
                Text = q.Text,
                Answers = q.Answers.Select(a => new AnswerVm
                {
                    Id = a.Id,
                    Text = a.Text,
                    IsTrue = a.IsTrue
                }).ToList()
            }).ToList()
        };

        return View(model);
    }
}
