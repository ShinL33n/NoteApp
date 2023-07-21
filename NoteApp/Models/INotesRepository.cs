namespace NoteApp.Models
{
    public interface INotesRepository
    {
        Note Add(Note note);
        Note Get(int Id);
        Note Delete(int Id);
        IEnumerable<Note> GetAll();
    }
}
