module internal NetherlandsSSNValidation

open System.Text.RegularExpressions
open CommonValidation
open NetherlandsSSNGeneration
open NetherlandsSSNParameters
open Util
open StringUtil
open Types.SSNTypes

let hasCorrectShape (ssn: string) = 
    let regexMatch = Regex.Match(ssn, "^\d{9}$")

    match regexMatch.Success with
    | true  -> Success ssn
    | false -> Failure WrongShape

let hasCorrectChecksum (ssn: string) =
    let individualNumber = ssn |> substring IndividualNumberStart IndividualNumberLength

    let cs = generateChecksum individualNumber
    let csFromSSN = ssn |> substring ChecksumStart ChecksumLength

    match csFromSSN with
    | Equals cs -> Success ssn
    | _         -> Failure WrongChecksum

let toString (result: SSNValidationResult<string>) =
    match result with
    | Success _ -> true
    | Failure _ -> false

let validateSSNForNetherlands =
    let hasIndividualNumberForNetherlands = hasIndividualNumber IndividualNumberStart IndividualNumberLength

    hasCorrectShape
    >> bind hasIndividualNumberForNetherlands
    >> bind hasCorrectChecksum
    >> toString
