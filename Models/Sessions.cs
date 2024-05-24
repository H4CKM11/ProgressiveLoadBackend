using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProgressiveLoadBackend.Models
{
    public class Sessions
    {
        [Key]
        public Guid sessionID { get; set; }
        [Required]
        public Guid userID { get; set; }
        [Required]
        public DateTime createdAt { get; set; }
        public DateTime expiresAt { get; set; }

        [ForeignKey("userID")]
        public required Users user { get; set; }
    }
}
