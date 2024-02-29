using CommandLine;

namespace Telta.Configuration;

public sealed class Arguments
{
    [Value(0)]
    public IList<string> InputFilePaths { get; init; } = null!;
    
    [Option('o', "out")]
    public string OutputFilePath { get; init; } = null!;
    
    [Option("module", 
        HelpText = "Defines target module")]
    public ModuleKind TargetModule { get; init; }
    
    [Option('p', "processor", 
        HelpText = "Target processor architecture. Default = Any",
        Default = TargetArchType.Any)]
    public TargetArchType TargetArch { get; init; }
    
    [Option("no-logo", 
        HelpText = "Suppress compiler banner message")] 
    public bool NoLogo { get; init; }
}

public enum ModuleKind
{
    Console,
    Library
}

public enum TargetArchType
{
    X86,
    X64,
    Any,
}