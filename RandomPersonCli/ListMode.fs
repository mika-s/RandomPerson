module internal ListMode

open RandomPersonLib
open PrintToConsole
open PrintToFile
open ReadInputFiles
open CliEnums
open Settings

let listMode (settingsFilePath: string) (amount: int) (nationality: Nationality) (outputType: OutputType) (fileFormat: FileFormat) (outputFilePath: string) =
    let i = readInputFiles settingsFilePath
    let lib = RandomPerson()
    let options = genericOptionsToRandomPersonOptions i.settings.ListMode.Options

    match outputType with
    | OutputType.Console -> lib.CreatePeople(amount, nationality, options)
                            |> printToConsole i
    | OutputType.File    -> lib.CreatePeople(amount, nationality, options)
                            |> List.toArray
                            |> printToFile fileFormat outputFilePath i.settings.ListMode
    | OutputType.ConsoleAndFile ->
                            let people = lib.CreatePeople(amount, nationality, options)
                            printToConsole i people

                            people
                            |> List.toArray
                            |> printToFile fileFormat outputFilePath i.settings.ListMode
    | _ -> invalidArg "outputType" "Illegal output type."
