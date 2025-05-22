using Microsoft.AspNetCore.Mvc;

namespace Finance.Api.Controllers;

[ApiController]
[Route("/")]
public class HomeController : ControllerBase
{
  [HttpGet()]
  public IResult Get()
  {
    return Results.Ok("Welcome to the Finance API");
  }
}
