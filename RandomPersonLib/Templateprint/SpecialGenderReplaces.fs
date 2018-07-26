module SpecialGenderReplaces

open System.Text.RegularExpressions
open CommonTemplatePrint
open RandomPersonLib

let getValuesForGender (genderString: string) =
    genderString.Split('(').[1].Split(',')
    |> removeLastParenthesisFromArray
    |> Array.map(cleanupValue)

let replaceGender (remainingString: string) (genderPattern: string) (genderString: string) (gender: Gender) =
    let gendersInString = getValuesForGender genderString
    let regex = Regex genderPattern
    regex.Replace(remainingString, gendersInString.[int gender - 1], 1)

let performSpecialGenderReplaces (gender: Gender) (stringToDoReplaces: string) =
    let genderPattern = "#{Gender\(\s?\w+\s?,\s?\w+\s?\)}"
    let genderRegex = Regex genderPattern

    let rec loop (remaining: string) =
        let matching = genderRegex.Match remaining

        let modified = match matching.Success with
                       | true  -> replaceGender remaining genderPattern matching.Value gender
                       | false -> remaining

        let isMoreRemaining = (genderRegex.Match modified).Success
        match isMoreRemaining with
        | true  -> loop modified
        | false -> modified
            
    loop stringToDoReplaces