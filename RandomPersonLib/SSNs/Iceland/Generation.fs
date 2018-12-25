module IcelandSSNGeneration

(*
    DDMMYY-XXCY
    | | |  | ||
    | | |  | ||--> Century sign
    | | |  | |---> Checksum
    | | |  |-----> Individual number
    | | |-------> Year (last two digits)
    | |---------> Month
    |-----------> Day
*)

open System
open IcelandSSNParameters
open Util

let generateIndividualNumber (random: Random) = random.Next(20, 100) |> sprintf "%d"
    
let generateChecksum (birthdate: DateTime) (individualNumber: string) =
    let d1 = int (birthdate.Day   .ToString("D2").Substring(0, 1))
    let d2 = int (birthdate.Day   .ToString("D2").Substring(1, 1))
    let m1 = int (birthdate.Month .ToString("D2").Substring(0, 1))
    let m2 = int (birthdate.Month .ToString("D2").Substring(1, 1))
    let y1 = int (birthdate.Year  .ToString("D2").Substring(2, 1))
    let y2 = int (birthdate.Year  .ToString("D4").Substring(3, 1))
    let i1 = int (individualNumber               .Substring(0, 1))
    let i2 = int (individualNumber               .Substring(1, 1))

    let cs = 11 - ((3 * d1 + 2 * d2 + 7 * m1 + 6 * m2 + 5 * y1 + 4 * y2 + 3 * i1 + 2 * i2) % 11)

    match cs with
    | 11 -> sprintf "%d" 0
    | _  -> sprintf "%d" cs

let getCenturyNumber (year: int) =
    match () with
    | () when 1800 <= year && year <= 1899 -> "8"
    | () when 1900 <= year && year <= 1999 -> "9"
    | () when 2000 <= year && year <= 2099 -> "0"
    | _ -> invalidArg "year" "Illegal year."    

let anonymizeSSN (ssn: string) = ssn |> incrementAtPosition 8

let generateIcelandicSSN (random: Random) (birthdate: DateTime) (isAnonymizingSSN: bool) =
    let rec loop () = 
        let day   = birthdate.Day  .ToString("D2")
        let month = birthdate.Month.ToString("D2")
        let year  = birthdate.Year .ToString("D4").Substring(2)
        let date = sprintf "%s%s%s" day month year

        let individualNumber = generateIndividualNumber random
        let checksum = generateChecksum birthdate individualNumber
        let centuryNumber = getCenturyNumber birthdate.Year

        if checksum.Length = ChecksumLength then 
            let ssn = sprintf "%s-%s%s%s" date individualNumber checksum centuryNumber
            match isAnonymizingSSN with
            | true  -> anonymizeSSN ssn 
            | false -> ssn
        else
            loop ()

    loop ()
