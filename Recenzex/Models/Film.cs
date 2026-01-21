using System.ComponentModel.DataAnnotations;

namespace Recenzex.Models
{
    public class Film
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Tytuł jest wymagany")]
        [StringLength(100)]
        public string Title { get; set; }

        [Range(1900, 2100, ErrorMessage = "Rok musi być pomiędzy 1900 a 2100")]
        public int Year { get; set; }

        [Required(ErrorMessage = "Opis jest wymagany")]
        [StringLength(1000)]
        public string Description { get; set; }

        [Url(ErrorMessage = "Podaj link")]
        [Display(Name = "Plakat (URL)")]
        [StringLength(500)]
        public string? PosterUrl { get; set; }

        [Required]
        public int GenreId { get; set; }
        public Genre? Genre { get; set; }
        public int RatingSum { get; set; } = 0;
        public int RatingCount { get; set; } = 0;

        public ICollection<Review>? Reviews { get; set; }
    }
}
