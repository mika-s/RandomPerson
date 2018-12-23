module internal FinlandSSNValidation

open System
open System.Globalization
open System.Text.RegularExpressions
open CommonValidation
open FinlandSSNGeneration
open FinlandSSNParameters
open Util

let (|FinnishSSN|_|) (potentialSSN: string) =
    let regexMatch = Regex.Match(potentialSSN, "^\d{6}(-|\+|A)\d{3}[\dA-Y]$")

    match regexMatch.Success with
    | true  -> Some(potentialSSN)
    | false -> None

let (|HasCorrectChecksum|_|) (csFromSSN: string) (ssn: string) (_: string) =
    let birthDateString = ssn.Substring(DateStart, DateLength)
    let _, birthDate = DateTime.TryParseExact(birthDateString, "ddMMyy", CultureInfo.InvariantCulture, DateTimeStyles.None)
    let individualNumber = ssn.Substring(IndividualNumberStart, IndividualNumberLength)

    let cs = generateFinnishChecksum birthDate individualNumber

    match csFromSSN with
    | Equals cs -> Some(cs)
    | _         -> None

let validateFinnishSSN (ssn: string) = 
    match ssn with
    | HasCorrectLength SsnLength ssn potentialSSN ->
        match potentialSSN with
        | HasDate DateStart DateLength IndividualNumberStart "ddMMyy" potentialSSN rest ->
            match rest with
            | HasIndividualNumber IndividualNumberLength rest newRest ->
                match newRest with 
                | HasCorrectChecksum newRest ssn _ -> true
                | _ -> false
            | _ -> false
        | _ -> false
    | _  -> false
