module SpecialBirthDateReplaces

open System
open System.Text.RegularExpressions
open System.Globalization

let modifyWithoutCulture (regex: Regex) (birthDate: DateTime) (remaining: string) =
    let matching = regex.Match remaining

    let birthDateFormat = matching.Groups.[1].Value

    match matching.Success with
    | true  -> regex.Replace(remaining, birthDate.ToString(birthDateFormat), 1)
    | false -> remaining

let modifyWithCulture (regex: Regex) (birthDate: DateTime) (remaining: string) =
    let matching = regex.Match remaining

    let birthDateFormat = matching.Groups.[1].Value
    let culture = matching.Groups.[2].Value

    match matching.Success with
    | true  -> regex.Replace(remaining, birthDate.ToString(birthDateFormat, CultureInfo.CreateSpecificCulture(culture)), 1)
    | false -> remaining

let performSpecialBirthDateReplaces (birthDate: DateTime) (stringToDoReplaces: string) =
    let birthDateRegex            = Regex "#{BirthDate\(\s?([dfFghHKmMstyz ,\/-]+)\s?\)}"
    let birthDateWithCultureRegex = Regex "#{BirthDate\(\s?([dfFghHKmMstyz ,\/-]+)\s?,\s?([a-zA-Z-]+)\s?\)}"

    let rec loop (remaining: string) =

        let modified = remaining
                       |> modifyWithoutCulture birthDateRegex birthDate
                       |> modifyWithCulture    birthDateWithCultureRegex birthDate

        let isMoreRemaining = (birthDateRegex.Match modified).Success
                           || (birthDateWithCultureRegex.Match modified).Success
        match isMoreRemaining with
        | true  -> loop modified
        | false -> modified
            
    loop stringToDoReplaces
