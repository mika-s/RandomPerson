module SpecialGenderReplaces

open System.Text.RegularExpressions
open RandomPersonLib

let replaceGender (regex: Regex) (gender: Gender) (remaining: string) =
    let matching = regex.Match remaining

    match matching.Success with
    | true  ->
        let gendersInString = [| matching.Groups.[1].Value; matching.Groups.[2].Value |]
        regex.Replace(remaining, gendersInString.[int gender - 1], 1)
    | false -> remaining

let performSpecialGenderReplaces (gender: Gender) (stringToDoReplaces: string) =
    let genderRegex = Regex "#{Gender\(\s?'([\w-\\\/, ]+)'\s?,\s?'([\w-\\\/, ]+)'\s?\)}"

    let rec loop (remaining: string) =

        let modified = remaining
                       |> replaceGender genderRegex gender

        let isMoreRemaining = (genderRegex.Match modified).Success
        match isMoreRemaining with
        | true  -> loop modified
        | false -> modified
            
    loop stringToDoReplaces
