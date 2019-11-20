module internal FinlandSSNGeneration

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
open FinlandSSNParameters
open Util
open MathUtil
open StringUtil

let generateCenturySign (year: int) =
    match year with
    | Between 1800 1899 () -> "+"
    | Between 1900 1999 () -> "-"
    | Between 2000 2099 () -> "A"
    | _ -> invalidArg "year" "Illegal year."

let getIndividualNumber (random: Random) = random.Next(002, 899)

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

let generateChecksum (birthdate: DateTime) (individualNumber: string) =
    let d = birthdate.Day  .ToString("D2")
    let m = birthdate.Month.ToString("D2")
    let y = birthdate.Year .ToString("D2") |> substring 2 2
    let ssnWithoutChecksum = sprintf "%s%s%s%s" d m y individualNumber |> int64

    let forAbove10 = "0123456789ABCDEFHJKLMNPRSTUVWXY" |> toCharArray

    let mod31 = (ssnWithoutChecksum % int64 31) |> int

    match mod31 with
    | x when x < 10 -> sprintf "%d" mod31
    | _             -> forAbove10.[mod31].ToString()

let anonymizeSSN (ssn: string) =
    let rec loop (initialSSN: string) =
        let incrementedSSN = initialSSN |> incrementAtPosition 8
        let individualNumber = incrementedSSN |> substring IndividualNumberStart IndividualNumberLength

        match individualNumber with
        | "000" | "001" -> loop incrementedSSN
        | _             -> incrementedSSN

    loop ssn

let generateFinnishSSN (random: Random) (birthdate: DateTime) (gender: Gender) (isAnonymizingSSN: bool) =
    let rec loop () = 
        let day   = birthdate.Day  .ToString("D2")
        let month = birthdate.Month.ToString("D2")
        let year  = birthdate.Year .ToString("D4") |> substringToEnd 2
        let date = sprintf "%s%s%s" day month year

        let individualNumber = generateIndividualNumber random gender
        let centurySign = generateCenturySign birthdate.Year
        let checksum = generateChecksum birthdate individualNumber

        if checksum.Length = ChecksumLength then 
            let ssn = sprintf "%s%s%s%s" date centurySign individualNumber checksum
            match isAnonymizingSSN with
            | true  -> anonymizeSSN ssn
            | false -> ssn
        else
            loop ()

    loop ()
