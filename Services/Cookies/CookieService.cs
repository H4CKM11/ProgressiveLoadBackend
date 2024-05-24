using ProgressiveLoadBackend.Models;

namespace ProgressiveLoadBackend.Services.Cookies
{
    public class CookieService : ICookieService
    {
        public CookieOptions createSessionCookie(Sessions session)
        {
            var cookieOption = new CookieOptions();
            cookieOption.HttpOnly = true;
            cookieOption.Secure = true;
            cookieOption.Expires = session.expiresAt;

            return cookieOption;
        }
    }
}
