using System;

namespace Finance.Shared.Messages;

public class InfoMessage(string text, string code = "") : MessageApp(text, code, MessageType.Info), IInfoMessage;

