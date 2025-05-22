using System;

namespace Finance.Api.Endpoints;

public static class HomeEndpoints
{
  public static void MapHomeEndpoints(this IEndpointRouteBuilder endpoints)
  {
    var group = endpoints.MapGroup("/api")
      .WithTags("Home")
      .WithName("Home");

    group.MapGet("/", () =>
    {
      return Results.Ok("Welcome to the Finance minimal API");
    });
  }
}
