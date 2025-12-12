namespace Lib.ResultApp;

public interface IResultBaseApp
{
  bool IsFailed { get; }

  bool IsSuccess { get; }

  ResultFailureApp? ResultFailure { get; }
}
