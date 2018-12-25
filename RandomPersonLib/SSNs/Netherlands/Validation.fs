module internal NetherlandsSSNValidation

open System.Text.RegularExpressions
open CommonValidation
open NetherlandsSSNGeneration
open NetherlandsSSNParameters
open Util

let (|DutchSSN|_|) (potentialSSN: string) =
    let regexMatch = Regex.Match(potentialSSN, "^\d{9}$")

    match regexMatch.Success with
    | true  -> Some(potentialSSN)
    | false -> None

let (|HasCorrectChecksum|_|) (csFromSSN: string) (ssn: string) (_: string) =
    let individualNumber = ssn.Substring(IndividualNumberStart, IndividualNumberLength)

    let cs = generateChecksum individualNumber

    match csFromSSN with
    | Equals cs -> Some(cs)
    | _         -> None

let validateDutchSSN (ssn: string) =
    match ssn with
    | HasCorrectLength SsnLength ssn potentialSSN ->
        match potentialSSN with
        | HasIndividualNumber IndividualNumberLength potentialSSN newRest -> 
            match newRest with
            | HasCorrectChecksum newRest ssn _ -> true
            | _ -> false 
        | _ -> false
    | _  -> false
