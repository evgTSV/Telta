namespace Telta.Core.ErrorHandling.Exceptions;

public class ErrorLevelException : Exception, ILogConfig
{
    public ConsoleColor TextColor => ConsoleColor.Red;
}