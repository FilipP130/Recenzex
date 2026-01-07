using System.ComponentModel.DataAnnotations;

namespace Recenzex.Models
{
    public class Review
    {
        public int Id { get; set; }

        [Range(1, 10, ErrorMessage = "Ocena musi być od 1 do 10")]
        public int Rating { get; set; }

        [Required(ErrorMessage = "Treść recenzji jest wymagana")]
        [StringLength(1000)]
        public string Content { get; set; }

        public int FilmId { get; set; }
        public Film? Film { get; set; }

        public ICollection<Comment>? Comments { get; set; }
    }
}
