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
open StringUtil

let generateIndividualNumber (random: Random) = random.Next(20, 100) |> sprintf "%d"
    
let generateChecksum (birthdate: DateTime) (individualNumber: string) =
    let d1 = birthdate.Day   .ToString("D2") |> substring 0 1 |> int
    let d2 = birthdate.Day   .ToString("D2") |> substring 1 1 |> int
    let m1 = birthdate.Month .ToString("D2") |> substring 0 1 |> int
    let m2 = birthdate.Month .ToString("D2") |> substring 1 1 |> int
    let y1 = birthdate.Year  .ToString("D2") |> substring 2 1 |> int
    let y2 = birthdate.Year  .ToString("D4") |> substring 3 1 |> int
    let i1 = individualNumber                |> substring 0 1 |> int
    let i2 = individualNumber                |> substring 1 1 |> int

    let cs = 11 - ((3 * d1 + 2 * d2 + 7 * m1 + 6 * m2 + 5 * y1 + 4 * y2 + 3 * i1 + 2 * i2) % 11)

    match cs with
    | 11 -> sprintf "%d" 0
    | _  -> sprintf "%d" cs

let getCenturyNumber (year: int) =
    match year with
    | Between 1800 1899 -> "8"
    | Between 1900 1999 -> "9"
    | Between 2000 2099 -> "0"
    | _ -> invalidArg "year" "Illegal year."    

let anonymizeSSN (ssn: string) = ssn |> incrementAtPosition 8

let generateIcelandicSSN (random: Random) (birthdate: DateTime) (isAnonymizingSSN: bool) =
    let rec loop () = 
        let day   = birthdate.Day  .ToString("D2")
        let month = birthdate.Month.ToString("D2")
        let year  = birthdate.Year .ToString("D4") |> substringToEnd 2
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
