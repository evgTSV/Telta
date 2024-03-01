using Telta.Core;

using Info = Telta.Core.InfoCompilerMessages;
using Error = Telta.Core.ErrorCompilerMessages;

namespace Telta.Configuration.Tests;

public class CommandLineParserTests
{
    [Fact]
    public async Task UseMainArguments()
    {
        string inputFile = @"C:\Programs\Calc\calc.tlt";
        string outputFile = @"C:\Programs\Calc\bin\calc.exe";
        string[] arguments = [inputFile, "-o", outputFile];
        var reporter = new MockCompilerReporter();

        var isSuccess = await CommandLineParser.TryParseCommandLineArguments(arguments, reporter, args =>
        {
            Assert.Equal(inputFile, args.InputFilePaths.Single());
            Assert.Single(args.InputFilePaths);
            Assert.Equal(outputFile, args.OutputFilePath);
            AssertOptionalArgsHasDefaultValues(args);
            return Task.FromResult(true);
        });
        
        AssertOnlyInformationalCompilerBanner(reporter);
        Assert.Empty(reporter.Errors);
        Assert.True(isSuccess);
    }
    
    [Fact]
    public async Task MissingAnyInputFile()
    {
        string outputFile = @"C:\Programs\Calc\bin\calc.exe";
        string[] arguments = ["-o", outputFile];
        var reporter = new MockCompilerReporter();

        bool isSuccess = await CommandLineParser.TryParseCommandLineArguments(arguments, reporter, args =>
        {
            AssertOptionalArgsHasDefaultValues(args);
            return Task.FromResult(false);
        });

        AssertOnlyInformationalCompilerBanner(reporter);
        Assert.Single(reporter.Errors);
        Assert.Equal(Error.InputFilesShouldBeDefined, reporter.Errors.Single());
        Assert.False(isSuccess);
    }

    [Fact]
    public async Task MissingOutputFile()
    {
        string inputFile = @"C:\Programs\Calc\calc.tlt";
        string[] arguments = [inputFile];
        var reporter = new MockCompilerReporter();

        bool isSuccess = await CommandLineParser.TryParseCommandLineArguments(arguments, reporter, args =>
        {
            AssertOptionalArgsHasDefaultValues(args);
            return Task.FromResult(false);
        });
        
        AssertOnlyInformationalCompilerBanner(reporter);
        Assert.Single(reporter.Errors);
        Assert.Equal(Error.MissingOutputOption, reporter.Errors.Single());
        Assert.False(isSuccess);
    }

    private void AssertOptionalArgsHasDefaultValues(Arguments args)
    {
        Assert.Equal(TargetArchType.Any, args.TargetArch);
        Assert.Equal(ModuleKind.Console, args.TargetModule);
    }

    private void AssertOnlyInformationalCompilerBanner(MockCompilerReporter reporter)
    {
        Assert.NotNull(reporter.Information.Single(m => m == Info.CompilerBanner));
        Assert.Empty(reporter.Information.Where(m => m != Info.CompilerBanner));
    }
}