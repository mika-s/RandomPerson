module internal FinlandSSNValidation

open System
open System.Globalization
open System.Text.RegularExpressions
open CommonValidation
open FinlandSSNGeneration
open FinlandSSNParameters
open Util
open StringUtil
open Types.SSNTypes

let hasCorrectShape (ssn: string) =
    let regexMatch = Regex.Match(ssn, "^\d{6}(-|\+|A)\d{3}[\dA-Y]$")

    match regexMatch.Success with
    | true  -> Success ssn
    | false -> Failure WrongShape

let hasCorrectChecksum (ssn: string) =
    let datePart = ssn |> substring DateStart DateLength
    let datePattern = "ddMMyy"

    let _, birthDate = DateTime.TryParseExact(datePart, datePattern, CultureInfo.InvariantCulture, DateTimeStyles.None)
    let individualNumber = ssn |> substring IndividualNumberStart IndividualNumberLength

    let cs = generateChecksum birthDate individualNumber
    let csFromSSN = ssn |> substring ChecksumStart ChecksumLength

    match csFromSSN with
    | Equals cs -> Success ssn
    | _         -> Failure WrongChecksum

let toString (result: SSNValidationResult<string>) =
    match result with
    | Success _ -> true
    | Failure _ -> false

let validateSSNForFinland = 
    let hasDateForFinland = hasDate DateStart DateLength
    let hasIndividualNumberForFinland = hasIndividualNumber IndividualNumberStart IndividualNumberLength

    hasCorrectShape
    >> bind hasDateForFinland
    >> bind hasIndividualNumberForFinland
    >> bind hasCorrectChecksum
    >> toString
