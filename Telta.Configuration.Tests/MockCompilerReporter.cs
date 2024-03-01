using Telta.Compiler;

namespace Telta.Configuration.Tests;

public class MockCompilerReporter : ICompilerReporter
{
    private List<string> _informationalMessages = new();
    private List<string> _errorMessages = new();

    public IEnumerable<string> Information => _informationalMessages;
    public IEnumerable<string> Errors => _errorMessages;

    public void ReportError(string message)
    {
        _errorMessages.Add(message);
    }

    public void ReportInformation(string message)
    {
        _informationalMessages.Add(message);
    }
}