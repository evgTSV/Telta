namespace Telta.Core.ErrorHandling.Warnings;

public class WarningLevelException : Exception, ILogConfig
{
    public ConsoleColor TextColor => ConsoleColor.Yellow;
}