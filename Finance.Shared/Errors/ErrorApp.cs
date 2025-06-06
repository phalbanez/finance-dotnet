using System;
using Finance.Shared.Messages;

namespace Finance.Shared.Errors;

public class ErrorApp(string text, string code = "") : MessageApp(text, code, MessageType.Error), IErrorApp;
