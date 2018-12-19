module internal CmdLineArgumentParsing

open System
open RandomPersonLib
open CliEnums
open CliUtil

type options = {
    mode: Mode
    amount: int                 // In ListMode and TemplateMode
    nationality: Nationality    // In ListMode and TemplateMode
    outputType: OutputType      // In ListMode
    fileFormat: FileFormat      // In ListMode
    outputFilePath: string      // In ListMode
    settingsFilePath: string
    ssn: string                 // In ValidationMode
}

let defaultOptions = {
    mode = Mode.Interactive
    amount = 10
    nationality = Nationality.Norwegian
    outputType = OutputType.Console
    fileFormat = FileFormat.CSV
    outputFilePath = "output.?"
    settingsFilePath = "data/Settings.json"
    ssn = String.Empty
}

let printVersion () = printfn "Version: 1.7.0.0"

let printUsage () =
    printfn "NAME"
    printfn ""
    printfn "    RandomPersonCli - generate random personal information"
    printfn ""
    printfn "SYNOPSIS"
    printfn ""
    printfn "dotnet RandomPersonCli.dll [-m (I|L|T|V [<SSN>])]"
    printfn "                           [-n (Danish|Dutch|Finnish|Icelandic|Norwegian|Swedish)]"
    printfn "                           [-a (n)] [-f (CSV|JSON|XML)] [--caf (true|false)]"
    printfn "                           [-o (path)] [-s (path)]"
    printfn ""
    printfn "DESCRIPTION"
    printfn ""
    printfn "    RandomPersonCli will generate random personal information, such as name,"
    printfn "    address, SSN, gender, email and so on. It has various modes, can generate"
    printfn "    information for different countries and write to CSV, XML and JSON files,"
    printfn "    as well as straight to the console."
    printfn ""
    printfn "OPTIONS"
    printfn ""
    printfn "-m, --mode"
    printfn "    Either I (interactive), L (list), T (templated list) or V (validation)."
    printfn "    Validation mode can take SSN as optional input, otherwise it's using"
    printfn "    interactive validation."
    printfn ""
    printfn "-n, --nationality"
    printfn "    Either Danish, Dutch, Finnish, Icelandic, Norwegian or Swedish."
    printfn "    Used in List or Template mode."
    printfn ""
    printfn "-a, --amount"
    printfn "    Number of people to generate in List or Template mode."
    printfn ""
    printfn "-f, --filetype"
    printfn "    File format to use when printing to file in List mode."
    printfn "    Will print to the console if not specified."
    printfn ""
    printfn "--caf"
    printfn "    Print to both console and file at the same time if true. Only used when -f"
    printfn "    is specified. False is default."
    printfn ""
    printfn "-o, --output"
    printfn "    Path to output file when printing to file in List mode."
    printfn ""
    printfn "-s, --settings"
    printfn "    Path to the settings file if non-default file is used."
    printfn ""
    printfn ""
    printfn "Default:"
    printfn "    Interactive mode."
    printfn "    If List or Template mode: 10 people, Norwegian nationality."
    printfn "    If Validation mode with SSN supplied as argument: Norwegian nationality."
    printfn ""
    printfn "The options are case-sensitive."

let rec parseArgs (args: list<string>) (options: options) =
    match args with
    | [] -> options
    | "-v"::xs | "--version"::xs ->
        printVersion ()
        Environment.Exit 1; parseArgs xs options
    | "-h"::xs | "--help"::xs ->
        printUsage ()
        Environment.Exit 1; parseArgs xs options
    | "-m"::xs | "--mode"::xs ->
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
            match xss.Length with
            | length when 0 < length ->
                match xss with
                | (CmdLineArgument _)::_ ->
                    let newOptions = { options with mode = Mode.Validation }
                    parseArgs xss newOptions
                | _ ->
                    let newOptions = { options with mode = Mode.Validation; ssn = xss.[0] }
                    let xsss = List.skip 1 xss
                    parseArgs xsss newOptions
            | _ ->
                let newOptions = { options with mode = Mode.Validation }
                parseArgs xss newOptions
        | _ ->
            eprintf "-m flag needs either I (interactive mode), L (list mode), T (template mode) or V (validation mode)\n"
            parseArgs xs options
    | "-n"::xs | "--nationality"::xs ->
        match xs with
        | "Danish"::xss ->
            let newOptions = { options with nationality = Nationality.Danish }
            parseArgs xss newOptions
        | "Dutch"::xss ->
            let newOptions = { options with nationality = Nationality.Dutch }
            parseArgs xss newOptions
        | "Finnish"::xss ->
            let newOptions = { options with nationality = Nationality.Finnish }
            parseArgs xss newOptions
        | "Icelandic"::xss ->
            let newOptions = { options with nationality = Nationality.Icelandic }
            parseArgs xss newOptions
        | "Norwegian"::xss ->
            let newOptions = { options with nationality = Nationality.Norwegian }
            parseArgs xss newOptions
        | "Swedish"::xss ->
            let newOptions = { options with nationality = Nationality.Swedish }
            parseArgs xss newOptions
        | _ ->
            invalidArg "-n flag" "needs either Danish, Dutch, Finnish, Icelandic, Norwegian or Swedish after it\n"
            parseArgs xs options
    | "-a"::xs | "--amount"::xs ->
        match xs with
        | (Int i)::xss ->
            let newOptions = { options with amount = i }
            parseArgs xss newOptions
        | _ -> invalidArg "-a flag" "needs a number after it\n"
    | "-f"::xs | "--filetype"::xs ->
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
    | "--caf"::xs ->
        match xs with
        | "true"::xss ->
            let newOptions = { options with outputType = OutputType.ConsoleAndFile  }
            parseArgs xss newOptions
        | "false"::xss ->
            let newOptions = { options with outputType = OutputType.ConsoleAndFile }
            parseArgs xss newOptions
        | _ -> invalidArg "--caf flag" "needs either true or false after it\n"
    | "-o"::xs | "--output"::xs ->
        match xs with
        | (Filename fn)::xss ->
            let newOptions = { options with outputFilePath = fn }
            parseArgs xss newOptions
        | _ -> invalidArg "-o flag" "needs a file path after it\n"
    | "-s"::xs | "--settings"::xs ->
        match xs with
        | (Filename fn)::xss ->
            let newOptions = { options with settingsFilePath = fn }
            parseArgs xss newOptions
        | _ -> invalidArg "-s flag" "needs a file path after it\n"
    | x::xs ->
        eprintf "Illegal argument: %s\n" x
        parseArgs xs options 
