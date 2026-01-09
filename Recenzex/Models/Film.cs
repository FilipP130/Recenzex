using System.ComponentModel.DataAnnotations;

namespace Recenzex.Models
{
    public class Film
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Tytuł jest wymagany")]
        [StringLength(100)]
        public string Title { get; set; }

        [Range(1900, 2025, ErrorMessage = "Rok musi być pomiędzy 1900 a 2025")]
        public int Year { get; set; }

        [Required(ErrorMessage = "Opis jest wymagany")]
        [StringLength(1000)]
        public string Description { get; set; }

        [Required]
        public int GenreId { get; set; }
        public Genre? Genre { get; set; }

        public ICollection<Review>? Reviews { get; set; }
    }
}
