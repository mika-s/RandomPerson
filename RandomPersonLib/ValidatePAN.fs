module internal ValidatePAN

open System.Text.RegularExpressions
open Util
open StringUtil
open Types.PANTypes
open Creditcard

let bind switchFunction twoTrackInput =
    match twoTrackInput with
    | Success s -> switchFunction s
    | Failure f -> Failure f

let cleanRawPan = trim >> removeChar "-" >> removeChar " "

let hasCorrectShape (pan: string) =
    let panRegex = Regex "^\d{14,19}$"
    let regexMatch = panRegex.Match pan

    match regexMatch.Success with
    | true  -> Success pan
    | false -> Failure WrongShape

let hasCorrectChecksum (pan: string) =
    let csInPan = pan |> lastChar
    let calculatedCs = pan |> substring 0 (pan.Length - 1) |> generateChecksum
    
    match csInPan = calculatedCs with
    | true  -> Success pan
    | false -> Failure WrongChecksum

let toBool (result: PANValidationResult<string>) =
    match result with
    | Success _ -> true
    | Failure _ -> false

let validatePAN =
    cleanRawPan
    >> hasCorrectShape
    >> bind hasCorrectChecksum
    >> toBool
