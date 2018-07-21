module internal TemplatePrint

open System
open System.Text.RegularExpressions
open Util
open RandomPersonLib

let random = Random()

let parseOrdinaryReplaces (originalOutput: string) (person: Person) =
    let mapping = 
        [
            "SSN", person.SSN;
            "Email", person.Email;
            "Password", person.Password;
            "FirstName", person.FirstName;
            "LastName", person.LastName;
            "Address1", person.Address1;
            "Address2", person.Address2;
            "PostalCode", person.PostalCode;
            "City", person.City;
            "Nationality", person.Nationality.ToString();
            "BirthDate", person.BirthDate.ToString();
            "Gender", person.Gender.ToString();
            "MobilePhone", person.MobilePhone;
            "HomePhone", person.HomePhone;
        ]

    let replaceOrdinary (str: string) =
        let ordinaryFolder (acc: string) (y: string * string) = acc.Replace(String.Format("#{{{0}}}", fst y), snd y)
        List.fold ordinaryFolder str mapping

    let replaceToLower (str: string) =
        let toLowerFolder (acc: string) (y: string * string) = acc.Replace(String.Format("#{{{0}.ToLower()}}", fst y), (snd y).ToLower())
        List.fold toLowerFolder str mapping

    let replaceToUpper (str: string) =
        let toUpperFolder (acc: string) (y: string * string) = acc.Replace(String.Format("#{{{0}.ToUpper()}}", fst y), (snd y).ToUpper())
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
    let cleanMin = splitString.[1] |> cleanupValue
    let cleanMax = removeLastParenthesis splitString.[2] |> cleanupValue
    let min = Convert.ToInt32 (cleanMin)
    let max = Convert.ToInt32 (cleanMax)
    randomIntBetween min max

let replaceRandomInt (remainingString: string) (randomIntPattern: string) (randomString: string) =
    let randomNumber = getValueForRandomInt randomString
    let numberAsString = sprintf "%d" randomNumber 
    let regex = Regex randomIntPattern
    regex.Replace(remainingString, numberAsString, 1)

let getValueForRandomFloat (randomString: string) =
    let splitString = randomString.Split(',')
    let cleanMin = splitString.[1] |> cleanupValue
    let cleanMax = removeLastParenthesis splitString.[2] |> cleanupValue
    let min = float (cleanMin)
    let max = float (cleanMax)
    randomFloatBetween min max

let replaceRandomFloat (remainingString: string) (randomFloatPattern: string) (randomString: string) =
    let randomNumber = getValueForRandomFloat randomString
    let numberAsString = sprintf "%.3f" randomNumber 
    let regex = Regex randomFloatPattern
    regex.Replace(remainingString, numberAsString, 1)

let getNumbersAfterDecimal (randomString: string) =
    let firstSplit = randomString.Split(',')
    let secondSplit = firstSplit.[0].Split('(')
    let thirdSplit  = secondSplit.[1].Split(':')
    let clean = thirdSplit.[1] |> cleanupValue
    int (clean)

let replaceRandomFloatWithDecimals (remainingString: string) (randomFloatPattern: string) (randomString: string) =
    let randomNumber = getValueForRandomFloat randomString
    let numberOfDigitsAfterDecimal = getNumbersAfterDecimal randomString
    let printfFormatString = sprintf "%%.%df" numberOfDigitsAfterDecimal
    let printfFormat = Printf.StringFormat<float->string>(printfFormatString)
    let numberAsString = sprintf printfFormat randomNumber
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
    let Random = getValueForRandomSwitch randomString
    let regex = Regex randomSwitchPattern
    regex.Replace(remainingString, Random, 1)

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
