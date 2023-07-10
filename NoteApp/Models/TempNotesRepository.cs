namespace NoteApp.Models
{
    public class TempNotesRepository : INotesRepository
    {
        private List<Note> _notesList;

        public TempNotesRepository()
        {
            _notesList = new List<Note>()
            {
                new Note() { Title = "eg. note num 1", DateCreated = DateTime.Now, DescriptionFileName = "4d2b0dc3-006b-40ae-8ba6-175ef1980700_eg.notenum1_10.07.2023" },
                new Note() { Title = "eg. note num 2", DateCreated = DateTime.Now, DescriptionFileName = "4d2b0dc3-006b-40ae-8tg6-175ef1980700_eg.notenum2_10.07.2023" },
                new Note() { Title = "eg. note num 3", DateCreated = DateTime.Now, DescriptionFileName = "4d2b0dc3-006b-40ae-8tg6-189ef1980700_eg.notenum3_10.07.2023" }
            };
        }

        public Note Add(Note note)
        {
            note.Id = _notesList.Max(n => n.Id);
            _notesList.Add(note);
            return note;
        }

        public Note Get(int Id)
        {
            return _notesList.FirstOrDefault(n => n.Id == Id);
        }

        public IEnumerable<Note> GetAll()
        {
            return _notesList;
        }
    }
}
