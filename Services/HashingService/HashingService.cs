using Isopoh.Cryptography.Argon2;

namespace ProgressiveLoadBackend.Services.HashingService
{
    public class HashingService : IHashingService
    {
        public string hashPassword(string password)
        {
            return Argon2.Hash(password);
        }

        public bool verifyPassword(string passwordHash, string passwordProvided)
        {
            return Argon2.Verify(passwordHash, passwordProvided);
        }
    }
}
