using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using NoteApp.Models;
using NoteApp.ViewModels;
using System.IO;
using System.Security.Cryptography;

namespace NoteApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly IWebHostEnvironment _hostingEnvironment;
        public HomeController(IWebHostEnvironment hostingEnvironment) 
        {
            _hostingEnvironment = hostingEnvironment;
        }

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
            model.DateCreated = DateTime.Now;

            if (ModelState.IsValid)
            {
                if (model.Title == null) model.Title = "My New Note"; //itf use AppDbContext to add Id and set that Id if title is null for example "My New Note 1"

                string uniqueFileName = ProcessNoteDescription(model);

                Note note = new Note
                {
                    Title = model.Title,
                    DateCreated = model.DateCreated,
                    DescriptionFileName = uniqueFileName
                };

                //_notesRepository.Add(note); // add with entity framework

                return RedirectToAction("Index", "Home"); // change to notes list view itf
            }

            return View();
        }

        private string ProcessNoteDescription(AddNoteViewModel model)
        {
            string uniqueFileName = null;
            string title = model.Title.Replace(" ", "");

            if (model.Description != null)
            {
                string uploadsFolderPath = Path.Combine(_hostingEnvironment.WebRootPath, "Notes", "local");
                uniqueFileName = Guid.NewGuid().ToString() + "_" + title + "_" + model.DateCreated.ToShortDateString();
                string filePath = Path.Combine(uploadsFolderPath, uniqueFileName);

                FileInfo fi = new FileInfo(filePath);
  
                if (fi.Exists)  fi.Delete(); 
  
                using (StreamWriter sw = fi.CreateText())
                {
                    sw.WriteLine(model.Description);
                }
            }

            return uniqueFileName;
        }
    }
}
