module internal Util

open System
open System.IO
open System.Runtime.Serialization.Json
open System.Text

let modulus (x: int) (y: int) = y % x
let isOdd  (x: int) = x % 2 <> 0
let isEven (x: int) = x % 2 =  0
let intFromChar (x: char) = x |> Char.GetNumericValue |> int
let intArrayFromString (numbersStr: string) = numbersStr |> Seq.toArray |> Array.map intFromChar
let stringAsChar (str: string) = str.[0]
let roundToNearest (rounding: float) (x: float) = Math.Round(x / rounding) * rounding
let nullCoalesce (value: Nullable<'T>) (otherValue: 'T) = if value.HasValue then value.Value else otherValue

let inline toCharArray (str: string) = str.ToCharArray()
let uppercase (str: string) = str.ToUpperInvariant()
let lowercase (str: string) = str.ToLowerInvariant()

let capitalize (str: string) =
    if str.Length = 0 then str
    else uppercase str.[0..0] + str.[ 1 .. str.Length - 1 ]

let uncapitalize (str: string) =
    if str.Length = 0 then str
    else lowercase str.[0..0] + str.[ 1 .. str.Length - 1 ]

let firstUppercaseRestLowercase (str: string) =
    if str.Length = 0 then str
    else uppercase str.[0..0] + lowercase str.[ 1 .. str.Length - 1 ]

let randomForTemplateMode = Random()

let randomIntBetween (min: int) (max: int) = randomForTemplateMode.Next(min, max + 1)
let randomIntBetweenWithStep (min: int) (step: int) (max: int) = (randomIntBetween 0 ((max - min) / step)) * step + min

let randomFloatBetween (min: float) (max: float) = randomForTemplateMode.NextDouble() * (max - min) + min
let randomFloatBetweenWithStep (min: float) (step: float) (max: float) = (float (randomIntBetween 0 (int ((max - min) / step))))  * step + min

let generateRandomNumberString (random: Random) (amount: int) (min: int) (max: int) =
    ("", [1 .. amount]) ||> List.fold (fun state _ -> state + sprintf "%d" (random.Next(min, max)))

let randomUppercaseLetter (random: Random) =
    let alphabet = "ABCDEFGHIJKLMNOPQRSTUVWXYZ"
    let randomNumber = random.Next(0, alphabet.Length - 1)
    alphabet.[randomNumber]

let boxMullerTransform () =
    let u1 = randomForTemplateMode.NextDouble()
    let u2 = randomForTemplateMode.NextDouble()
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

let incrementAtPosition (digit: int) (input: string) =
    let digitInInput = intFromChar input.[digit]
    let incremented = ((digitInInput + 1) % 10).ToString ()
    input.Remove(digit, 1).Insert(digit, incremented)

let getRandom (isUsingManualSeed: bool) (seed: int) =
    match isUsingManualSeed with
    | true  -> Random(seed)
    | false -> Random()
