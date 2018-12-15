module internal PrintXml

open System.Xml
open RandomPersonLib
open CliUtil
open Settings

let createXmlSerializerSettings (isFormatted: bool) =
    XmlWriterSettings (Indent = isFormatted)

let printToXml (people: Person[]) (outputFilePath: string) (settings: listModeSettings) =
    let filenameWithFixedFileEnding = outputFilePath.Replace("?", "xml")
    let xmlPrintSettings = createXmlSerializerSettings settings.PrintOptions.XmlPrettyPrint

    people
    |> writeToXmlFile<Person[]> filenameWithFixedFileEnding xmlPrintSettings
