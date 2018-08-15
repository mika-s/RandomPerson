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

let randomNormallyDistributedInt (matching: Match) =
    let mean               = int matching.Groups.[1].Value
    let standard_deviation = int matching.Groups.[2].Value
    normallyDistributedInt mean standard_deviation |> sprintf "%d"

let randomNormallyDistributedIntWithStep (matching: Match) =
    let mean               = int   matching.Groups.[1].Value
    let standard_deviation = int   matching.Groups.[2].Value
    let rounding           = float matching.Groups.[3].Value
    normallyDistributedInt mean standard_deviation |> float |> roundToNearest rounding |> int |> sprintf "%d"

let randomNormallyDistributedFloat (matching: Match) =
    let mean               = float matching.Groups.[1].Value
    let standard_deviation = float matching.Groups.[2].Value
    normallyDistributedFloat mean standard_deviation |> sprintf "%.3f"

let randomNormallyDistributedFloatWithStep (matching: Match) =
    let mean               = float matching.Groups.[1].Value
    let standard_deviation = float matching.Groups.[2].Value
    let rounding           = float matching.Groups.[3].Value
    normallyDistributedFloat mean standard_deviation |> roundToNearest rounding |> sprintf "%.3f"

let randomNormallyDistributedFloatWithDecimals (matching: Match) =
    let decimals           = int   matching.Groups.[1].Value
    let mean               = float matching.Groups.[2].Value
    let standard_deviation = float matching.Groups.[3].Value
    let printfFormat = decimals |> sprintf "%%.%df" |> Printf.StringFormat<float->string>
    normallyDistributedFloat mean standard_deviation |> sprintf printfFormat

let randomNormallyDistributedFloatWithDecimalsWithStep (matching: Match) =
    let decimals           = int   matching.Groups.[1].Value
    let mean               = float matching.Groups.[2].Value
    let standard_deviation = float matching.Groups.[3].Value
    let rounding           = float matching.Groups.[4].Value
    let printfFormat = decimals |> sprintf "%%.%df" |> Printf.StringFormat<float->string>
    normallyDistributedFloat mean standard_deviation |> roundToNearest rounding |> sprintf printfFormat

let replace (replaceFunction:  Match -> string) (regex: Regex) (remaining: string) =
    let matching = regex.Match remaining

    match matching.Success with
    | true  -> 
        let replaceValue = replaceFunction matching
        regex.Replace(remaining, replaceValue, 1)
    | false -> remaining

