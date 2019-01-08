module internal NorwaySSNValidation

open System
open System.Globalization
open System.Text.RegularExpressions
open NorwaySSNGeneration
open NorwaySSNParameters
open Util
open StringUtil
open Types.SSNTypes

let hasCorrectShape (ssn: string) =
    let regexMatch = Regex.Match(ssn, "^\d{11}$")

    match regexMatch.Success with
    | true  -> Success ssn
    | false -> Failure WrongShape

let hasDate (ssn: string) =
    let datePart = ssn |> substring DateStart DateLength
    let datePattern = "ddMMyy"
    let isDate, _ = DateTime.TryParseExact(datePart, datePattern, CultureInfo.InvariantCulture, DateTimeStyles.None)

    match isDate with
    | true  -> Success ssn
    | false -> Failure WrongDate

let hasIndividualNumber (ssn: string) =
    let individualNumberPart = ssn |> substring IndividualNumberStart IndividualNumberLength
    let isInt, _ = Int32.TryParse(individualNumberPart)

    match isInt with
    | true  -> Success ssn
    | false -> Failure WrongIndividualNumber

let hasCorrectChecksum (ssn: string) =
    let datePart = ssn |> substring DateStart DateLength
    let datePattern = "ddMMyy"
    let _, birthDate = DateTime.TryParseExact(datePart, datePattern, CultureInfo.InvariantCulture, DateTimeStyles.None)
    let individualNumberPart = ssn |> substring IndividualNumberStart IndividualNumberLength

    let cs = generateChecksum birthDate individualNumberPart
    let csFromSSN = ssn |> substring ChecksumStart ChecksumLength

    match csFromSSN with
    | Equals cs -> Success ssn
    | _         -> Failure WrongChecksum

let toString (result: SSNValidationResult<string>) =
    match result with
    | Success _ -> true
    | Failure _ -> false
    
let validateSSNForNorway = 
    hasCorrectShape
    >> bind hasDate
    >> bind hasIndividualNumber
    >> bind hasCorrectChecksum
    >> toString
