module internal CmdLineArgumentParsing

open System
open RandomPersonLib
open CliEnums
open CliUtil

type options = {
    mode: Mode
    numberOfElements: int       // In ListMode and TemplateMode
    nationality: Nationality    // In ListMode and TemplateMode
    outputType: OutputType      // In ListMode
    fileFormat: FileFormat      // In ListMode
    outputFilePath: string      // In ListMode
    settingsFilePath: string
}

let defaultOptions = {
    mode = Mode.Interactive;
    numberOfElements = 10;
    nationality = Nationality.Norwegian;
    outputType = OutputType.Console
    fileFormat = FileFormat.CSV;
    outputFilePath = "output.?";
    settingsFilePath = "data/Settings.json";
}

let printUsage () =
    printfn "Usage:"
    printfn "dotnet RandomPersonCli.dll [-m <I/L/T/V>] [-n <Danish/Finnish/Norwegian/Swedish>] [-a <n>] [-f <CSV/JSON/XML>]"
    printfn "                           [-o <path>] [-s <path>]"
    printfn ""
    printfn "-m: Mode. Either I (interactive), L (list), T (templated list) or V (validation)."
    printfn "-n: Nationality. Either Danish, Finnish, Norwegian or Swedish. Used in List or Template mode."
    printfn "-a: Amount. Number of people to generate in List or Template mode."
    printfn "-f: File format. File format to use when printing to file in List mode. Will print to the console if not specified."
    printfn "-o: Output file path. Path to output file when printing to file in List mode."
    printfn "-s: Settings file path. Path to the settings file if non-default file is used."
    printfn ""
    printfn "Default: Interactive mode. If List or Template mode: 10 people, Norwegian nationality."
    printfn ""
    printfn "The options are case-sensitive."

let rec parseArgs (args: list<string>) (options: options) =
    match args with
    | [] -> options
    | "-h"::xs ->
        printUsage ()
        Environment.Exit 1; parseArgs xs options
    | "-m"::xs ->
        match xs with
        | "I"::xss ->
            let newOptions = { options with mode = Mode.Interactive }
            parseArgs xss newOptions
        | "L"::xss ->
            let newOptions = { options with mode = Mode.List }
            parseArgs xss newOptions
        | "T"::xss ->
            let newOptions = { options with mode = Mode.Template }
            parseArgs xss newOptions
        | "V"::xss ->
            let newOptions = { options with mode = Mode.Validation }
            parseArgs xss newOptions
        | _ ->
            eprintf "-m flag needs either I (interactive mode), L (list mode), T (template mode) or V (validation mode)\n"
            parseArgs xs options
    | "-n"::xs ->
        match xs with
        | "Norwegian"::xss ->
            let newOptions = { options with nationality = Nationality.Norwegian }
            parseArgs xss newOptions
        | "Swedish"::xss ->
            let newOptions = { options with nationality = Nationality.Swedish }
            parseArgs xss newOptions
        | "Danish"::xss ->
            let newOptions = { options with nationality = Nationality.Danish }
            parseArgs xss newOptions
        | "Finnish"::xss ->
            let newOptions = { options with nationality = Nationality.Finnish }
            parseArgs xss newOptions
        | _ ->
            invalidArg "-n flag" "needs either Danish, Finnish, Norwegian or Swedish after it\n"
            parseArgs xs options
    | "-a"::xs ->
        match xs with
        | (Int i)::xss ->
            let newOptions = { options with numberOfElements = i }
            parseArgs xss newOptions
        | _ -> invalidArg "-a flag" "needs a number after it\n"
    | "-f"::xs ->
        match xs with
        | "CSV"::xss ->
            let newOptions = { options with outputType = OutputType.File; fileFormat = FileFormat.CSV }
            parseArgs xss newOptions
        | "JSON"::xss ->
            let newOptions = { options with outputType = OutputType.File; fileFormat = FileFormat.JSON }
            parseArgs xss newOptions
        | "XML"::xss ->
            let newOptions = { options with outputType = OutputType.File; fileFormat = FileFormat.XML }
            parseArgs xss newOptions
        | _ -> invalidArg "-f flag" "needs either CSV, JSON or XML after it\n"
    | "-o"::xs ->
        match xs with
        | (Filename fn)::xss ->
            let newOptions = { options with outputFilePath = fn }
            parseArgs xss newOptions
        | _ -> invalidArg "-o flag" "needs a file path after it\n"
    | "-s"::xs ->
        match xs with
        | (Filename fn)::xss ->
            let newOptions = { options with settingsFilePath = fn }
            parseArgs xss newOptions
        | _ -> invalidArg "-s flag" "needs a file path after it\n"
    | x::xs ->
        eprintf "Illegal argument: %s\n" x
        parseArgs xs options 
