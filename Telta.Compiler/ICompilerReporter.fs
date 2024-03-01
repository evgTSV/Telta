namespace Telta.Compiler

type ICompilerReporter =
    abstract ReportInformation: message:string -> unit
    abstract ReportError: message:string -> unit