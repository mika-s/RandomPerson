module internal ValidatePAN

open System
open System.Text.RegularExpressions
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

let toOutputResult (result: PANValidationResult<string>) =
    match result with
    | Success _ -> (true, String.Empty)
    | Failure f ->
        match f with
        | WrongShape    -> (false, "The shape of the PAN is wrong.")
        | WrongChecksum -> (false, "The checksum is wrong.")

let validatePAN =
    cleanRawPan
    >> hasCorrectShape
    >> bind hasCorrectChecksum
    >> toOutputResult
