module internal FranceSSNGeneration

(*
    syymmlloookkkcc
    || | | |  |  |--> Checksum
    || | | |  |-----> Individual number
    || | | |--------> Commune of origin
    || | |----------> Départment of origin
    || |------------> Month 
    ||--------------> Year (last two digits)
    |---------------> Gender

    Gender is 1 or 7 for male and 2 or 8 for female.
    
    The department is between 01 and 95, but not 20 (which is Corsica).
    2A, 2B, 971, 972, 973, 974, 975, 976 are also allowed. 99 is for
    foreigners.

    The commune is between 001 and 990. If department has 3 digits then
    the commune is between 01 and 90.

    The checksum has to be between 01 and 96.
*)

open System
open RandomPersonLib
open Util
open StringUtil

let generateGenderNumber (random: Random) (gender: Gender) =
    let randomChance = random.Next(0, 100)

    match gender with
    | Gender.Male   ->
        match randomChance with
        | c when 0 <= c && c <= 95 -> "1"
        | _                        -> "7"
    | Gender.Female ->
        match randomChance with
        | c when 0 <= c && c <= 95 -> "2"
        | _                        -> "8"
    | _ -> invalidArg "gender" "Illegal gender"

let generateDepartment (random: Random) =
    let randomChance = random.Next(1, 103)
    let rec loop () =
        match randomChance with
        | c when 0 <= c && c <= 94 ->
            let randomNumber = random.Next(1, 95)
            match randomNumber with
            | 20 -> loop ()
            | _  -> randomNumber.ToString("D2")
        | c when c = 95 -> "2A"
        | c when c = 96 -> "2B"
        | c when c <= 97 && c <= 102 ->
            random.Next(971, 977).ToString("D3")
        | _ -> "99"

    let department = loop ()
    department

let generateCommune (random: Random) =
    let rec loop () =
        let randomNumber = random.Next(1, 95)
        match randomNumber with
        | 20 -> loop ()
        | _  -> randomNumber

    let department = loop ()
    department.ToString("D3")

let generateIndividualNumber (random: Random) =
    sprintf "%s" (random.Next(0, 999).ToString("D3"))
    
let generateChecksum (genderNumber: string) (year: string) (month: string) (department: string) (commune: string) (individualNumber: string) =
    let g = genderNumber |> int
    let y = year |> int
    let m = month |> int
    let d = department |> replace "A" "0" |> replace "B" "0" |> int
    let c = commune |> int
    let i = individualNumber |> int
    
    let first13 = g + y + m + d + c + i
    let cs = 97 - (first13 % 97)

    assert (1 <= cs && cs <= 96)

    sprintf "%d" cs

let anonymizeSSN (ssn: string) = ssn |> incrementAtPosition 11

let generateFrenchSSN (random: Random) (birthdate: DateTime) (gender: Gender) (isAnonymizingSSN: bool) =
    let genderNumber = generateGenderNumber random gender
    let year  = birthdate.Year .ToString("D4") |> substringToEnd 2
    let month = birthdate.Month.ToString("D2")
    let department = generateDepartment random
    let commune = generateCommune random
    let individualNumber = generateIndividualNumber random
    let checksum = generateChecksum genderNumber year month department commune individualNumber

    let ssn = sprintf "%s%s%s%s%s%s%s" genderNumber year month department commune individualNumber checksum
    match isAnonymizingSSN with
    | true  -> anonymizeSSN ssn 
    | false -> ssn

