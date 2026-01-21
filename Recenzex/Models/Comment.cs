using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace Recenzex.Models
{
    public class Comment
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "brak tresci")]
        [StringLength(500)]
        public string Content { get; set; } = "";

        public int ReviewId { get; set; }
        public Review? Review { get; set; }

        public string UserId { get; set; } = "";
        public IdentityUser? User { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.Now;
    }
}
