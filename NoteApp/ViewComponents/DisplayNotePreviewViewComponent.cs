using Microsoft.AspNetCore.Mvc;
using NoteApp.Models;

namespace NoteApp.ViewComponents
{
    public class DisplayNotePreviewViewComponent : ViewComponent
    {
        private readonly INotesRepository _notesRepository;
        public DisplayNotePreviewViewComponent(INotesRepository notesRepository)
        {
            _notesRepository = notesRepository;
        }

        public IViewComponentResult Invoke(int id)
        {
            Note note = _notesRepository.Get(id);
            return View(note);
        }
    }
}
