module IcelandSSNValidation

open System
open System.Globalization
open System.Text.RegularExpressions
open CommonValidation
open IcelandSSNGeneration
open IcelandSSNParameters
open Util

let (|IcelandicSSN|_|) (potentialSSN: string) =
    let regexMatch = Regex.Match(potentialSSN, "^\d{6}-\d{4}$")

    match regexMatch.Success with
    | true  -> Some(potentialSSN)
    | false -> None

let (|HasCorrectChecksum|_|) (ssn: string) (rest: string) (_: string) =
    let csFromSSN = ssn.Substring(ChecksumStart, ChecksumLength)
    let birthDateString = ssn.Substring(DateStart, DateLength)
    let _, birthDate = DateTime.TryParseExact(birthDateString, "ddMMyy", CultureInfo.InvariantCulture, DateTimeStyles.None)
    let individualNumber = ssn.Substring(IndividualNumberStart, IndividualNumberLength)

    let cs = generateChecksum birthDate individualNumber

    match csFromSSN with
    | Equals cs -> Some(rest.[1].ToString())
    | _         -> None

let (|HasProperCenturyNumber|_|) (centuryNumber: string) =
    match centuryNumber with
    | "8" | "9" | "0" -> Some(centuryNumber)
    | _               -> None

let validateIcelandicSSN (ssn: string) = 
    match ssn with
    | HasCorrectLength SsnLength ssn potentialSSN ->
        match potentialSSN with
        | HasDate DateStart DateLength IndividualNumberStart "ddMMyy" potentialSSN rest ->
            match rest with
            | HasIndividualNumber IndividualNumberLength rest newRest -> 
                match newRest with
                | HasCorrectChecksum ssn newRest newNewRest ->
                    match newNewRest with
                    | HasProperCenturyNumber _ -> true
                    | _ -> false
                | _ -> false
            | _ -> false
        | _ -> false
    | _  -> false
