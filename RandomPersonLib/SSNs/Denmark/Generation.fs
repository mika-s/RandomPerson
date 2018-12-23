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

let numberFor1937to1999 (random: Random) = 
    let number = random.Next(0, 100)

    match () with
    | () when 0  <= number && number <= 66  -> random.Next(0, 3)
    | () when 67 <= number && number <= 84  -> 4
    | () when 84 <= number && number <= 100 -> 9
    | _ -> invalidOp "Illegal random number."

let numberFor2000to2036 (random: Random) = 
    let number = random.Next(0, 100)

    match () with
    | () when 0  <= number && number <= 49  -> 4
    | () when 50 <= number && number <= 99  -> 9
    | _ -> invalidOp "Illegal random number."

let getCenturyNumber (random: Random) (year: int) =
    match () with
    | () when 1858 <= year && year <= 1899 -> random.Next(5, 8)
    | () when 1900 <= year && year <= 1936 -> random.Next(0, 3)
    | () when 1937 <= year && year <= 1999 -> numberFor1937to1999 random
    | () when 2000 <= year && year <  2036 -> numberFor2000to2036 random
    | () when 2037 <= year && year <  2057 -> random.Next(5, 8)
    | _ -> invalidArg "year" "Illegal year."

let generateDanishIndividualNumber (random: Random) (century: int) =
    sprintf "%d%s" century (random.Next(0, 100).ToString("D2"))
    
let generateChecksumWithModulusControl (birthdate: DateTime) (individualNumber: string) =
    let d1 = Convert.ToInt32(birthdate.Day.ToString("D2").Substring(0, 1))
    let d2 = Convert.ToInt32(birthdate.Day.ToString("D2").Substring(1, 1))
    let m1 = Convert.ToInt32(birthdate.Month.ToString("D2").Substring(0, 1))
    let m2 = Convert.ToInt32(birthdate.Month.ToString("D2").Substring(1, 1))
    let y1 = Convert.ToInt32(birthdate.Year.ToString("D2").Substring(2, 1))
    let y2 = Convert.ToInt32(birthdate.Year.ToString("D4").Substring(3, 1))
    let i1 = Convert.ToInt32(individualNumber.Substring(0, 1))
    let i2 = Convert.ToInt32(individualNumber.Substring(1, 1))
    let i3 = Convert.ToInt32(individualNumber.Substring(2, 1))

    let modulus = (4 * d1 + 3 * d2 + 2 * m1 + 7 * m2 + 6 * y1 + 5 * y2 + 4 * i1 + 3 * i2 + 2 * i3) % 11

    let cs = match modulus with
             | 0 -> 0
             | _ -> 11 - modulus

    sprintf "%d" cs

let generateChecksumWithoutModulusControl (random: Random) = random.Next(1, 10) |> sprintf "%d"

let generateDanishChecksum (random: Random) (birthdate: DateTime) (individualNumber: string)  =
    match () with
    | () when 2007 <= birthdate.Year && 10 <= birthdate.Month -> generateChecksumWithoutModulusControl random
    | _                                                       -> generateChecksumWithModulusControl birthdate individualNumber

let isLegalChecksum (gender: Gender) (checksum: int) =
    (gender = Gender.Male   && isOdd  checksum) ||
    (gender = Gender.Female && isEven checksum)

let anonymizeSSN (ssn: string) = ssn |> incrementAtPosition 8

let generateDanishSSN (random: Random) (birthdate: DateTime) (gender: Gender) (isAnonymizingSSN: bool) =
    let day = birthdate.Day.ToString("D2")
    let month = birthdate.Month.ToString("D2")
    let year = birthdate.Year.ToString("D4").Substring(2)
    let date = sprintf "%s%s%s" day month year
    
    let century = getCenturyNumber random birthdate.Year

    let rec loop () =
        let individualNumber = generateDanishIndividualNumber random century
        let checksum = generateDanishChecksum random birthdate individualNumber
    
        if checksum.Length <> 2 && isLegalChecksum gender (Convert.ToInt32(checksum)) then
            let ssn = sprintf "%s-%s%s" date individualNumber checksum
            match isAnonymizingSSN with
            | true  -> anonymizeSSN ssn 
            | false -> ssn
        else
            loop ()

    loop ()
