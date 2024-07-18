using Microsoft.AspNetCore.Mvc;
using Domain.Models;
using System.Diagnostics;


namespace Web.Controllers 
{
    public class UserController : Controller
    {
        public IActionResult Index()
        { 
            return View();
        }
    }
}