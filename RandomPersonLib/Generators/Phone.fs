module internal Phone

open System

let replaceCharWithRandomNumber (random: Random) (inputChar: char) =
    match inputChar with
    | c when c = 'x' ->
        let asInt = random.Next(1, 10)
        let asString = sprintf "%d" asInt
        let asChar = asString.[0]
        asChar
    | _              -> inputChar

let addTheCountryCode (countryCode: int) (originalPhoneNumber: string) =
    sprintf "+%d %s" countryCode originalPhoneNumber

let addTheTrunkPrefix (trunkPrefix: string) (originalPhoneNumber: string) =
    sprintf "%s%s" trunkPrefix originalPhoneNumber

let removeTheHyphens (originalPhoneNumber: string) = originalPhoneNumber.Replace("-", "")
let removeTheSpace   (originalPhoneNumber: string) = originalPhoneNumber.Replace(" ", "")

let modifyPhoneNumber (originalPhoneNumber: string) (countryCode: int) (trunkPrefix: string) (addCountryCode: bool) (removeHyphen: bool) (removeSpace: bool) =
    match addCountryCode, removeHyphen, removeSpace with
    | (true, true, true)    -> addTheCountryCode countryCode originalPhoneNumber |> removeTheHyphens |> removeTheSpace
    | (true, true, false)   -> addTheCountryCode countryCode originalPhoneNumber |> removeTheHyphens
    | (true, false, false)  -> addTheCountryCode countryCode originalPhoneNumber
    | (false, true, true)   -> addTheTrunkPrefix trunkPrefix originalPhoneNumber |> removeTheHyphens |> removeTheSpace
    | (false, true, false)  -> addTheTrunkPrefix trunkPrefix originalPhoneNumber |> removeTheHyphens
    | (false, false, true)  -> addTheTrunkPrefix trunkPrefix originalPhoneNumber |> removeTheSpace
    | (true, false, true)   -> addTheCountryCode countryCode originalPhoneNumber |> removeTheSpace
    | (false, false, false) -> addTheTrunkPrefix trunkPrefix originalPhoneNumber

let generatePhone (random: Random) (countryCode: int) (trunkPrefix: string) (ranges: string[]) (addCountryCode: bool)  (removeHyphen: bool) (removeSpace: bool) = 
    let randomNumber = random.Next(ranges.Length)
    let pickedRange = ranges.[randomNumber]
    let asChars = pickedRange.ToCharArray()
    let replaced = Array.map (replaceCharWithRandomNumber random) asChars
    let asString = System.String(replaced)

    modifyPhoneNumber asString countryCode trunkPrefix addCountryCode removeHyphen removeSpace
