using Microsoft.AspNetCore.Mvc;

namespace Web.Controllers
{
    public class QuestionAndAnswerController : Controller
    {
        public IActionResult AddQuestion()
        {
            return View();
        }
    }
}
