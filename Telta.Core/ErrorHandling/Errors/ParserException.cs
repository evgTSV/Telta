namespace Telta.Core.ErrorHandling.Exceptions;

public class ParserException(string message) : ErrorLevelException
{
    public override string Message => message;
}