let performRandomReplaces (stringToDoReplaces: string) =
    let randomIntRegex                         = Regex "#{Random\(\s?int\s?,\s?(-?\d+)\s?,\s?(-?\d+)\s?\)}"
    let randomIntWithStepSizeRegex             = Regex "#{Random\(\s?int\s?,\s?(-?\d+)\s?,\s?(-?\d+)\s?,\s?(-?\d+)\s?\)}"
    let randomFloatRegex                       = Regex "#{Random\(\s?float\s?,\s?(-?\d+\.\d+|-?\d+)\s?,\s?(-?\d+\.\d+|-?\d+)\s?\)}"
    let randomFloatWithStepRegex               = Regex "#{Random\(\s?float\s?,\s?(-?\d+\.\d+|-?\d+)\s?,\s?(-?\d+\.\d+|-?\d+)\s?,\s?(-?\d+\.\d+|-?\d+)\s?\)}"
    let randomFloatWithDecimalsRegex           = Regex "#{Random\(\s?float:(\d+)\s?,\s?(-?\d+\.\d+|-?\d+)\s?,\s?(-?\d+\.\d+|-?\d+)\s?\)}"
    let randomFloatWithDecimalsWithStepRegex   = Regex "#{Random\(\s?float:(\d+)\s?,\s?(-?\d+\.\d+|-?\d+)\s?,\s?(-?\d+\.\d+|-?\d+)\s?,\s?(-?\d+\.\d+|-?\d+)\s?\)}"
    let randomSwitchRegex                      = Regex "#{Random\((?:switch,)\s?(?:\s*'([\w- \\\/,]+)',?){2,}\)}"
    let randomNdIntRegex                       = Regex "#{Random\(\s?nd_int\s?,\s?(-?\d+)\s?,\s?(-?\d+)\s?\)}"
    let randomNdIntWithStepSizeRegex           = Regex "#{Random\(\s?nd_int\s?,\s?(-?\d+)\s?,\s?(-?\d+)\s?,\s?(-?\d+)\s?\)}"
    let randomNdFloatRegex                     = Regex "#{Random\(\s?nd_float\s?,\s?(-?\d+\.\d+|-?\d+)\s?,\s?(-?\d+\.\d+|-?\d+)\s?\)}"
    let randomNdFloatWithStepRegex             = Regex "#{Random\(\s?nd_float\s?,\s?(-?\d+\.\d+|-?\d+)\s?,\s?(-?\d+\.\d+|-?\d+)\s?,\s?(-?\d+\.\d+|-?\d+)\s?\)}"
    let randomNdFloatWithDecimalsRegex         = Regex "#{Random\(\s?nd_float:(\d+)\s?,\s?(-?\d+\.\d+|-?\d+)\s?,\s?(-?\d+\.\d+|-?\d+)\s?\)}"
    let randomNdFloatWithDecimalsWithStepRegex = Regex "#{Random\(\s?nd_float:(\d+)\s?,\s?(-?\d+\.\d+|-?\d+)\s?,\s?(-?\d+\.\d+|-?\d+)\s?,\s?(-?\d+\.\d+|-?\d+)\s?\)}"

    let rec loop (remaining: string) =
        let modified = remaining
                       |> replace randomInt                                          randomIntRegex
                       |> replace randomIntWithStep                                  randomIntWithStepSizeRegex
                       |> replace randomFloat                                        randomFloatRegex
                       |> replace randomFloatWithStep                                randomFloatWithStepRegex
                       |> replace randomFloatWithDecimals                            randomFloatWithDecimalsRegex
                       |> replace randomFloatWithDecimalsWithStep                    randomFloatWithDecimalsWithStepRegex
                       |> replace randomSwitch                                       randomSwitchRegex
                       |> replace randomNormallyDistributedInt                       randomNdIntRegex
                       |> replace randomNormallyDistributedIntWithStep               randomNdIntWithStepSizeRegex
                       |> replace randomNormallyDistributedFloat                     randomNdFloatRegex
                       |> replace randomNormallyDistributedFloatWithStep             randomNdFloatWithStepRegex
                       |> replace randomNormallyDistributedFloatWithDecimals         randomNdFloatWithDecimalsRegex
                       |> replace randomNormallyDistributedFloatWithDecimalsWithStep randomNdFloatWithDecimalsWithStepRegex

        let isMoreRemaining = (randomIntRegex.Match                         modified).Success 
                           || (randomIntWithStepSizeRegex.Match             modified).Success
                           || (randomFloatRegex.Match                       modified).Success
                           || (randomFloatWithStepRegex.Match               modified).Success
                           || (randomFloatWithDecimalsRegex.Match           modified).Success
                           || (randomFloatWithDecimalsWithStepRegex.Match   modified).Success
                           || (randomSwitchRegex.Match                      modified).Success
                           || (randomNdIntRegex.Match                       modified).Success
                           || (randomNdIntWithStepSizeRegex.Match           modified).Success
                           || (randomNdFloatRegex.Match                     modified).Success
                           || (randomNdFloatWithStepRegex.Match             modified).Success
                           || (randomNdFloatWithDecimalsRegex.Match         modified).Success
                           || (randomNdFloatWithDecimalsWithStepRegex.Match modified).Success

        match isMoreRemaining with
        | true  -> loop modified
        | false -> modified
            
    loop stringToDoReplaces
