module internal FinnishSSNValidation

open System
open System.Globalization
open System.Text.RegularExpressions
open FinnishSSNGeneration
open FinnishSSNParameters
open Util

let (|FinnishSSN|_|) (potentialSSN: string) =
    let regexMatch = Regex.Match(potentialSSN, "^\d{6}(-|\+|A)\d{3}[\dA-Y]$")

    match regexMatch.Success with
    | true  -> Some(potentialSSN)
    | false -> None

let (|HasCorrectLength|_|) (ssn: string) (_: string) =
    match ssn.Length with
    | SsnLength -> Some(ssn)
    | _         -> None

let (|HasDate|_|) (_: string) (s: string) =
    let datePart = s.Substring(DateStart, DateLength)
    let isDate, _ = DateTime.TryParseExact(datePart, "ddMMyy", CultureInfo.InvariantCulture, DateTimeStyles.None)

    match isDate with
    | true  -> Some(s.Substring(IndividualNumberStart, s.Length - IndividualNumberStart))
    | false -> None

let (|HasIndividualNumber|_|) (_: string) (s: string) =
    let individualNumberPart = s.Substring(0, IndividualNumberLength)
    let isInt, _ = Int32.TryParse(individualNumberPart)

    match isInt with
    | true  -> Some(s.Substring(IndividualNumberLength, s.Length - IndividualNumberLength))
    | false -> None

let isCorrectChecksum (csFromSSN: string) (ssn: string) =
    let birthDateString = ssn.Substring(DateStart, DateLength)
    let _, birthDate = DateTime.TryParseExact(birthDateString, "ddMMyy", CultureInfo.InvariantCulture, DateTimeStyles.None)
    let individualNumber = ssn.Substring(IndividualNumberStart, IndividualNumberLength)

    let cs = generateFinnishChecksum birthDate individualNumber

    match csFromSSN with
    | Equals cs -> true
    | _         -> false

let validateFinnishSSN (ssn: string) = 
    match ssn with
    | HasCorrectLength ssn potentialSSN ->
        match potentialSSN with
        | HasDate ssn rest ->
            match rest with
            | HasIndividualNumber rest newRest -> isCorrectChecksum newRest ssn
            | _ -> false
        | _ -> false
    | _  -> false
