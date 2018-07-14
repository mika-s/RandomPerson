module internal SwedishSSNGeneration

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
open Util

let getIndividualNumber (random: Random) =
    random.Next(000, 999)

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

let generateSwedishIndividualNumber (random: Random) (gender: Gender) =
    match gender with
    | Gender.Male   -> (getIndividualNumberMale   random).ToString("D3")
    | Gender.Female -> (getIndividualNumberFemale random).ToString("D3")
    | _ -> failwith "Illegal gender."

let sumTheDigits (product: int) =
    let productAsString = sprintf "%d" product

    intFromChar productAsString.[0] + intFromChar productAsString.[1]

let luhn (w: int) (nAsChar: char) =
    let n = intFromChar nAsChar
    let product = n * w

    match product with
    | p when p >= 10 -> sumTheDigits product
    | _              -> product

let generateSwedishChecksum (numbersStr: string) =
    let weight = [| 2; 1; 2; 1; 2; 1; 2; 1; 2 |];
    let numbers = numbersStr.ToCharArray ()

    let mapped = Array.map2(luhn) weight numbers
    let sum = mapped |> Array.sum
    let sumStr = sprintf "%d" sum
    let lastDigit = intFromChar sumStr.[sumStr.Length - 1]
    let tenMinusLastDigit = 10 - lastDigit
    let tenMinusLastDigitAsStr = sprintf "%d" tenMinusLastDigit
    let lastDigitOfDifference = tenMinusLastDigitAsStr.[tenMinusLastDigitAsStr.Length - 1]
    let cs = intFromChar lastDigitOfDifference

    sprintf "%d" cs

let anonymizeSSN (ssn: string) =
    incrementNumberInString ssn 8

let generateSwedishSSN (random: Random) (birthdate: DateTime) (gender: Gender) (isAnonymizingSSN: bool) =
    let day   = birthdate.Day  .ToString("D2")
    let month = birthdate.Month.ToString("D2")
    let year  = birthdate.Year .ToString("D4").Substring(2)
    let date = sprintf "%s%s%s" year month day
    
    let individualNumber = generateSwedishIndividualNumber random gender
    let numbers = sprintf "%s%s" date individualNumber
    let checksum = generateSwedishChecksum numbers

    let ssn = sprintf "%s-%s%s" date individualNumber checksum
    match isAnonymizingSSN with
    | true  -> anonymizeSSN ssn 
    | false -> ssn
