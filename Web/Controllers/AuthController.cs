using Microsoft.AspNetCore.Mvc;
using Domain.Models;
using System.Diagnostics;


namespace Web.Controllers 
{
    public class AuthController : Controller
    {

        public IActionResult Login()
        { 
            return View("Login");
        }

        public IActionResult Register()
        {
            return View("Register");
        }

    }
}