using CommandLine;
using CommandLine.Text;
using Telta.Compiler;

using ClParser = CommandLine.Parser;
using Info = Telta.Core.InfoCompilerMessages;
using Error = Telta.Core.ErrorCompilerMessages;

namespace Telta.Configuration;

public static class CommandLineParser
{
    public static async Task<bool> TryParseCommandLineArguments(string[] arguments, ICompilerReporter? reporter, Func<Arguments, Task<bool>> worker)
    {
        bool success;
        
        var parserResult = new ClParser(cfg =>
        {
            cfg.HelpWriter = null;
            cfg.AllowMultiInstance = true;
        }).ParseArguments<Arguments>(arguments);

        return await parserResult.MapResult(async args =>
        {
            if (!args.NoLogo)
            {
                reporter?.ReportInformation(Info.CompilerBanner);
            }

            if (args.InputFilePaths.Count == 0)
            {
                reporter?.ReportError(Error.InputFilesShouldBeDefined);
            }

            if (string.IsNullOrWhiteSpace(args.OutputFilePath))
            {
                reporter?.ReportError(Error.MissingOutputOption);
            }
            
            success = await worker(args);

            return success;
        },
        _ =>
        {
            string helpMessage = GetHelpMessage(parserResult);
            Console.WriteLine(helpMessage);
            return Task.FromResult(false);
        });
    }

    private static string GetHelpMessage<T>(ParserResult<T> result)
    {
        return HelpText.AutoBuild(result);
    }
}