module internal CliUtil

open System
open System.IO
open System.Runtime.Serialization
open System.Text
open System.Xml
open Newtonsoft.Json

let b2i (b: bool) = if b = true then 1 else 0

let readDataFromJsonFile<'T> (filename: string) =
    let readJson = File.ReadAllText(filename)
    JsonConvert.DeserializeObject<'T>(readJson);

let writeDataToJsonFile<'T> (filename: string) (objToWrite: obj) (jsonSerializerSettings: JsonSerializerSettings) = 
    let output = JsonConvert.SerializeObject(objToWrite, jsonSerializerSettings)
                 |> Encoding.UTF8.GetBytes

    use fs = new FileStream(filename, FileMode.Create, FileAccess.Write)
    fs.Write(output, 0, output.Length)

let writeDataToXmlFile<'T> (filename: string) (objToWrite: obj) (xmlSerializerSettings: XmlWriterSettings) = 
    let serializer = DataContractSerializer(typedefof<'T>)
    
    use xw = XmlWriter.Create(filename, xmlSerializerSettings)
    serializer.WriteObject(xw, objToWrite)

let nullCoalesce (value: Nullable<'T>) (otherValue: 'T) =
    if value.HasValue then value.Value
    else otherValue

let (|Int|_|) str =
    match Int32.TryParse(str) with
    | (true, int) -> Some(int)
    | _           -> None

let (|Filename|_|) (str: string) =
    match str.Length with
    | x when x > 0 -> Some(str)
    | _            -> None
