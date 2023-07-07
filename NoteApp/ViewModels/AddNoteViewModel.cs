using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace NoteApp.ViewModels
{
    public class AddNoteViewModel
    {
        [DefaultValue("My new note")]
        public string? Title { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [DisplayName("Created on")]
        public DateTime DateCreated { get; set; }

        public string? Description { get; set; }
    }
}
