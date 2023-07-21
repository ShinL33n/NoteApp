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
        private readonly INotesRepository _notesRepository;
        public HomeController(IWebHostEnvironment hostingEnvironment, INotesRepository notesRepository)
        {
            _hostingEnvironment = hostingEnvironment;
            _notesRepository = notesRepository;
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
                if (model.Title == null) model.Title = "My New Note";
                string uniqueFileName = ProcessNoteDescription(model);

                Note note = new Note
                {
                    Title = model.Title,
                    DisplayTitle = model.Title,
                    DateCreated = model.DateCreated,
                    DescriptionFileName = uniqueFileName
                };

                _notesRepository.Add(note);

                return RedirectToAction("ViewNotes", "Home");
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

        [HttpGet]
        public ViewResult ViewNotes()
        {
            IEnumerable<Note> model = _notesRepository.GetAll();

            return View(model);
        }

        public IActionResult DisplayNotePreviewBasedOnId(int? clickedNoteId)
        {
            return ViewComponent("DisplayNotePreview", clickedNoteId);
        }

        [HttpPost]
        public IActionResult DeleteNote(int id)
        {
            _notesRepository.Delete(id);

            return View();
        }

        [HttpGet]
        public ViewResult EditNote(int id = 1)
        {
            Note note = _notesRepository.Get(id);
            EditNoteViewModel editNoteViewModel = new EditNoteViewModel
            {
                Id = note.Id,
                Title = note.DisplayTitle != null ? note.DisplayTitle : note.Title,
                DescriptionFileName = note.DescriptionFileName
            };

            return View("EditNote", editNoteViewModel);
        }

        [HttpPost]
        public IActionResult EditNote()
        {
            if (ModelState.IsValid)
            {

                //_notesRepository.Update(note);

                return RedirectToAction("ViewNotes", "Home");
            }

            return View();
        }


        // Home/ViewNotes - list of notes
        // /Home/ViewNotes/{Note_Id} - note preview
        // /Home/ViewNotes/{Note_Id}/edit (/Home/?>ViewNotes<?/edit/{Note_Id}??? ) [get] - editing existing note (with save edit [post], cancel [redirect to ViewNotes or smth] and delete [? - using delete method of INotesRepository for sure, probably next post action (post for security)] actions)
        // design: viewNotes/{note_id} blures background (design whim) and display full note description and /{note_id}/edit gets triggered after another click on displayed note area opening another clean page with edit capabilities
        // /\ learn nesting resources / model nesting routes
    }
}
