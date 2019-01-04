open System
open System.Runtime.CompilerServices
open CmdLineArgumentParsing
open CliEnums
open InteractiveMode
open ListMode
open TemplateMode
open ValidatePANMode
open ValidateSSNMode

[<assembly: InternalsVisibleTo("Tests")>]
do()

let handleException (ex: Exception) =
    printfn "%s" ex.Message
    Environment.Exit 1

[<EntryPoint>]
let main argv =
    try
        let args = argv |> Array.toList
        let options = parseArgs args defaultOptions

        match options.mode with
        | Interactive -> interactiveMode
                            options.settingsFilePath
        | List        -> listMode
                            options.settingsFilePath
                            options.amount
                            options.country
                            options.outputType
                            options.fileFormat
                            options.outputFilePath
        | Template    -> templateMode
                            options.settingsFilePath
                            options.amount
                            options.country
        | ValidatePAN  -> validatePANMode
                            options.pan
        | ValidateSSN  -> validateSSNMode
                            options.ssn
                            options.country
    
        0
    with
    | _ as ex -> handleException ex; 1
