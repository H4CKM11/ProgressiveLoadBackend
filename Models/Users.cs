using System.ComponentModel.DataAnnotations;

namespace ProgressiveLoadBackend.Models
{
    public class Users
    {
        [Key]
        public Guid userID { get; set; }
        public required string firstName { get; set; }
        public required string lastName { get; set; }
        public required string email { get; set; }
        public required string passwordHash { get; set; }

    }
}
