using ProgressiveLoadBackend.Models;

namespace ProgressiveLoadBackend.Services.Cookies
{
    public interface ICookieService
    {
        CookieOptions createSessionCookie(Sessions session);
    }
}
