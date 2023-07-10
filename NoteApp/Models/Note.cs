using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace NoteApp.Models
{
    public class Note
    {
        public int Id { get; set; }

        public string? Title { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime DateCreated { get; set; }

        [Required]
        public string DescriptionFileName { get; set; }



    }
}
