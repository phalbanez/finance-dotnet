using Finance.Shared.Errors;
using Finance.Shared.Messages;

namespace Finance.Shared.Results;

public class ResultApp : ResultBaseApp<ResultApp>
{
  public static ResultApp<TValue> Success<TValue>(TValue value) => new(value);

  // public static ResultApp<TValue> Fail<TValue>(ErrorApp error) => new(error);

  public static ResultApp Fail(ErrorApp error)
  {
    var result = new ResultApp();
    result.AddError(error);
    return result;
  }

  private ResultApp()
  { }

  public ResultApp<TNewValue> ToResult<TNewValue>(TNewValue? newValue = default)
  {
    return new ResultApp<TNewValue>()
        .WithValue(IsFailed ? default : newValue)
        .WithMessages(Messages);
  }
}

public class ResultApp<TValue> : ResultBaseApp<ResultApp<TValue>>, IResultApp<TValue>
{
  private TValue? _value = default;

  public TValue? Value
  {
    get
    {
      ThrowIfFailed();
      return _value;
    }
  }

  public ResultApp()
  { }

  public ResultApp(TValue value)
  {
    _value = value;
  }

  public ResultApp(IErrorApp error)
  {
    AddError(error);
  }

  private void ThrowIfFailed()
  {
    if (IsFailed)
      throw new InvalidOperationException("Cannot access the success value of a failed result");
  }

  public ResultApp<TValue> WithValue(TValue? value)
  {
    _value = value;
    return this;
  }

  public static implicit operator ResultApp<TValue>(ResultApp result)
  {
    return result.ToResult<TValue>(default);
  }

  // public static implicit operator Result<object>(Result<TValue> result)
  // {
  //   return result.ToResult<object>(value => value);
  // }

  // public static implicit operator Result<TValue>(TValue value)
  // {
  //   if (value is Result<TValue> r)
  //     return r;

  //   return Result.Ok(value);
  // }

  // public static implicit operator Result<TValue>(Error error)
  // {
  //   return Result.Fail(error);
  // }

  // public static implicit operator Result<TValue>(List<Error> errors)
  // {
  //   return Result.Fail(errors);
  // }

}
