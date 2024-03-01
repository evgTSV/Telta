using System.Reflection;

namespace Telta.Core;

public static class InfoCompilerMessages
{
    public static string CompilerBanner => $"Telta Compiler v{Assembly.GetExecutingAssembly().GetName().Version}";
}