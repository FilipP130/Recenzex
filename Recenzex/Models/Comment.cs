using System.ComponentModel.DataAnnotations;

namespace Recenzex.Models
{
    public class Comment
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Treść komentarza jest wymagana")]
        [StringLength(500)]
        public string Content { get; set; }

        public int ReviewId { get; set; }
        public Review? Review { get; set; }
    }
}
