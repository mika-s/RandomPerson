module RandomReplaces

open System
open System.Text.RegularExpressions
open Util

let random = Random()

let randomInt (matching: Match) =
    let min = int matching.Groups.[1].Value
    let max = int matching.Groups.[2].Value
    randomIntBetween min max |> sprintf "%d"

let randomIntWithStep (matching: Match) =
    let min =  int matching.Groups.[1].Value
    let step = int matching.Groups.[2].Value
    let max =  int matching.Groups.[3].Value
    randomIntBetweenWithStep min step max |> sprintf "%d"

let randomFloat  (matching: Match) =
    let min = float matching.Groups.[1].Value
    let max = float matching.Groups.[2].Value
    randomFloatBetween min max |> sprintf "%.3f"

let randomFloatWithStep (matching: Match) =
    let min  = float matching.Groups.[1].Value
    let step = float matching.Groups.[2].Value
    let max  = float matching.Groups.[3].Value
    randomFloatBetweenWithStep min step max  |> sprintf "%.3f"

let randomFloatWithDecimals (matching: Match) =
    let decimals = int   matching.Groups.[1].Value
    let min      = float matching.Groups.[2].Value
    let max      = float matching.Groups.[3].Value
    let printfFormat = decimals |> sprintf "%%.%df" |> Printf.StringFormat<float->string>
    randomFloatBetween min max |> sprintf printfFormat

let randomFloatWithDecimalsWithStep (matching: Match) =
    let decimals = int   matching.Groups.[1].Value
    let min      = float matching.Groups.[2].Value
    let step     = float matching.Groups.[3].Value
    let max      = float matching.Groups.[4].Value
    let printfFormat = decimals |> sprintf "%%.%df" |> Printf.StringFormat<float->string>
    randomFloatBetweenWithStep min step max |> sprintf printfFormat

let randomSwitch (matching: Match) =
    let min, max = 0, matching.Groups.[1].Captures.Count - 1
    let randomNumber = randomIntBetween min max
    matching.Groups.[1].Captures.[randomNumber].Value

let replace (replaceFunction:  Match -> string) (regex: Regex) (remaining: string) =
    let matching = regex.Match remaining

    match matching.Success with
    | true  -> 
        let replaceValue = replaceFunction matching
        regex.Replace(remaining, replaceValue, 1)
    | false -> remaining

let performRandomReplaces (stringToDoReplaces: string) =
    let randomIntRegex                        = Regex "#{Random\(\s?int\s?,\s?(-?\d+)\s?,\s?(-?\d+)\s?\)}"
    let randomIntWithStepSizeRegex            = Regex "#{Random\(\s?int\s?,\s?(-?\d+)\s?,\s?(-?\d+)\s?,\s?(-?\d+)\s?\)}"
    let randomFloatRegex                      = Regex "#{Random\(\s?float\s?,\s?(-?\d+.\d+|-?\d+)\s?,\s?(-?\d+.\d+|-?\d+)\s?\)}"
    let randomFloatWithStepRegex              = Regex "#{Random\(\s?float\s?,\s?(-?\d+.\d+|-?\d+)\s?,\s?(-?\d+.\d+|-?\d+)\s?,\s?(-?\d+.\d+|-?\d+)\s?\)}"
    let randomFloatWithDecimalsRegex          = Regex "#{Random\(\s?float:(\d+)\s?,\s?(-?\d+.\d+|-?\d+)\s?,\s?(-?\d+.\d+|-?\d+)\s?\)}"
    let randomFloatWithDecimalsWithStepRegex  = Regex "#{Random\(\s?float:(\d+)\s?,\s?(-?\d+.\d+|-?\d+)\s?,\s?(-?\d+.\d+|-?\d+)\s?,\s?(-?\d+.\d+|-?\d+)\s?\)}"
    let randomSwitchRegex                     = Regex "#{Random\((?:switch,)\s?(?:\s*'([\w- \\\/,]+)',?){2,}\)}"

    let rec loop (remaining: string) =
        let modified = remaining
                       |> replace randomInt                       randomIntRegex
                       |> replace randomIntWithStep               randomIntWithStepSizeRegex
                       |> replace randomFloat                     randomFloatRegex
                       |> replace randomFloatWithStep             randomFloatWithStepRegex
                       |> replace randomFloatWithDecimals         randomFloatWithDecimalsRegex
                       |> replace randomFloatWithDecimalsWithStep randomFloatWithDecimalsWithStepRegex
                       |> replace randomSwitch                    randomSwitchRegex

        let isMoreRemaining = (randomIntRegex.Match                       modified).Success 
                           || (randomIntWithStepSizeRegex.Match           modified).Success
                           || (randomFloatRegex.Match                     modified).Success
                           || (randomFloatWithStepRegex.Match             modified).Success
                           || (randomFloatWithDecimalsRegex.Match         modified).Success
                           || (randomFloatWithDecimalsWithStepRegex.Match modified).Success
                           || (randomSwitchRegex.Match                    modified).Success

        match isMoreRemaining with
        | true  -> loop modified
        | false -> modified
            
    loop stringToDoReplaces
