using System.ComponentModel.DataAnnotations;

namespace Recenzex.Models
{
    public class Genre
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Nazwa gatunku jest wymagana")]
        [StringLength(50)]
        public string Name { get; set; }

        public ICollection<Film>? Films { get; set; }
    }
}
