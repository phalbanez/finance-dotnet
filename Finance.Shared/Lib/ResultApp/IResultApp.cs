namespace Lib.ResultApp;

public interface IResultApp<out TValue> : IResultBaseApp
{
    TValue? Value { get; }
}