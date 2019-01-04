module internal ValidatePAN

open StringUtil
open Creditcard
open System.Text.RegularExpressions

let validatePAN (rawPan: string) =
    let pan = rawPan |> trim |> removeChar "-" |> removeChar " "

    let panRegex = Regex "^\d{14,19}$"
    let regexMatch = panRegex.Match pan

    match regexMatch.Success with
    | false -> false
    | true ->
        let csInPan = pan |> lastChar
        let calculatedCs = pan |> substring 0 (pan.Length - 1) |> generateChecksum

        csInPan = calculatedCs
