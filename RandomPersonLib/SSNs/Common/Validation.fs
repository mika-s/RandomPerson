module internal CommonValidation

open System
open System.Globalization
open StringUtil
open Types.SSNTypes

let hasDate (dateStart: int) (dateLength: int) (ssn: string) =
    let datePart = ssn |> substring dateStart dateLength
    let datePattern = "ddMMyy"
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
