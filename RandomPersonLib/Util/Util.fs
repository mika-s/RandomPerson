module internal Util

open System
open System.IO
open System.Runtime.Serialization.Json
open System.Text
open Types.SSNTypes

let intFromChar (x: char) = x |> Char.GetNumericValue |> int
let intArrayFromString (numbersStr: string) = numbersStr |> Seq.toArray |> Array.map intFromChar
let nullCoalesce (value: Nullable<'T>) (otherValue: 'T) = if value.HasValue then value.Value else otherValue
let inline toCharArray (str: string) = str.ToCharArray()
let stringAsChar (str: string) = str.[0]

let deserializeJson<'T> (json: string) =
    use ms = new MemoryStream(Encoding.UTF8.GetBytes(json))
    let jsonSerializer = DataContractJsonSerializer(typedefof<'T>)
    jsonSerializer.ReadObject(ms) :?> 'T

let readDataFromJsonFile<'T> (filename: string) =
    let json = File.ReadAllText(filename)
    deserializeJson<'T> json

let (|Equals|_|) arg x = if (arg = x) then Some() else None

let incrementAtPosition (digit: int) (input: string) =
    let digitInInput = intFromChar input.[digit]
    let incremented = ((digitInInput + 1) % 10).ToString ()
    input.Remove(digit, 1).Insert(digit, incremented)

let getRandom (isUsingManualSeed: bool) (seed: int) =
    match isUsingManualSeed with
    | true  -> Random(seed)
    | false -> Random()

let bind switchFunction twoTrackInput =
    match twoTrackInput with
    | Success s -> switchFunction s
    | Failure f -> Failure f
