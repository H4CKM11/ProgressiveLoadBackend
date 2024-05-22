namespace ProgressiveLoadBackend.DTOs
{
    public class RegisterDTO
    {
        public required string firstName { get; set; }
        public required string lastName { get; set; }
        public required string Email { get; set; }
        public required string Password { get; set; }
    }

    public class LoginDTO
    {
        public required string Email { get; set; }
        public required string Password { get; set; }
    }

}
