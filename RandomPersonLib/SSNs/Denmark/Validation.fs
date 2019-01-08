module internal DenmarkSSNValidation

open System
open System.Globalization
open System.Text.RegularExpressions
open DenmarkSSNGeneration
open DenmarkSSNParameters
open Util
open StringUtil
open Types.SSNTypes
open CommonValidation

let hasCorrectShape (ssn: string) =
    let regexMatch = Regex.Match(ssn, "^\d{6}-\d{4}$")

    match regexMatch.Success with
    | true  -> Success ssn
    | false -> Failure WrongShape

let hasCorrectChecksum (ssn: string) =
    let random = getRandom false 100

    let datePart = ssn |> substring DateStart DateLength
    let datePattern = "ddMMyy"
    let _, birthDate = DateTime.TryParseExact(datePart, datePattern, CultureInfo.InvariantCulture, DateTimeStyles.None)
    let individualNumber = ssn |> substring IndividualNumberStart IndividualNumberLength

    let cs = generateChecksum random birthDate individualNumber
    let csFromSSN = ssn |> substring ChecksumStart ChecksumLength

    match csFromSSN with
    | Equals cs -> Success ssn
    | _         -> Failure WrongChecksum

let toString (result: SSNValidationResult<string>) =
    match result with
    | Success _ -> true
    | Failure _ -> false

let validateSSNForDenmark =
    let hasDateForDenmark = hasDate DateStart DateLength
    let hasIndividualNumberForDenmark = hasIndividualNumber IndividualNumberStart IndividualNumberLength

    hasCorrectShape
    >> bind hasDateForDenmark
    >> bind hasIndividualNumberForDenmark
    >> bind hasCorrectChecksum
    >> toString
