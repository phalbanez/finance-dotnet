using System;

namespace Finance.Shared.Messages;

public class SuccessMessage(string text, string code = "") : MessageApp(text, code, MessageType.Success), ISuccessMessage;
