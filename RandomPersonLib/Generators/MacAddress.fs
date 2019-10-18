module internal MacAddress

open System
open StringUtil
open Util

let replaceCharWithRandomHexadecimalNumber (random: Random) (replaceChar: char) (inputChar: char) =
    match inputChar with
    | c when c = replaceChar -> random.Next(1, 16) |> sprintf "%x" |> stringAsChar
    | _                      -> inputChar

let replaceHyphensWithColons (originalMacAddress: string) = originalMacAddress.Replace("-", ":")

let modifyMacAddress (useColons: bool) (useUppercase: bool) (originalMacAddress: string) =
    match useColons, useUppercase with
    | (true, true)   -> originalMacAddress |> replaceHyphensWithColons |> uppercase
    | (true, false)  -> originalMacAddress |> replaceHyphensWithColons
    | (false, true)  -> originalMacAddress |> uppercase
    | (false, false) -> originalMacAddress

let generateMacAddress (random: Random) (useColons: bool) (useUppercase: bool) =
    "xx-xx-xx-xx-xx-xx"
    |> toCharArray
    |> Array.map (replaceCharWithRandomHexadecimalNumber random 'x')
    |> String
    |> modifyMacAddress useColons useUppercase
