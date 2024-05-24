namespace ProgressiveLoadBackend.Services.HashingService
{
    public interface IHashingService
    {
        string hashPassword(string password);
        bool verifyPassword(string passwordHash, string passwordProvided);
    }
}
