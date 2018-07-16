module IcelandicSSNValidation

open System
open System.Globalization
open System.Text.RegularExpressions
open IcelandicSSNGeneration
open IcelandicSSNParameters
open Util

let (|IcelandicSSN|_|) (potentialSSN: string) =
    let regexMatch = Regex.Match(potentialSSN, "^\d{6}-\d{4}$")

    match regexMatch.Success with
    | true  -> Some(potentialSSN)
    | false -> None

let (|HasCorrectLength|_|) (ssn: string) (_: string) =
    match ssn.Length with
    | SsnLength -> Some(ssn)
    | _         -> None

let (|HasDate|_|) (ssn: string) (_: string) =
    let datePart = ssn.Substring(DateStart, DateLength)
    let isDate, _ = DateTime.TryParseExact(datePart, "ddMMyy", CultureInfo.InvariantCulture, DateTimeStyles.None)

    match isDate with
    | true  -> Some(ssn.Substring(IndividualNumberStart, ssn.Length - IndividualNumberStart))
    | false -> None

let (|HasIndividualNumber|_|) (rest: string) (_: string) =
    let individualNumberPart = rest.Substring(0, IndividualNumberLength)
    let isInt, _ = Int32.TryParse(individualNumberPart)

    match isInt with
    | true  -> Some(rest.Substring(IndividualNumberLength, rest.Length - IndividualNumberLength))
    | false -> None

let (|HasCorrectChecksum|_|) (ssn: string) (rest: string) (_: string) =
    let csFromSSN = ssn.Substring(ChecksumStart, ChecksumLength)
    let birthDateString = ssn.Substring(DateStart, DateLength)
    let _, birthDate = DateTime.TryParseExact(birthDateString, "ddMMyy", CultureInfo.InvariantCulture, DateTimeStyles.None)
    let individualNumber = ssn.Substring(IndividualNumberStart, IndividualNumberLength)

    let cs = generateIcelandicChecksum birthDate individualNumber

    match csFromSSN with
    | Equals cs -> Some(rest.[1].ToString())
    | _         -> None

let (|HasProperCenturyNumber|_|) (centuryNumber: string) =
    match centuryNumber with
    | "8" | "9" | "0" -> Some(centuryNumber)
    | _               -> None

let validateIcelandicSSN (ssn: string) = 
    match ssn with
    | HasCorrectLength ssn potentialSSN ->
        match potentialSSN with
        | HasDate potentialSSN rest ->
            match rest with
            | HasIndividualNumber rest newRest -> 
                match newRest with
                | HasCorrectChecksum ssn newRest newNewRest ->
                    match newNewRest with
                    | HasProperCenturyNumber _ -> true
                    | _ -> false
                | _ -> false
            | _ -> false
        | _ -> false
    | _  -> false
