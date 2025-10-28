using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BookMoodMAUI2.Models
{
    public class Book
    {
        [Key]
        public Guid Id { get; init; }

        public string Title { get; set; }
        public string? Author { get; set; }
        public string? Description { get; set; }
        public string? Notes { get; set; }
        public string? Mood { get; set; }
        public DateTime DateAdded { get; set; }

        [NotMapped]
        public Microsoft.Maui.Graphics.Color CoverColor { get; set; }

        public Book() { }
    }
}
