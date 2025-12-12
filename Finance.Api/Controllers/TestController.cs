using System.Net;
using Finance.Api.Validators;
using Microsoft.AspNetCore.Mvc;
using Lib.ResultApp;
using Finance.Shared.Dtos;
using System.ComponentModel.DataAnnotations;

namespace Finance.Api.Controllers;

// public record TestDto(string Code, string Name, double Value);

[ApiController]
[Route("[controller]")]
public class TestController : Controller
{
    private readonly ILogger<TestController> _logger;

    public TestController(ILogger<TestController> logger)
    {
        _logger = logger;
    }

    [HttpGet]
    public IResult Index()
    {
        return Results.Ok("Test Controller");
    }

    [HttpPost]
    public IResult Validate(TestDto testDto)
    {
        var validator = new TestDtoValidator();
        var validationResult = validator.Validate(testDto);

        validationResult.Errors.ForEach(error =>
        {
            _logger.LogWarning("Property: {PropertyName} - Error: {ErrorMessage}",
                error.PropertyName, error.ErrorMessage);
        });

        Results.ValidationProblem(validationResult.ToDictionary());

        return Results.Ok(testDto);
    }

    [HttpPost("validate-details")]
    public IResult ValidateDetails(TestDto testDto)
    {
        var validationProblemDetails = new ValidationProblemDetails
        {
            Title = "Um ou mais erros de validação ocorreram.",
            Status = (int)HttpStatusCode.BadRequest
        };

        validationProblemDetails.Errors.Add("Name",
            ["Primeira validação do usuário", "Segunda validação do usuário"]);

        validationProblemDetails.Errors.Add("Email",
            ["Primeira validação do email", "Segunda validação do email"]);

        return Results.BadRequest(validationProblemDetails);
    }

    [HttpPost("validate-model")]
    public IResult ValidateModel(TestDto testDto)
    {
        ModelState.AddModelError("Name", "Primeira validação do usuário");
        ModelState.AddModelError("Name", "Segunda validação do usuário");

        ModelState.AddModelError("Email", "Primeira validação do email");
        ModelState.AddModelError("Email", "Segunda validação do email");

        return Results.BadRequest(ModelState);
    }

    [HttpGet("ok")]
    public IResult TestOk()
    {
        var result = GetSucessResult();

        return Results.Ok(result.Value);
    }

    [HttpGet("fail")]
    public IResult TestFail()
    {
        var result = GetFailResult();

        // throw new InvalidOperationException("This is a test exception");

        return Results.BadRequest(result.ResultFailure);

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

    private static ResultApp<TestDto> GetSucessResult()
    {
        var value = new TestDto
        {
            Id = 123,
            Name = "Teste 123",
            Email = "test@test.com",
            // Date= DateTime.UtcNow,
            Value = 123.45
        };

        var result = ResultApp.Success(value);

        return result;
    }

    private static ResultApp<TestDto> GetFailResult()
    {
        var result = ResultApp.Fail("Um ou mais erros de validação ocorreram.",
                (int)HttpStatusCode.BadRequest);

        result.AddError("Name", "Primeira validação do usuário")
          .AddError("Name", "Segunda validação do usuário")
          .AddError("Email", "Primeira validação do email")
          .AddError("Email", "Segunda validação do email");

        return result;
    }

}