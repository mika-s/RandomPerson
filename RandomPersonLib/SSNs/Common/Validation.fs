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

let hasCorrectChecksum (getCalculatedCs: string -> string) (checksumStart: int) (checksumLength: int) (ssn: string) =
    let cs = getCalculatedCs ssn
    let csFromSSN = ssn |> substring checksumStart checksumLength

    match csFromSSN with
    | Equals cs -> Success ssn
    | _         -> Failure WrongChecksum

let toOutputResult (result: SSNValidationResult<string>) =
    match result with
    | Success _ -> (true, String.Empty)
    | Failure f ->
        match f with
        | WrongShape            -> (false, "The shape is wrong.")
        | WrongDate             -> (false, "The date is wrong.")
        | WrongIndividualNumber -> (false, "The individual number is wrong.")
        | WrongChecksum         -> (false, "The checksum is wrong.")
        | WrongCenturyNumber    -> (false, "The century number is wrong.")
        | WrongAreaNumber       -> (false, "The area number is wrong.")
        | WrongGroupNumber      -> (false, "The group number is wrong.")
        | WrongSerialNumber     -> (false, "The serial number is wrong.")
