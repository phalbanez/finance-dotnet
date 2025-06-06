using System;
using Finance.Shared.Errors;
using Finance.Shared.Messages;

namespace Finance.Shared.Results;

public interface IResultBaseApp
{
  bool IsFailed { get; }

  bool IsSuccess { get; }

  List<IMessageApp> Messages { get; }

  List<IInfoMessage> InforMessages { get; }

  List<ISuccessMessage> SuccessMessages { get; }

  List<IWarningMessage> WarningMessages { get; }

  List<IErrorApp> Errors { get; }
}
