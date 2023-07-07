using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace NoteApp.ViewModels
{
    public class AddNoteViewModel
    {
        public string? Title { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime DateCreated { get; set; }

        [Required]
        public string Description { get; set; }

    }
}
