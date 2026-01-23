using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace Recenzex.Models
{
    public class Comment
    {
        public int Id { get; set; }

        [Required]
        [StringLength(500)]
        public string Content { get; set; } = "";

        public int ReviewId { get; set; }
        public Review? Review { get; set; }

        [Required]
        public string UserId { get; set; } = "";

        public IdentityUser? User { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.Now;
    }
}
