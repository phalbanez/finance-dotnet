using System;

namespace Finance.Shared.Messages;

public enum MessageType { Info, Warning, Error, Success }

public interface IMessageApp
{
  string Code { get; }

  string Text { get; }

  MessageType Type { get; }
}
