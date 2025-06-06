using Finance.Shared.Errors;
using Finance.Shared.Messages;
using Finance.Shared.Results;
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

  public record TestDto(string Code, string Name, double Value);

  [HttpGet("test/ok")]
  public IResult TestOk()
  {
    var result = GetSucessResult();

    return Results.Ok(result.Value);
  }

  private static ResultApp<TestDto> GetSucessResult()
  {
    var value = new TestDto(
      Code: "123",
      Name: "Teste 123",
      // Date: DateTime.UtcNow,
      Value: 123.45
    );

    var result = ResultApp.Success(value);

    return result;
  }

  [HttpGet("test/fail")]
  public IResult TestFail()
  {
    var result = GetFailResult();

    // throw new InvalidOperationException("This is a test exception");

    return Results.BadRequest(result.Errors);

    // return Results.BadRequest(error.Message);

    // return Results.ValidationProblem(
    //   new Dictionary<string, string[]>
    //   {
    //     { "Code", new[] { error.Code } },
    //     { "Message", new[] { error.Message } },
    //     { "Details", new[] { error.Details } }
    //   },
    //   title: "Validation Error",
    //   type: "https://example.com/validation-error"
    // );

    //   return Results.Problem("Teste de falha - Problem Details");

  }

  private static ResultApp<TestDto> GetFailResult()
  {
    ErrorApp error = new ErrorApp(
      text: "This is a test error 1",
      code: "TestError"
    );

    var result = ResultApp.Fail(error)
      .AddError(new ErrorApp("This is a test error 2"))
      .AddError(new ErrorApp("This is a test error 3", "TestError3"))
      .AddInfoMessage(new InfoMessage("This is a test info message"));

    return result;
  }

}
