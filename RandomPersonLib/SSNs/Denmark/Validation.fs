module internal DenmarkSSNValidation

open System
open System.Globalization
open System.Text.RegularExpressions
open CommonValidation
open DenmarkSSNGeneration
open DenmarkSSNParameters
open Util

let (|SSNForDenmark|_|) (potentialSSN: string) =
    let regexMatch = Regex.Match(potentialSSN, "^\d{6}-\d{4}$")

    match regexMatch.Success with
    | true  -> Some(potentialSSN)
    | false -> None

let (|HasCorrectChecksum|_|) (random: Random) (csFromSSN: string) (ssn: string) (_: string) =
    let birthDateString = ssn.Substring(DateStart, DateLength)
    let _, birthDate = DateTime.TryParseExact(birthDateString, "ddMMyy", CultureInfo.InvariantCulture, DateTimeStyles.None)
    let individualNumber = ssn.Substring(IndividualNumberStart, IndividualNumberLength)

    let cs = generateChecksum random birthDate individualNumber

    match csFromSSN with
    | Equals cs -> Some(cs)
    | _         -> None

let validateSSNForDenmark (ssn: string) =
    let random = getRandom false 100

    match ssn with
    | HasCorrectLength SsnLength ssn potentialSSN ->
        match potentialSSN with
        | HasDate DateStart DateLength IndividualNumberStart "ddMMyy"  ssn rest ->
            match rest with
            | HasIndividualNumber IndividualNumberLength rest newRest -> 
                match newRest with
                | HasCorrectChecksum random newRest ssn _ -> true
                | _ -> false 
            | _ -> false
        | _ -> false
    | _  -> false
