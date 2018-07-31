module RandomReplaces

open System
open System.Text.RegularExpressions
open Util

let random = Random()

let replaceRandomInt (regex: Regex) (remaining: string) =
    let matching = regex.Match remaining

    match matching.Success with
    | true  ->
        let min = int matching.Groups.[1].Value
        let max = int matching.Groups.[2].Value
        let randomValue = randomIntBetween min max
        regex.Replace(remaining, randomValue.ToString(), 1)
    | false -> remaining

let replaceRandomIntWithStep (regex: Regex) (remaining: string) =
    let matching = regex.Match remaining

    match matching.Success with
    | true  ->
        let min =  int matching.Groups.[1].Value
        let step = int matching.Groups.[2].Value
        let max =  int matching.Groups.[3].Value
        let randomValue = randomIntBetweenWithStep min step max
        regex.Replace(remaining, randomValue.ToString(), 1)
    | false -> remaining

let replaceRandomFloat (regex: Regex) (remaining: string) =
    let matching = regex.Match remaining

    match matching.Success with
    | true  ->
        let min = float matching.Groups.[1].Value
        let max = float matching.Groups.[2].Value
        let randomValue = randomFloatBetween min max
        regex.Replace(remaining, randomValue.ToString(), 1)
    | false -> remaining

let replaceRandomFloatWithDecimals (regex: Regex) (remaining: string) =
    let matching = regex.Match remaining

    match matching.Success with
    | true  ->
        let decimals = int   matching.Groups.[1].Value
        let min      = float matching.Groups.[2].Value
        let max      = float matching.Groups.[3].Value
        
        let printfFormat = decimals |> sprintf "%%.%df" |> Printf.StringFormat<float->string>
        let numberAsString = randomFloatBetween min max |> sprintf printfFormat

        regex.Replace(remaining, numberAsString, 1)
    | false -> remaining

let replaceRandomSwitch (regex: Regex) (remaining: string) =
    let matching = regex.Match remaining

    match matching.Success with
    | true  ->
        let randomNumber = randomIntBetween 0 (matching.Groups.[1].Captures.Count - 1)
        let randomValue = matching.Groups.[1].Captures.[randomNumber].Value
        regex.Replace(remaining, randomValue, 1)
    | false -> remaining

let performRandomReplaces (stringToDoReplaces: string) =
    let randomIntRegex                 = Regex "#{Random\(\s?int\s?,\s?(-?\d+)\s?,\s?(-?\d+)\s?\)}"
    let randomIntWithStepSizeRegex     = Regex "#{Random\(\s?int\s?,\s?(-?\d+)\s?,\s?(-?\d+)\s?,\s?(-?\d+)\s?\)}"
    let randomFloatRegex               = Regex "#{Random\(\s?float\s?,\s?(-?\d+.\d+|-?\d+)\s?,\s?(-?\d+.\d+|-?\d+)\s?\)}"
    let randomFloatWithDecimalsNoRegex = Regex "#{Random\(\s?float:(\d+)\s?,\s?(-?\d+.\d+|-?\d+)\s?,\s?(-?\d+.\d+|-?\d+)\s?\)}"
    let randomSwitchRegex              = Regex "#{Random\((?:switch,+)\s?(?:\s*([\w']+),?){2,}\)}"

    let rec loop (remaining: string) =
        let modified = remaining
                       |> replaceRandomInt                randomIntRegex
                       |> replaceRandomIntWithStep        randomIntWithStepSizeRegex
                       |> replaceRandomFloat              randomFloatRegex
                       |> replaceRandomFloatWithDecimals  randomFloatWithDecimalsNoRegex
                       |> replaceRandomSwitch             randomSwitchRegex

        let isMoreRemaining = (randomIntRegex.Match                 modified).Success 
                           || (randomIntWithStepSizeRegex.Match     modified).Success
                           || (randomFloatRegex.Match               modified).Success
                           || (randomFloatWithDecimalsNoRegex.Match modified).Success
                           || (randomSwitchRegex.Match              modified).Success

        match isMoreRemaining with
        | true  -> loop modified
        | false -> modified
            
    loop stringToDoReplaces
