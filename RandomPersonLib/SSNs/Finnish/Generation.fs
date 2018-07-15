module internal FinnishSSNGeneration

(*
    DDMMYYCZZZQ
    | | | | | |--> Checksum
    | | | | |
    | | | | |---> Individual number
    | | | |-----> Century
    | | |-------> Year (last two digits)
    | |---------> Month
    |-----------> Day

    Century sign:

    + (1800–1899), - (1900–1999), or A (2000–2099)

    The individual number has to be even for women and odd for men.
*)

open System
open RandomPersonLib
open FinnishSSNParameters
open Util

let generateCenturySign (year: int) =
    match () with
    | () when 1800 <= year && year <= 1899 -> "+"
    | () when 1900 <= year && year <= 1999 -> "-"
    | () when 2000 <= year && year <= 2099 -> "A"
    | _ -> invalidArg "year" "Illegal year."

let getIndividualNumber (random: Random) =
    let min = 002
    let max = 899

    random.Next(min, max)

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

let generateFinnishIndividualNumber (random: Random) (gender: Gender) =
    match gender with
    | Gender.Male   -> (getIndividualNumberMale   random).ToString("D3")
    | Gender.Female -> (getIndividualNumberFemale random).ToString("D3")
    | _ -> invalidArg "gender" "Illegal gender."

let generateFinnishChecksum (birthdate: DateTime) (individualNumber: string) =
    let d = birthdate.Day.ToString("D2")
    let m = birthdate.Month.ToString("D2")
    let y = birthdate.Year.ToString("D2").Substring(2, 2)
    let asString = sprintf "%s%s%s%s" d m y individualNumber
    let asInt = Convert.ToInt64(asString)

    let forAbove10 = "0123456789ABCDEFHJKLMNPRSTUVWXY".ToCharArray()

    let mod31 = Convert.ToInt32(asInt % Convert.ToInt64(31))

    match mod31 with
    | x when x < 10 -> sprintf "%d" mod31
    | _             -> forAbove10.[mod31].ToString()

let anonymizeSSN (ssn: string) = incrementNumberInString ssn 8

let generateFinnishSSN (random: Random) (birthdate: DateTime) (gender: Gender) (isAnonymizingSSN: bool) =
    let rec loop () = 
        let day   = birthdate.Day  .ToString("D2")
        let month = birthdate.Month.ToString("D2")
        let year  = birthdate.Year .ToString("D4").Substring(2)
        let date = sprintf "%s%s%s" day month year

        let individualNumber = generateFinnishIndividualNumber random gender
        let centurySign = generateCenturySign birthdate.Year
        let checksum = generateFinnishChecksum birthdate individualNumber

        if checksum.Length = ChecksumLength then 
            let ssn = sprintf "%s%s%s%s" date centurySign individualNumber checksum
            match isAnonymizingSSN with
            | true  -> anonymizeSSN ssn
            | false -> ssn
        else
            loop ()

    loop ()
