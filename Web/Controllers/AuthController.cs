using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Domain.Models;
using Domain.ViewModels;

namespace Web.Controllers
{
    public class AuthController : Controller
    {
        private readonly SignInManager<User> _signInManager;
        private readonly UserManager<User> _userManager;
        
        public AuthController(SignInManager<User> signInManager, UserManager<User> userManager)
        {
            _signInManager = signInManager;
            _userManager = userManager;
        }
    [HttpPost]
    public async Task<IActionResult> Login(string email, string password)
    {
        var user = await signIn.UserManager.FindByEmailAsync(email);
        if (user is null) return NotFound($"User {email} not found"); 

        var result = await signIn.PasswordSignInAsync(user, password, false, false);

        if (!result.Succeeded) 
        {
            throw new Exception("Login fail");
        } 


        return RedirectToAction("Index", "Home");
    }


        [HttpGet]
        public IActionResult Register()
        {
            var model = new RegisterViewModel();
            return View("Register", model);
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View("Register", model); // Возвращаем форму регистрации с ошибками валидации
            }
       
            var user = await _userManager.FindByEmailAsync(model.Email);
            if (user != null)
            {
                ModelState.AddModelError("", $"User {model.Email} already exists");
                return View("Register", model);
            }

            user = new User
            {
                Id = Guid.NewGuid(),
                FirstName = model.FirstName,
                LastName = model.LastName,
                UserName = model.Email,
                Email = model.Email,
                Gmina = model.Gmina
            };

            var result = await _userManager.CreateAsync(user, model.Password);

            if (!result.Succeeded)
            {
                ModelState.AddModelError("", "Failed to create user");
                return View("Register", model);
            }

            return RedirectToAction("Login", "Auth");
        }
    }
    public async Task<IActionResult> Logout()
    {
        await signIn.SignOutAsync();
        return RedirectToAction(nameof(AuthController.Login), "Auth");
    }
}