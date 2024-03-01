using CommandLine;
using CommandLine.Text;
using Telta.Compiler;

using ClParser = CommandLine.Parser;
using Info = Telta.Core.InfoCompilerMessages;

namespace Telta.Configuration;

public static class CommandLineParser
{
    public static async Task<bool> TryParseCommandLineArguments(string[] arguments, Func<Arguments, Task<bool>> worker, ICompilerReporter? reporter)
    {
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
                return true;
            }
            
            return await worker(args);
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