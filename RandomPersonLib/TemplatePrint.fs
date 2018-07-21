module internal TemplatePrint

open System
open System.Text.RegularExpressions
open Util
open RandomPersonLib

let random = Random()

let parseOrdinaryReplaces (originalOutput: string) (person: Person) =
    let mapping = 
        [
            "SSN", box person.SSN;
            "Email", box person.Email;
            "Password", box person.Password;
            "FirstName", box person.FirstName;
            "LastName", box person.LastName;
            "Address1", box person.Address1;
            "Address2", box person.Address2;
            "PostalCode", box person.PostalCode;
            "City", box person.City;
            "Nationality", box person.Nationality;
            "BirthDate", box person.BirthDate;
            "Gender", box person.Gender;
            "MobilePhone", box person.MobilePhone;
            "HomePhone", box person.HomePhone;
        ]

    let replaceOrdinary (str: string) =
        let ordinaryFolder (acc: string) (y: string * obj) =
            let valueBoxed = snd y
            let value = match valueBoxed.GetType() with
                        | x when x = typedefof<string>      -> string (unbox valueBoxed)
                        | x when x = typedefof<Gender>      -> (valueBoxed :?> Gender).ToString()
                        | x when x = typedefof<Nationality> -> (valueBoxed :?> Nationality).ToString()
                        | x when x = typedefof<DateTime>    -> (valueBoxed :?> DateTime).ToString()
                        | _ -> failwith "Error in ordinaryFolder"

            acc.Replace(String.Format("#{{{0}}}", fst y), value)

        List.fold ordinaryFolder str mapping

    let replaceToLower (str: string) =
        let toLowerFolder (acc: string) (y: string * obj) =
            let valueBoxed = snd y
            let value = match valueBoxed.GetType() with
                        | x when x = typedefof<string>      -> (string (unbox valueBoxed)).ToLower()
                        | x when x = typedefof<Gender>      -> (valueBoxed :?> Gender).ToString().ToLower()
                        | x when x = typedefof<Nationality> -> (valueBoxed :?> Nationality).ToString().ToLower()
                        | x when x = typedefof<DateTime>    -> (valueBoxed :?> DateTime).ToString().ToLower()
                        | _ -> failwith "Error in toLowerFolder"

            acc.Replace(String.Format("#{{{0}.ToLower()}}", fst y), value)

        List.fold toLowerFolder str mapping

    let replaceToUpper (str: string) =
        let toUpperFolder (acc: string) (y: string * obj) =
            let valueBoxed = snd y
            let value = match valueBoxed.GetType() with
                        | x when x = typedefof<string>      -> (string (unbox valueBoxed)).ToUpper()
                        | x when x = typedefof<Gender>      -> (valueBoxed :?> Gender).ToString().ToUpper()
                        | x when x = typedefof<Nationality> -> (valueBoxed :?> Nationality).ToString().ToUpper()
                        | x when x = typedefof<DateTime>    -> (valueBoxed :?> DateTime).ToString().ToUpper()
                        | _ -> failwith "Error in toUpperFolder"

            acc.Replace(String.Format("#{{{0}.ToUpper()}}", fst y), value)

        List.fold toUpperFolder str mapping

    originalOutput |> replaceOrdinary |> replaceToLower |> replaceToUpper

let cleanupValue (input: string) = input.Trim()

let rec removeLastParenthesis (input: string) = 
    let lastIdx = input.Length - 1
    
    let maybeCleaned = match input.[lastIdx] with
                       | '}' | ')' -> input.Remove(lastIdx)
                       | _         -> input
    
    let newLastIdx = maybeCleaned.Length - 1
    let lastCharacter = maybeCleaned.[newLastIdx]
    
    if (lastCharacter <> ')' && lastCharacter <> '}') then
        maybeCleaned
    else
        removeLastParenthesis (maybeCleaned)

let removeLastParenthesisFromArray (input: string[]) =
    let lastElemIdx = input.Length - 1
    let lastString = input.[lastElemIdx]
    
    let newArrayPart1 = input.[.. input.Length - 2]
    let newArrayPart2 = removeLastParenthesis (lastString) |> Array.create 1
    
    Array.concat [ newArrayPart1; newArrayPart2 ]

let getValueForRandomInt (randomString: string) =
    let splitString = randomString.Split(',')
    let min = splitString.[1] |> cleanupValue |> int
    let max = splitString.[2] |> removeLastParenthesis |> cleanupValue |> int
    randomIntBetween min max

let replaceRandomInt (remainingString: string) (randomIntPattern: string) (randomString: string) =
    let numberAsString = randomString |> getValueForRandomInt |> sprintf "%d"
    let regex = Regex randomIntPattern
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

let parseSpecialReplaces (stringTodoReplaces: string) =
    let randomIntPattern = "#{Random\(\s?int\s?,\s?-?\d+\s?,\s?-?\d+\s?\)}"
    let randomIntRegex = Regex randomIntPattern

    let randomFloatPattern = "#{Random\(\s?float\s?,\s?(-?\d+.\d+|-?\d+)\s?,\s?(-?\d+.\d+|-?\d+)\s?\)}"
    let randomFloatRegex = Regex randomFloatPattern

    let randomFloatWithDecimalsNoPattern = "#{Random\(\s?float:\d+\s?,\s?(-?\d+.\d+|-?\d+)\s?,\s?(-?\d+.\d+|-?\d+)\s?\)}"
    let randomFloatWithDecimalsNoRegex = Regex randomFloatWithDecimalsNoPattern

    let randomSwitchPattern = "#{Random\(\s?switch\s?,(\s?['\w\,\\\/]+\s?,)+\s?['\w\,\\\/]+\s?\)}"
    let randomSwitchRegex = Regex randomSwitchPattern

    let rec loop (remaining: string) =
        let modified = modifyString    randomIntRegex                 randomIntPattern                 replaceRandomInt               remaining
                       |> modifyString randomFloatRegex               randomFloatPattern               replaceRandomFloat
                       |> modifyString randomFloatWithDecimalsNoRegex randomFloatWithDecimalsNoPattern replaceRandomFloatWithDecimals
                       |> modifyString randomSwitchRegex              randomSwitchPattern              replaceRandomSwitch

        if (randomIntRegex.Match modified).Success || (randomFloatRegex.Match modified).Success then
            loop modified
        else
            modified

    loop stringTodoReplaces

let printForTemplateMode (originalOutput: string) (person: Person) =
    parseOrdinaryReplaces originalOutput person
    |> parseSpecialReplaces
