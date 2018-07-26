module RandomReplaces

open System
open System.Text.RegularExpressions
open CommonTemplatePrint
open Util

let random = Random()

let getValueForRandomInt (randomString: string) =
    let splitString = randomString.Split(',')
    let min = splitString.[1] |> cleanupValue |> int
    let max = splitString.[2] |> removeLastParenthesis |> cleanupValue |> int
    randomIntBetween min max

let replaceRandomInt (remainingString: string) (randomIntPattern: string) (randomString: string) =
    let numberAsString = randomString |> getValueForRandomInt |> sprintf "%d"
    let regex = Regex randomIntPattern
    regex.Replace(remainingString, numberAsString, 1)

let getValueForRandomIntWithStep (randomString: string) =
    let splitString = randomString.Split(',')
    let min  = splitString.[1] |> cleanupValue |> int
    let step = splitString.[2] |> cleanupValue |> int
    let max  = splitString.[3] |> removeLastParenthesis |> cleanupValue |> int
    randomIntBetweenWithStep min step max

let replaceRandomIntWithStep (remainingString: string) (randomIntWithStepPattern: string) (randomString: string) =
    let numberAsString = randomString |> getValueForRandomIntWithStep |> sprintf "%d"
    let regex = Regex randomIntWithStepPattern
    regex.Replace(remainingString, numberAsString, 1)

let getValueForRandomFloat (randomString: string) =
    let splitString = randomString.Split(',')
    let min = splitString.[1] |> cleanupValue |> float
    let max = splitString.[2] |> removeLastParenthesis |> cleanupValue |> float
    randomFloatBetween min max

let replaceRandomFloat (remainingString: string) (randomFloatPattern: string) (randomString: string) =
    let numberAsString = randomString |> getValueForRandomFloat |> sprintf "%.3f" 
    let regex = Regex randomFloatPattern
    regex.Replace(remainingString, numberAsString, 1)

let getNumbersAfterDecimal (randomString: string) =
    randomString.Split(',').[0].Split('(').[1].Split(':').[1] |> cleanupValue |> int

let replaceRandomFloatWithDecimals (remainingString: string) (randomFloatPattern: string) (randomString: string) =
    let printfFormat = randomString |> getNumbersAfterDecimal |> sprintf "%%.%df" |> Printf.StringFormat<float->string>
    let numberAsString = randomString |> getValueForRandomFloat |> sprintf printfFormat
    let regex = Regex randomFloatPattern
    regex.Replace(remainingString, numberAsString, 1)

let getValueForRandomSwitch (randomString: string) =
    let specialSplitReplaceComma = "0x¤@$§|1"
    let replaceEscapedComma = randomString.Replace("\,", specialSplitReplaceComma)
    let splitString = replaceEscapedComma.Split(',') |> Array.map(fun x -> x.Replace(specialSplitReplaceComma, ","))

    let cleanValues = removeLastParenthesisFromArray splitString
                      |> Array.skip(1)
                      |> Array.map(cleanupValue)
    
    let randomNumber = random.Next(0, cleanValues.Length)
    cleanValues.[randomNumber]

let replaceRandomSwitch (remainingString: string) (randomSwitchPattern: string) (randomString: string) =
    let randomValue = getValueForRandomSwitch randomString
    let regex = Regex randomSwitchPattern
    regex.Replace(remainingString, randomValue, 1)

let modifyString (regex: Regex) (pattern: string) (replaceFunc: string -> string -> string -> string) (remaining: string) =
    let matching = regex.Match remaining
    match matching.Success with
    | true  -> replaceFunc remaining pattern matching.Value
    | false -> remaining

let performRandomReplaces (stringToDoReplaces: string) =
    let randomIntPattern = "#{Random\(\s?int\s?,\s?-?\d+\s?,\s?-?\d+\s?\)}"
    let randomIntRegex = Regex randomIntPattern

    let randomIntWithStepSizePattern = "#{Random\(\s?int\s?,\s?-?\d+\s?,\s?-?\d+\s?,\s?-?\d+\s?\)}"
    let randomIntWithStepSizeRegex = Regex randomIntWithStepSizePattern

    let randomFloatPattern = "#{Random\(\s?float\s?,\s?(-?\d+.\d+|-?\d+)\s?,\s?(-?\d+.\d+|-?\d+)\s?\)}"
    let randomFloatRegex = Regex randomFloatPattern

    let randomFloatWithDecimalsNoPattern = "#{Random\(\s?float:\d+\s?,\s?(-?\d+.\d+|-?\d+)\s?,\s?(-?\d+.\d+|-?\d+)\s?\)}"
    let randomFloatWithDecimalsNoRegex = Regex randomFloatWithDecimalsNoPattern

    let randomSwitchPattern = "#{Random\(\s?switch\s?,(\s?['\w\,\\\/]+\s?,)+\s?['\w\,\\\/]+\s?\)}"
    let randomSwitchRegex = Regex randomSwitchPattern

    let rec loop (remaining: string) =
        let modified = modifyString    randomIntRegex                 randomIntPattern                 replaceRandomInt               remaining
                       |> modifyString randomIntWithStepSizeRegex     randomIntWithStepSizePattern     replaceRandomIntWithStep
                       |> modifyString randomFloatRegex               randomFloatPattern               replaceRandomFloat
                       |> modifyString randomFloatWithDecimalsNoRegex randomFloatWithDecimalsNoPattern replaceRandomFloatWithDecimals
                       |> modifyString randomSwitchRegex              randomSwitchPattern              replaceRandomSwitch

        let isMoreRemaining = (randomIntRegex.Match                 modified).Success 
                           || (randomIntWithStepSizeRegex.Match     modified).Success
                           || (randomFloatRegex.Match               modified).Success
                           || (randomFloatWithDecimalsNoRegex.Match modified).Success
                           || (randomSwitchRegex.Match              modified).Success

        match isMoreRemaining with
        | true  -> loop modified
        | false -> modified
            
    loop stringToDoReplaces
