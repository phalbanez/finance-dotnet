using System;

namespace Finance.Shared.Messages;

public class MessageApp(string text, string code, MessageType type) : IMessageApp
{
  public string Code { get; } = code;

  public string Text { get; } = text;

  public MessageType Type { get; } = type;
}
