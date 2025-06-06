
namespace Finance.Shared.Errors;

public class NotFoundErrorApp(string text, string code) : ErrorApp(text, code);

public class BadRequestErrorApp(string text, string code) : ErrorApp(text, code);

public class ValidationErrorApp(string text, string code) : ErrorApp(text, code);

public class ConflictErrorApp(string text, string code) : ErrorApp(text, code);

public class UnauthorizedErrorApp(string text, string code) : ErrorApp(text, code);

public class ForbiddenErrorApp(string text, string code) : ErrorApp(text, code);

public class InternalServerErrorApp(string text, string code) : ErrorApp(text, code);

// public class ServiceUnavailableErrorApp(string text, string code) : ErrorApp(text, code);

// public class GatewayTimeoutErrorApp(string text, string code) : ErrorApp(text, code);

// public class TooManyRequestsErrorApp(string text, string code) : ErrorApp(text, code);

// public class NotImplementedErrorApp(string text, string code) : ErrorApp(text, code);

// public class MethodNotAllowedErrorApp(string text, string code) : ErrorApp(text, code);

// public class NotAcceptableErrorApp(string text, string code) : ErrorApp(text, code);