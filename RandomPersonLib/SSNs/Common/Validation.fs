module internal CommonValidation

open System
open System.Globalization
open System.Text.RegularExpressions
open StringUtil
open Types.SSNTypes

let hasCorrectShape (shape: string) (ssn: string) =
    let regexMatch = Regex.Match(ssn, shape)

    match regexMatch.Success with
    | true  -> Success ssn
    | false -> Failure WrongShape

let hasDate (datePattern: string) (dateStart: int) (dateLength: int) (ssn: string) =
    let datePart = ssn |> substring dateStart dateLength
    let isDate, _ = DateTime.TryParseExact(datePart, datePattern, CultureInfo.InvariantCulture, DateTimeStyles.None)

    match isDate with
    | true  -> Success ssn
    | false -> Failure WrongDate

let hasIndividualNumber (individualNumberStart: int) (individualNumberLength: int) (ssn: string) =
    let individualNumberPart = ssn |> substring individualNumberStart individualNumberLength
    let isInt, _ = Int32.TryParse(individualNumberPart)

    match isInt with
    | true  -> Success ssn
    | false -> Failure WrongIndividualNumber

let toBool (result: SSNValidationResult<string>) =
    match result with
    | Success _ -> true
    | Failure _ -> false