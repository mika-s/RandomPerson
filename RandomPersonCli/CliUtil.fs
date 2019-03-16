module internal CliUtil

open System
open System.IO
open System.Runtime.Serialization
open System.Text
open System.Xml
open Newtonsoft.Json

let b2i (b: bool) = if b then 1 else 0

let readDataFromJsonFile<'T> (filename: string) =
    filename |> File.ReadAllText |> JsonConvert.DeserializeObject<'T>

let writeToJsonFile<'T> (filename: string) (jsonSerializerSettings: JsonSerializerSettings) (objToWrite: obj) =
    let output = JsonConvert.SerializeObject(objToWrite, jsonSerializerSettings)
                 |> Encoding.UTF8.GetBytes

    use fs = new FileStream(filename, FileMode.Create, FileAccess.Write)
    fs.Write(output, 0, output.Length)

let writeToXmlFile<'T> (filename: string) (xmlSerializerSettings: XmlWriterSettings) (objToWrite: obj) =
    let serializer = DataContractSerializer(typedefof<'T>)

    use xw = XmlWriter.Create(filename, xmlSerializerSettings)
    serializer.WriteObject(xw, objToWrite)

let nullCoalesce (value: Nullable<'T>) (otherValue: 'T) = if value.HasValue then value.Value else otherValue

let (|Int|_|) (str: string) =
    match Int32.TryParse(str) with
    | (true, int) -> Some(int)
    | _           -> None

let (|Filename|_|) (str: string) =
    match str.Length with
    | x when x > 0 -> Some(str)
    | _            -> None

let (|CmdLineArgument|_|) (str: string) =
    match str.[0] with
    | '-' -> Some(str)
    | _   -> None

let printHelp () =
    printfn "\nUsage:"
    printfn "Denmark: d"
    printfn "Finland: f"
    printfn "Iceland: i"
    printfn "Netherlands: N"
    printfn "Norway: n"
    printfn "Sweden: s"
    printfn "USA: u"
    printfn "Go back: b"
    printfn "Quit: q\n\n"
