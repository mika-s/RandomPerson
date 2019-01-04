module internal ListMode

open RandomPersonLib
open PrintToConsole
open PrintToFile
open ReadInputFiles
open CliEnums
open Settings

let listMode (settingsFilePath: string) (amount: int) (country: Country) (outputType: OutputType) (fileFormat: FileFormat) (outputFilePath: string) =
    let i = readInputFiles settingsFilePath
    let lib = RandomPerson()
    let options = genericOptionsToRandomPersonOptions i.settings.ListMode.Options

    match outputType with
    | Console -> lib.CreatePeople(amount, country, options)
                 |> printToConsole i
    | File    -> lib.CreatePeople(amount, country, options)
                 |> List.toArray
                 |> printToFile fileFormat outputFilePath i.settings.ListMode
    | ConsoleAndFile ->
        let people = lib.CreatePeople(amount, country, options)
        printToConsole i people

        people
        |> List.toArray
        |> printToFile fileFormat outputFilePath i.settings.ListMode
