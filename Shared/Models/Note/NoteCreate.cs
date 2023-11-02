using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElevenNoteWebApp_2.Shared.Models.Note
{
    public class NoteCreate
    {
        [Required]
        public string Title { get; set; }
        [Required]
        public string Content { get; set; }
        public int CategoryId { get; set; }
    }
}
