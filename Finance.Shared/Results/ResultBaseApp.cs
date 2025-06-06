using System;
using Finance.Shared.Errors;
using Finance.Shared.Messages;

namespace Finance.Shared.Results;

public abstract class ResultBaseApp : IResultBaseApp
{
  public bool IsFailed => Messages.OfType<IErrorApp>().Any();

  public bool IsSuccess => !IsFailed;

  public List<IMessageApp> Messages { get; } = [];

  public List<IInfoMessage> InforMessages => [.. Messages.OfType<IInfoMessage>()];

  public List<ISuccessMessage> SuccessMessages => [.. Messages.OfType<ISuccessMessage>()];

  public List<IWarningMessage> WarningMessages => [.. Messages.OfType<IWarningMessage>()];

  public List<IErrorApp> Errors => [.. Messages.OfType<IErrorApp>()];
}

public abstract class ResultBaseApp<TResult> : ResultBaseApp
    where TResult : ResultBaseApp<TResult>

{
  private TResult AddMessage(IMessageApp message)
  {
    Messages.Add(message);
    return (TResult)this;
  }

  public TResult AddInfoMessage(IInfoMessage message) => AddMessage(message);

  public TResult AddInfoSuccess(ISuccessMessage message) => AddMessage(message);

  public TResult AddWarningSuccess(IWarningMessage message) => AddMessage(message);

  public TResult AddError(IErrorApp error) => AddMessage(error);

  public TResult WithMessages(IEnumerable<IMessageApp> messages)
  {
    Messages.AddRange(messages);
    return (TResult)this;
  }

}