using System.ComponentModel.DataAnnotations;

namespace ElevenNoteWebApp_2.Server.Models
{
    public class Category
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
    }
}
