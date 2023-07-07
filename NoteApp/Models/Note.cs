using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace NoteApp.Models
{
    public class Note
    {
        [DefaultValue("My new note")]
        public string? Title { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime DateCreated { get; set; }

        public string? Description { get; set; }

        //public string? DescriptionPath { get; set; }



    }
}
