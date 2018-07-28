module SpecialBirthDateReplaces

open System
open System.Text.RegularExpressions
open CommonTemplatePrint

let getValueForBirthDate (birthDateString: string) =
    birthDateString.Split('(').[1]
    |> removeLastParenthesis
    |> cleanupValue

let replaceBirthDate (remainingString: string) (birthDatePattern: string) (birthDateString: string) (birthDate: DateTime) =
    let datetimeFormatInString = getValueForBirthDate birthDateString
    let regex = Regex birthDatePattern
    regex.Replace(remainingString, birthDate.ToString(datetimeFormatInString), 1)

let performSpecialBirthDateReplaces (birthDate: DateTime) (stringToDoReplaces: string) =
    let birthDatePattern = "#{BirthDate\(\s?[dfFghHKmMstyz ,\/-]+\s?\)}"
    let birthDateRegex = Regex birthDatePattern

    let rec loop (remaining: string) =
        let matching = birthDateRegex.Match remaining

        let modified = match matching.Success with
                       | true  -> replaceBirthDate remaining birthDatePattern matching.Value birthDate
                       | false -> remaining

        let isMoreRemaining = (birthDateRegex.Match modified).Success
        match isMoreRemaining with
        | true  -> loop modified
        | false -> modified
            
    loop stringToDoReplaces
