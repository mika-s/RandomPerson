module SpecialDateReplaces

open System
open System.Globalization
open System.Text.RegularExpressions

let nowWithoutFormatAndCulture (_: Match) =
    DateTime.Now.ToString("yyyy-MM-dd")

let nowWithFormat (matching: Match) =
    let format = matching.Groups.[1].Value
    DateTime.Now.ToString(format)

let nowWithFormatAndCulture (matching: Match) =
    let format  = matching.Groups.[1].Value
    let culture = matching.Groups.[2].Value
    DateTime.Now.ToString(format, CultureInfo.CreateSpecificCulture(culture))

let daysWithoutFormatAndCulture (matching: Match) =
    let days = matching.Groups.[1].Value
    DateTime.Now.AddDays(float days).ToString("yyyy-MM-dd")

let daysWithFormat (matching: Match) =
    let days   = matching.Groups.[1].Value
    let format = matching.Groups.[2].Value
    DateTime.Now.AddDays(float days).ToString(format)

let daysWithFormatAndCulture (matching: Match) =
    let days    = matching.Groups.[1].Value
    let format  = matching.Groups.[2].Value
    let culture = matching.Groups.[3].Value
    DateTime.Now.AddDays(float days).ToString(format, CultureInfo.CreateSpecificCulture(culture))

let replace (replaceFunction: Match -> string) (regex: Regex) (remaining: string) =
    let matching = regex.Match remaining

    match matching.Success with
    | true  -> 
        let replaceValue = replaceFunction matching
        regex.Replace(remaining, replaceValue, 1)
    | false -> remaining

let performSpecialDateReplaces (stringToDoReplaces: string) =
    let dateNowRegex                      = Regex "#{Date\(\s?'now'\s?\)}"
    let dateNowWithFormatRegex            = Regex "#{Date\(\s?'now'\s?,\s?'([dfFghHKmMstyz ,\/-]+)'\s?\)}"
    let dateNowWithFormatAndCultureRegex  = Regex "#{Date\(\s?'now'\s?,\s?'([dfFghHKmMstyz ,\/-]+)'\s?,\s?'([a-zA-Z-]+)'\s?\)}"
    let dateDaysRegex                     = Regex "#{Date\(\s?([-+]?\d+)\s?\)}"
    let dateDaysWithFormatRegex           = Regex "#{Date\(\s?([-+]?\d+)\s?,\s?'([dfFghHKmMstyz ,\/-]+)'\s?\)}"
    let dateDaysWithFormatAndCultureRegex = Regex "#{Date\(\s?([-+]?\d+)\s?,\s?'([dfFghHKmMstyz ,\/-]+)'\s?,\s?'([a-zA-Z-]+)'\s?\)}"

    let rec loop (remaining: string) =
        let modified = remaining
                       |> replace nowWithoutFormatAndCulture  dateNowRegex
                       |> replace nowWithFormat               dateNowWithFormatRegex
                       |> replace nowWithFormatAndCulture     dateNowWithFormatAndCultureRegex
                       |> replace daysWithoutFormatAndCulture dateDaysRegex
                       |> replace daysWithFormat              dateDaysWithFormatRegex
                       |> replace daysWithFormatAndCulture    dateDaysWithFormatAndCultureRegex

        let isMoreRemaining = (dateNowRegex.Match                      modified).Success
                           || (dateNowWithFormatRegex.Match            modified).Success
                           || (dateNowWithFormatAndCultureRegex.Match  modified).Success
                           || (dateDaysRegex.Match                     modified).Success
                           || (dateDaysWithFormatRegex.Match           modified).Success
                           || (dateDaysWithFormatAndCultureRegex.Match modified).Success

        match isMoreRemaining with
        | true  -> loop modified
        | false -> modified
            
    loop stringToDoReplaces
