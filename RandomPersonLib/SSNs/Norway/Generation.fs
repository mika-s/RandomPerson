module internal NorwaySSNGeneration

(*
    DDMMYYXXXCC
    | | | |  |--> Checksum
    | | | |
    | | | |
    | | | |-----> Individual number
    | | |-------> Year (last two digits)
    | |---------> Month
    |-----------> Day

    The checksum has to be even for women and odd for men.
*)

open System
open RandomPersonLib
open NorwaySSNParameters
open Util

let getIndividualNumber (random: Random) (year: int) =
    match () with
    | () when 1854 <= year && year <= 1899 -> random.Next(500, 749)
    | () when 1900 <= year && year <= 1999 -> random.Next(000, 499)
    | () when 2000 <= year && year <= 2039 -> random.Next(500, 999)
    | _ -> invalidArg "year" "Illegal year."

let getIndividualNumberMale (random: Random) (year: int) =
    let rec loop () = 
        let individualNumber = getIndividualNumber random year
        if isOdd individualNumber then 
           individualNumber
        else
            loop ()

    loop ()

let getIndividualNumberFemale (random: Random) (year: int) =
    let rec loop () = 
        let individualNumber = getIndividualNumber random year
        if isEven individualNumber then 
           individualNumber
        else
            loop ()

    loop ()

let generateIndividualNumber (random: Random) (year: int) (gender: Gender) =
    match gender with
    | Gender.Male   -> (getIndividualNumberMale   random year).ToString("D3")
    | Gender.Female -> (getIndividualNumberFemale random year).ToString("D3")
    | _ -> invalidArg "gender" "Illegal gender."
    
let generateChecksum (birthdate: DateTime) (individualNumber: string) =
    let d1 = int (birthdate.Day   .ToString("D2").Substring(0, 1))
    let d2 = int (birthdate.Day   .ToString("D2").Substring(1, 1))
    let m1 = int (birthdate.Month .ToString("D2").Substring(0, 1))
    let m2 = int (birthdate.Month .ToString("D2").Substring(1, 1))
    let y1 = int (birthdate.Year  .ToString("D2").Substring(2, 1))
    let y2 = int (birthdate.Year  .ToString("D4").Substring(3, 1))
    let i1 = int (individualNumber               .Substring(0, 1))
    let i2 = int (individualNumber               .Substring(1, 1))
    let i3 = int (individualNumber               .Substring(2, 1))

    let cs1 = 11 - ((3 * d1 + 7 * d2 + 6 * m1 + 1 * m2 + 8 * y1 + 9 * y2 + 4 * i1 + 5 * i2 + 2 * i3) % 11)
    let cs2 = 11 - ((5 * d1 + 4 * d2 + 3 * m1 + 2 * m2 + 7 * y1 + 6 * y2 + 5 * i1 + 4 * i2 + 3 * i3 + 2 * cs1) % 11)

    match cs1, cs2 with
    | (11, 11) -> sprintf "%d%d"  0   0
    | (11,  _) -> sprintf "%d%d"  0  cs2
    | ( _, 11) -> sprintf "%d%d" cs1  0
    | ( _,  _) -> sprintf "%d%d" cs1 cs2

let anonymizeSSN (ssn: string) = ssn |> incrementAtPosition 7

let generateNorwegianSSN (random: Random) (birthdate: DateTime) (gender: Gender) (isAnonymizingSSN: bool) =
    let rec loop () = 
        let day   = birthdate.Day  .ToString("D2")
        let month = birthdate.Month.ToString("D2")
        let year  = birthdate.Year .ToString("D4").Substring(2)
        let date = sprintf "%s%s%s" day month year

        let individualNumber = generateIndividualNumber random birthdate.Year gender
        let checksum = generateChecksum birthdate individualNumber

        if checksum.Length = ChecksumLength then 
            let ssn = sprintf "%s%s%s" date individualNumber checksum
            match isAnonymizingSSN with
            | true  -> anonymizeSSN ssn 
            | false -> ssn
        else
            loop ()

    loop ()
