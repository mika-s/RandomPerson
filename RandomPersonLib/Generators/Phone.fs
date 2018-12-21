module internal Phone

open System
open Util

let replaceCharWithRandomNumber (random: Random) (replaceChar: char) (inputChar: char) =
    match inputChar with
    | c when c = replaceChar -> random.Next(1, 10) |> sprintf "%d" |> stringAsChar
    | _                      -> inputChar

let addTheCountryCode (countryCode: int) (originalPhoneNumber: string) =
    sprintf "+%d %s" countryCode originalPhoneNumber

let addTheTrunkPrefix (trunkPrefix: string) (originalPhoneNumber: string) =
    sprintf "%s%s" trunkPrefix originalPhoneNumber

let removeTheHyphens (originalPhoneNumber: string) = originalPhoneNumber.Replace("-", "")
let removeTheSpace   (originalPhoneNumber: string) = originalPhoneNumber.Replace(" ", "")

let modifyPhoneNumber (countryCode: int) (trunkPrefix: string) (addCountryCode: bool) (removeHyphen: bool) (removeSpace: bool) (originalPhoneNumber: string) =
    match addCountryCode, removeHyphen, removeSpace with
    | (true, true, true)    -> originalPhoneNumber |> addTheCountryCode countryCode |> removeTheHyphens |> removeTheSpace
    | (true, true, false)   -> originalPhoneNumber |> addTheCountryCode countryCode |> removeTheHyphens
    | (true, false, false)  -> originalPhoneNumber |> addTheCountryCode countryCode 
    | (false, true, true)   -> originalPhoneNumber |> addTheTrunkPrefix trunkPrefix |> removeTheHyphens |> removeTheSpace
    | (false, true, false)  -> originalPhoneNumber |> addTheTrunkPrefix trunkPrefix |> removeTheHyphens
    | (false, false, true)  -> originalPhoneNumber |> addTheTrunkPrefix trunkPrefix |> removeTheSpace
    | (true, false, true)   -> originalPhoneNumber |> addTheCountryCode countryCode |> removeTheSpace
    | (false, false, false) -> originalPhoneNumber |> addTheTrunkPrefix trunkPrefix 

let generatePhone (random: Random) (countryCode: int) (trunkPrefix: string) (ranges: string[]) (addCountryCode: bool)  (removeHyphen: bool) (removeSpace: bool) = 
    ranges.[random.Next(ranges.Length)]
    |> toCharArray
    |> Array.map (replaceCharWithRandomNumber random 'x')
    |> String
    |> modifyPhoneNumber countryCode trunkPrefix addCountryCode removeHyphen removeSpace
