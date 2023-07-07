using Microsoft.AspNetCore.Mvc;
using NoteApp.ViewModels;

namespace NoteApp.Controllers
{
    public class HomeController : Controller
    {
        
        public ViewResult Index()
        {
            return View();
        }

        [HttpGet]
        public ViewResult AddNote()
        {
            return View();
        }

        [HttpPost]
        public IActionResult AddNote(AddNoteViewModel model)
        {
            if (ModelState.IsValid)
            {
                model.DateCreated = DateTime.Now;
                      
            }

            //return RedirectToAction("index", "home"); redirect to notes list
            return View();
        }
    }
}
