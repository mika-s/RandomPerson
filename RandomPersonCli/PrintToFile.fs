module internal PrintToFile

open RandomPersonLib
open CliEnums
open Settings
open PrintCsv
open PrintJson
open PrintXml

let printToFile (fileFormat: FileFormat) (outputFilePath: string) (settings: ListModeSettings) (people: Person[]) =
    match fileFormat with
    | CSV  -> printToCsv  people outputFilePath settings.FilePrint settings.PrintOptions |> ignore
    | JSON -> printToJson people outputFilePath settings                                 |> ignore
    | XML  -> printToXml  people outputFilePath settings                                 |> ignore
