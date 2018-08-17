module internal Util

open System
open System.IO
open System.Runtime.Serialization.Json
open System.Text

let isOdd  (x: int) = x % 2 <> 0
let isEven (x: int) = x % 2 =  0
let intFromChar (x: char) = int(Char.GetNumericValue x)
let roundToNearest (rounding: float) (x: float) = Math.Round(x / rounding) * rounding

let uppercase (str: string) = str.ToUpper()
let lowercase (str: string) = str.ToLower()

let titlecase (str: string) =
    match str.Length with
    | length when 1 < length ->
        let lowered = str.Substring(1, str.Length - 1).ToLower()
        let firstCapitalized = str.[0].ToString().ToUpper()
        let result = String.Concat(firstCapitalized, lowered)
        result
    | _ -> String.Empty

let randomIntBetween (min: int) (max: int) = Random().Next(min, max + 1)
let randomIntBetweenWithStep (min: int) (step: int) (max: int) = (randomIntBetween 0 ((max - min) / step)) * step + min

let randomFloatBetween (min: float) (max: float) = Random().NextDouble() * (max - min) + min
let randomFloatBetweenWithStep (min: float) (step: float) (max: float) = (float (randomIntBetween 0 (int ((max - min) / step))))  * step + min

let boxMullerTransform () =
    let random = Random()
    let u1 = random.NextDouble()
    let u2 = random.NextDouble()
    let z0 = sqrt(-2.0 * log u1) * cos(2.0 * Math.PI * u2)
    let z1 = sqrt(-2.0 * log u1) * sin(2.0 * Math.PI * u2)

    z0, z1

let normallyDistributedInt (mean: int) (std: int) =
    let z0, _ = boxMullerTransform ()
    int (z0 * float std + float mean)

let normallyDistributedFloat (mean: float) (std: float) =
    let z0, _ = boxMullerTransform ()
    z0 * std + mean

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

let getRandom (isUsingManualSeed: bool) (seed: int) =
    match isUsingManualSeed with
    | true  -> Random(seed)
    | false -> Random()
