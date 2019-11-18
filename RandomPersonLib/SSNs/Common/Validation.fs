module internal CommonValidation

open System
open System.Globalization
open System.Text.RegularExpressions
open Util
open StringUtil
open Types.SSNTypes

let hasCorrectShape (shape: string) (ssn: string) =
    let regexMatch = Regex.Match(ssn, shape)

    match regexMatch.Success with
    | true  -> Success ssn
    | false -> Failure InvalidShape

let hasCorrectYear (yearStart: int) (yearLength: int) (ssn: string) =
    let yearPart = ssn |> substring yearStart yearLength |> int
    let isValidYear = 0 <= yearPart && yearPart <= 99

    match isValidYear with
    | true  -> Success ssn
    | false -> Failure InvalidYear

let hasCorrectMonth (monthStart: int) (monthLength: int) (ssn: string) =
    let monthPart = ssn |> substring monthStart monthLength |> int
    let isValidMonth = 1 <= monthPart && monthPart <= 12

    match isValidMonth with
    | true  -> Success ssn
    | false -> Failure InvalidMonth

let hasCorrectMonthWithExtraWhitelist (monthStart: int) (monthLength: int) (whitelist: int list) (ssn: string) =
    let monthPart = ssn |> substring monthStart monthLength |> int
    let isValidMonth = (1 <= monthPart && monthPart <= 12) || List.exists (fun e -> e = monthPart) whitelist

    match isValidMonth with
    | true  -> Success ssn
    | false -> Failure InvalidMonth

let hasDate (datePattern: string) (dateStart: int) (dateLength: int) (ssn: string) =
    let datePart = ssn |> substring dateStart dateLength
    let isDate, _ = DateTime.TryParseExact(datePart, datePattern, CultureInfo.InvariantCulture, DateTimeStyles.None)

    match isDate with
    | true  -> Success ssn
    | false -> Failure InvalidDate

let hasIndividualNumber (individualNumberStart: int) (individualNumberLength: int) (ssn: string) =
    let individualNumberPart = ssn |> substring individualNumberStart individualNumberLength
    let isInt, _ = Int32.TryParse(individualNumberPart)

    match isInt with
    | true  -> Success ssn
    | false -> Failure InvalidIndividualNumber

let hasCorrectChecksum (getCalculatedCs: string -> string) (checksumStart: int) (checksumLength: int) (ssn: string) =
    let cs = getCalculatedCs ssn
    let csFromSSN = ssn |> substring checksumStart checksumLength

    match csFromSSN with
    | Equals cs -> Success ssn
    | _         -> Failure InvalidChecksum

let toOutputResult (result: SSNValidationResult<string>) =
    match result with
    | Success _ -> (true, String.Empty)
    | Failure f ->
        match f with
        | InvalidShape            -> (false, "The shape is invalid.")
        | InvalidDate             -> (false, "The date is invalid.")
        | InvalidYear             -> (false, "The year is invalid.")
        | InvalidMonth            -> (false, "The month is invalid.")
        | InvalidIndividualNumber -> (false, "The individual number is invalid.")
        | InvalidChecksum         -> (false, "The checksum is invalid.")
        | InvalidCenturyNumber    -> (false, "The century number is invalid.")
        | InvalidAreaNumber       -> (false, "The area number is invalid.")
        | InvalidGroupNumber      -> (false, "The group number is invalid.")
        | InvalidSerialNumber     -> (false, "The serial number is invalid.")
        | InvalidGenderNumber     -> (false, "The gender number is invalid.")
        | InvalidDepartmentNumber -> (false, "The department number is invalid.")
        | InvalidCommuneNumber    -> (false, "The commune number is invalid.")
