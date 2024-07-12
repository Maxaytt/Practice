using Microsoft.AspNetCore.Mvc;
using Domain.Models;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System;
using Infrastructure;

namespace Web.Controllers
{
    public class UserController : Controller
    {
        private readonly AppDbContext _context;

        public UserController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var userIdString = "A845927D-D24A-4C89-9259-177E4D788E52";

            if (!Guid.TryParse(userIdString, out var userId))
            {
                return BadRequest("Invalid user ID format.");
            }

            var user = await _context.Users.FirstOrDefaultAsync(u => u.Id == userId);;

            if (user == null)
            {
                return NotFound($"User with ID '{userId}' not found.");
            }

            return View(user);
        }

        [HttpPost]
        public async Task<IActionResult> Index(User model)
        {
            if (ModelState.IsValid)
            {
                var user = await _context.Users.FirstOrDefaultAsync(u => u.Id == model.Id);
                if (user == null)
                {
                    return NotFound($"User with ID '{model.Id}' not found.");
                }

                user.FirstName = model.FirstName;
                user.LastName = model.LastName;
                user.Email = model.Email;
                user.Gmina = model.Gmina;

                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            return View(model);
        }
     // Проблемма с пост , не сохраняет в БД
       
    }
}


