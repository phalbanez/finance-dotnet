using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using Finance.Shared.Dtos;

namespace Finance.Api.Controllers;

[Route("api/[controller]")]
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
    // CurrentUICulture reflete a cultura para recursos (strings, etc.) do thread atual.
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

    HttpContext.Response.Cookies.Append(
      CookieRequestCultureProvider.DefaultCookieName,
      CookieRequestCultureProvider.MakeCookieValue(new RequestCulture(culture, culture))
    );

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

    HttpContext.Response.Cookies.Append(
        CookieRequestCultureProvider.DefaultCookieName,
        CookieRequestCultureProvider.MakeCookieValue(
          new RequestCulture(cultureRequest.Name, cultureRequest.Name))
    );

    if (string.IsNullOrWhiteSpace(cultureRequest.RedirectUri))
    {
      return Results.Ok("Culture set successfully");
    }

    return Results.LocalRedirect(cultureRequest.RedirectUri);
  }
}
