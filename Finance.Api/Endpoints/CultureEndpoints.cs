using System;
using System.Globalization;
using Finance.Shared.Dtos;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;

namespace Finance.Api.Endpoints;

public static class CultureEndpoints
{
  public static void MapCultureEndpoints(this IEndpointRouteBuilder endpoints)
  {
    var group = endpoints.MapGroup("culture")
      .WithTags("Culture")
      .WithName("Culture");

    group.MapGet("/", () =>
    {
      CultureInfo currentCulture = CultureInfo.CurrentCulture;
      // CultureInfo currentCulture = Thread.CurrentThread.CurrentCulture;

      var cultureInfo = new
      {
        Name = currentCulture.Name, // Ex: "en-US", "pt-BR"
        DisplayName = currentCulture.DisplayName, // Ex: "English (United States)", "Português (Brasil)"
        LCID = currentCulture.LCID // Ex: 1033 (en-US), 1046 (pt-BR)
      };

      return Results.Ok(cultureInfo);
    }).WithName("GetDefaultCulture");

    group.MapGet("/ui", () =>
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

    }).WithName("GetDefaultUICulture");

    group.MapGet("/change", (HttpContext httpContext, string? culture, string? redirectUri) =>
    {
      if (string.IsNullOrWhiteSpace(culture))
        return Results.BadRequest("Culture name cannot be null");

      httpContext.Response.Cookies.Append(
        CookieRequestCultureProvider.DefaultCookieName,
        CookieRequestCultureProvider.MakeCookieValue(new RequestCulture(culture, culture))
      );

      if (string.IsNullOrWhiteSpace(redirectUri))
        return Results.Ok("Culture set successfully");

      return Results.LocalRedirect(redirectUri);

    }).WithName("ChangeCulture");

    group.MapPost("/", (HttpContext httpContext, [FromBody] CultureRequest cultureRequest) =>
    {
      if (string.IsNullOrWhiteSpace(cultureRequest.Name))
        return Results.BadRequest("Culture name cannot be null");

      httpContext.Response.Cookies.Append(
          CookieRequestCultureProvider.DefaultCookieName,
          CookieRequestCultureProvider.MakeCookieValue(
            new RequestCulture(cultureRequest.Name, cultureRequest.Name))
      );

      if (string.IsNullOrWhiteSpace(cultureRequest.RedirectUri))
        return Results.Ok("Culture set successfully");

      return Results.LocalRedirect(cultureRequest.RedirectUri);

    }).WithName("SelectCulture");

  }

}