module internal PrintToFile

open RandomPersonLib
open CliEnums
open Settings
open PrintCsv
open PrintJson
open PrintXml

let printToFile (fileFormat: FileFormat) (outputFilePath: string) (settings: listModeSettings) (people: Person[]) =
    match fileFormat with
    | FileFormat.CSV  -> printToCsv  people outputFilePath settings.FilePrint |> ignore
    | FileFormat.JSON -> printToJson people outputFilePath settings           |> ignore
    | FileFormat.XML  -> printToXml  people outputFilePath settings           |> ignore
    | _ -> failwith "Illegal file format."
