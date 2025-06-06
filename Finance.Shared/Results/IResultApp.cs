namespace Finance.Shared.Results;

public interface IResultApp<out TValue> : IResultBaseApp
{
  TValue? Value { get; }
}