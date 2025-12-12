using System.ComponentModel.DataAnnotations;
using System.Net;
using Finance.Shared.Entities;
using Microsoft.AspNetCore.Mvc;

namespace Finance.Controllers;

[ApiController]
[Route("[controller]")]
public class UsersController : ControllerBase
{
    [HttpGet()]
    public IResult GetUsers()
    {
        return Results.Ok("Usuários");
    }

    // [HttpPost()]
    // public IResult CreateUsers(User user)
    // {
    //     return Results.Ok(user);
    // }

    [HttpPost()]
    public IResult CreateUsers(User user)
    {
        return Results.Ok(user);
    }
}
