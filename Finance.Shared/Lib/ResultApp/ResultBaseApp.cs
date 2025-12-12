namespace Lib.ResultApp;

public abstract class ResultBaseApp : IResultBaseApp
{
    public bool IsFailed => ResultFailure is not null;

    public bool IsSuccess => !IsFailed;

    public ResultFailureApp? ResultFailure { get; set; }
}

public abstract class ResultBaseApp<TResult> : ResultBaseApp
    where TResult : ResultBaseApp<TResult>

{
    public TResult AddError(string propertyName, string errorMessage)
    {
        if (ResultFailure is null)
            throw new NullReferenceException($"{nameof(ResultFailure)} is null. Cannot add error.");

        ResultFailure.AddError(propertyName, errorMessage);
        return (TResult)this;
    }

    public TResult WithResultFailure(ResultFailureApp? resultFailure)
    {
        ResultFailure = resultFailure;
        return (TResult)this;
    }
}