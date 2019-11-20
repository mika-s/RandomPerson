module internal SwedenSSNGeneration

(*
    Old:

    YYMMDD-XXXC
    | | | | | |-> Checksum
    | | | | |---> Individual number
    | | | |
    | | | |-----> Hyphen
    | | |-------> Day
    | |---------> Month
    |-----------> Year (all digits)

    New:

    YYYYMMDD-XXXC
    |   | | | | |-> Checksum
    |   | | | |---> Individual number
    |   | | |
    |   | | |-----> Hyphen
    |   | |-------> Day
    |   |---------> Month
    |-------------> Year (all digits)

    The checksum has to be even for women and odd for men.
*)

open System
open RandomPersonLib
open ChecksumAlgorithms
open Util
open MathUtil
open StringUtil

let getIndividualNumber (random: Random) = random.Next(000, 999)

let getIndividualNumberMale (random: Random) =
    let rec loop () = 
        let individualNumber = getIndividualNumber random
        if isOdd individualNumber then 
           individualNumber
        else
            loop ()

    loop ()

let getIndividualNumberFemale (random: Random) =
    let rec loop () = 
        let individualNumber = getIndividualNumber random
        if isEven individualNumber then 
           individualNumber
        else
            loop ()

    loop ()

let generateIndividualNumber (random: Random) (gender: Gender) =
    match gender with
    | Gender.Male   -> (getIndividualNumberMale   random).ToString("D3")
    | Gender.Female -> (getIndividualNumberFemale random).ToString("D3")
    | _ -> invalidArg "gender" "Illegal gender."

let generateChecksum (numbers: string) =
    let weights = [| 2; 1; 2; 1; 2; 1; 2; 1; 2 |]
    luhn numbers weights

let anonymizeSSN (ssn: string) = ssn |> incrementAtPosition 8

let generateSwedishSSN (random: Random) (birthdate: DateTime) (gender: Gender) (isAnonymizingSSN: bool) =
    let day   = birthdate.Day  .ToString("D2")
    let month = birthdate.Month.ToString("D2")
    let year  = birthdate.Year .ToString("D4") |> substringToEnd 2
    let date  = sprintf "%s%s%s" year month day
    
    let individualNumber = generateIndividualNumber random gender
    let numbers = sprintf "%s%s" date individualNumber
    let checksum = generateChecksum numbers

    let ssn = sprintf "%s-%s%s" date individualNumber checksum
    match isAnonymizingSSN with
    | true  -> anonymizeSSN ssn 
    | false -> ssn
