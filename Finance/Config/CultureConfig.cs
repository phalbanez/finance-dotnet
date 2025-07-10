using Microsoft.AspNetCore.Localization;

namespace Finance.Config;

public static class CultureConfig
{

    public static CookieOptions GetCookieOptions(bool isSecure = true)
    {
        return new CookieOptions
        {
            // Expires = "DateTimeOffset.UtcNow.AddYears(1)",
            Path = "/",
            HttpOnly = false,
            Secure = isSecure,
            // IsEssential = true,
            // SameSite = SameSiteMode.Lax
            SameSite = SameSiteMode.Strict,
            MaxAge = TimeSpan.FromDays(365)
        };
    }

    public static void SetCookie(string culture, HttpContext httpContext)
    {
        var cookieOptions = GetCookieOptions(httpContext.Request.IsHttps);

        var requestCulture = new RequestCulture(culture, culture);
        var cookieName = CookieRequestCultureProvider.DefaultCookieName;
        var cookieValue = CookieRequestCultureProvider.MakeCookieValue(requestCulture);

        httpContext.Response.Cookies.Append(cookieName, cookieValue, cookieOptions);
    }
}
