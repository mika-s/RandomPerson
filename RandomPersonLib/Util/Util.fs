module internal Util

open System
open System.IO
open System.Runtime.Serialization.Json
open System.Text
open System.Collections.Generic

let isOdd  (x: int) = x % 2 <> 0
let isEven (x: int) = x % 2 =  0
let intFromChar (x: char) = int(Char.GetNumericValue x)

let randomIntBetween   (min: int)   (max: int)   = Random().Next(min, max + 1)
let randomFloatBetween (min: float) (max: float) = Random().NextDouble() * (max - min) + min

let randomIntBetweenWithStep (min: int) (step: int) (max: int) = (randomIntBetween 0 ((max - min) / step)) * step + min

let deserializeJson<'T> (json: string) =
    use ms = new MemoryStream(Encoding.UTF8.GetBytes(json))
    let jsonSerializer = DataContractJsonSerializer(typedefof<'T>)
    jsonSerializer.ReadObject(ms) :?> 'T

let readDataFromJsonFile<'T> (filename: string) =
    let json = File.ReadAllText(filename)
    deserializeJson<'T> json

let (|Equals|_|) arg x = if (arg = x) then Some() else None

let incrementNumberInString (input: string) (digit: int) =
    let digitInInput = intFromChar input.[digit]
    let incremented = ((digitInInput + 1) % 10).ToString ()
    input.Remove(digit, 1).Insert(digit, incremented)

let convertDictToMap (dictionary : IDictionary<_,_>) = 
    dictionary |> Seq.map (|KeyValue|) |> Map.ofSeq

let mergeMaps (a: Map<'a,'b>) (b: Map<'a,'b>) = 
    Map.fold (fun acc key value -> Map.add key value acc) a b

let mergeDictsIntoMap (a: IDictionary<'a, 'b>) (b: IDictionary<'a, 'b>) =
    let aMap = convertDictToMap a
    let bMap = convertDictToMap b
    mergeMaps aMap bMap

let getRandom (isUsingManualSeed: bool) (seed: int) =
    match isUsingManualSeed with
    | true  -> Random(seed)
    | false -> Random()
