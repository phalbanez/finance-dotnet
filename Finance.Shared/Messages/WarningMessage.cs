using System;

namespace Finance.Shared.Messages;

public class WarningMessage(string text, string code = "") : MessageApp(text, code, MessageType.Warning), IWarningMessage;