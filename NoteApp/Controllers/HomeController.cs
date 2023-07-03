using Microsoft.AspNetCore.Mvc;

namespace NoteApp.Controllers
{
    public class HomeController : Controller
    {
        [HttpGet]
        public ViewResult Index()
        {
            return View();
        }
    }
}
