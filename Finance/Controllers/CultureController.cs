using System.Globalization;
using Finance.Config;
using Finance.Shared.Dtos;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;

namespace Finance.Controllers;

[Route("[controller]")]
public class CultureController : Controller
{
    [HttpGet()]
    public IResult GetDefaultCulture()
    {
        CultureInfo currentCulture = CultureInfo.CurrentCulture;
        // CultureInfo currentCulture = Thread.CurrentThread.CurrentCulture;

        var cultureInfo = new
        {
            Name = currentCulture.Name, // Ex: "en-US", "pt-BR"
            DisplayName = currentCulture.DisplayName, // Ex: "English (United States)", "Português (Brasil)"
            LCID = currentCulture.LCID, // Ex: 1033 (en-US), 1046 (pt-BR)
        };

        return Results.Ok(cultureInfo);
    }

    [HttpGet("ui")]
    public IResult GetDefaultUICulture()
    {
        CultureInfo currentUICulture = CultureInfo.CurrentUICulture;

        var cultureInfo = new
        {
            Name = currentUICulture.Name,
            DisplayName = currentUICulture.DisplayName,
            LCID = currentUICulture.LCID
        };

        return Results.Ok(cultureInfo);
    }

    [HttpGet("change")]
    public IResult ChangeCulture(string? culture, string? redirectUri)
    {
        if (string.IsNullOrWhiteSpace(culture))
            return Results.BadRequest("Culture name cannot be null");

        CultureConfig.SetCookie(culture, HttpContext);

        if (string.IsNullOrWhiteSpace(redirectUri))
            return Results.Ok("Culture set successfully");

        return Results.LocalRedirect(redirectUri);
    }

    [HttpPost()]
    public IResult SelectCulture([FromBody] CultureRequest cultureRequest)
    {
        if (string.IsNullOrWhiteSpace(cultureRequest.Name))
        {
            return Results.BadRequest("Culture name cannot be null");
        }

        CultureConfig.SetCookie(cultureRequest.Name, HttpContext);

        if (string.IsNullOrWhiteSpace(cultureRequest.RedirectUri))
        {
            return Results.Ok("Culture set successfully");
        }

        return Results.LocalRedirect(cultureRequest.RedirectUri);
    }
}