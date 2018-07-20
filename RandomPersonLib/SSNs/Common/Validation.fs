module CommonValidation

open System
open System.Globalization

let (|HasCorrectLength|_|) (ssnLength: int) (potentialSSN: string) (_: string) =
    match potentialSSN.Length with
    | ssnLength -> Some(potentialSSN)
    | _         -> None

let (|HasDate|_|) (dateStart: int) (dateLength: int) (individualNumberStart: int) (datePattern: string) (potentialSSN: string) (_: string) =
    let datePart = potentialSSN.Substring(dateStart, dateLength)
    let isDate, _ = DateTime.TryParseExact(datePart, datePattern, CultureInfo.InvariantCulture, DateTimeStyles.None)

    match isDate with
    | true  -> Some(potentialSSN.Substring(individualNumberStart, potentialSSN.Length - individualNumberStart))
    | false -> None

let (|HasIndividualNumber|_|) (individualNumberLength: int) (rest: string) (_: string) =
    let individualNumberPart = rest.Substring(0, individualNumberLength)
    let isInt, _ = Int32.TryParse(individualNumberPart)

    match isInt with
    | true  -> Some(rest.Substring(individualNumberLength, rest.Length - individualNumberLength))
    | false -> None