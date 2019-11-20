module internal DenmarkSSNGeneration

(*
    DDMMYY-CXXY
    | | | ||| |-> Checksum
    | | | |||---> Individual number
    | | | ||----> Century number
    | | | |-----> Hyphen
    | | |-------> Year (last two digits)
    | |---------> Month
    |-----------> Day

    Generation of checksum comes in two different flavors:
    - 1968 definition
    - 2001 definition. This came in effect on October 2007.

    The checksum has to be even for women and odd for men.
*)

open System
open RandomPersonLib
open Util
open MathUtil
open StringUtil

let numberFor1937to1999 (random: Random) = 
    let chance = random.Next(0, 100)

    match chance with
    | Between 0  66  -> random.Next(0, 3)
    | Between 67 84  -> 4
    | Between 85 100 -> 9
    | _ -> invalidOp "Illegal random number."

let numberFor2000to2036 (random: Random) = 
    let chance = random.Next(0, 100)

    match chance with
    | Between 0  49 -> 4
    | Between 50 99 -> 9
    | _ -> invalidOp "Illegal random number."

let getCenturyNumber (random: Random) (year: int) =
    match year with
    | Between 1858 1899 -> random.Next(5, 8)
    | Between 1900 1936 -> random.Next(0, 3)
    | Between 1937 1999 -> numberFor1937to1999 random
    | Between 2000 2036 -> numberFor2000to2036 random
    | Between 2037 2057 -> random.Next(5, 8)
    | _ -> invalidArg "year" "Illegal year."

let generateIndividualNumber (random: Random) (century: int) =
    sprintf "%d%s" century (random.Next(0, 100).ToString("D2"))
    
let generateChecksumWithModulusControl (birthdate: DateTime) (individualNumber: string) =
    let d1 = birthdate.Day  .ToString("D2") |> substring 0 1 |> int
    let d2 = birthdate.Day  .ToString("D2") |> substring 1 1 |> int
    let m1 = birthdate.Month.ToString("D2") |> substring 0 1 |> int
    let m2 = birthdate.Month.ToString("D2") |> substring 1 1 |> int
    let y1 = birthdate.Year .ToString("D2") |> substring 2 1 |> int
    let y2 = birthdate.Year .ToString("D4") |> substring 3 1 |> int
    let i1 = individualNumber               |> substring 0 1 |> int
    let i2 = individualNumber               |> substring 1 1 |> int
    let i3 = individualNumber               |> substring 2 1 |> int

    let modulus = (4 * d1 + 3 * d2 + 2 * m1 + 7 * m2 + 6 * y1 + 5 * y2 + 4 * i1 + 3 * i2 + 2 * i3) % 11

    let cs = match modulus with
             | 0 -> 0
             | _ -> 11 - modulus

    sprintf "%d" cs

let generateChecksumWithoutModulusControl (random: Random) = random.Next(1, 10) |> sprintf "%d"

let generateChecksum (random: Random) (birthdate: DateTime) (individualNumber: string)  =
    match () with
    | () when 2007 <= birthdate.Year && 10 <= birthdate.Month -> generateChecksumWithoutModulusControl random
    | _                                                       -> generateChecksumWithModulusControl birthdate individualNumber

let isLegalChecksum (gender: Gender) (checksum: int) =
    (gender = Gender.Male   && isOdd  checksum) ||
    (gender = Gender.Female && isEven checksum)

let anonymizeSSN (ssn: string) = ssn |> incrementAtPosition 8

let generateDanishSSN (random: Random) (birthdate: DateTime) (gender: Gender) (isAnonymizingSSN: bool) =
    let day   = birthdate.Day  .ToString("D2")
    let month = birthdate.Month.ToString("D2")
    let year  = birthdate.Year .ToString("D4") |> substringToEnd 2
    let date = sprintf "%s%s%s" day month year
    
    let century = getCenturyNumber random birthdate.Year

    let rec loop () =
        let individualNumber = generateIndividualNumber random century
        let checksum = generateChecksum random birthdate individualNumber
    
        if checksum.Length <> 2 && isLegalChecksum gender (int checksum) then
            let ssn = sprintf "%s-%s%s" date individualNumber checksum
            match isAnonymizingSSN with
            | true  -> anonymizeSSN ssn 
            | false -> ssn
        else
            loop ()

    loop ()
