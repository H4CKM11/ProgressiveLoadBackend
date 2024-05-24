namespace ProgressiveLoadBackend.DTOs
{
    public class SessionsDTOs
    {
        public Guid sessionID { get; set; }
        public Guid userID { get; set; }
        public DateTime createdAt { get; set; }
        public DateTime expiresAt { get; set; }

    }
}
