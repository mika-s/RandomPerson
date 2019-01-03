module internal Creditcard

open System
open Util

type CardIssuer = VISA | MasterCard

let generatePAN (random: Random) (removeHyphenFromPAN: bool) =
    let addHyphensToPAN (panWithoutHyphens: string) =
        panWithoutHyphens |> insert 4 "-" |> insert 9 "-" |> insert 14 "-"

    let generateBIN (random: Random) (issuer: CardIssuer) =
        match issuer with
        | VISA       -> "4" + generateRandomNumberString random 5 0 10
        | MasterCard -> generateRandomNumberString random 1 51 55 + generateRandomNumberString random 4 0 10

    let generateIndividualNumbers (amount: int) = generateRandomNumberString random amount 0 10

    let sumTheDigits (product: int) =
        let productAsString = sprintf "%d" product

        intFromChar productAsString.[0] + intFromChar productAsString.[1]

    let luhn (w: int) (nAsChar: char) =
        let n = intFromChar nAsChar
        let product = n * w

        match product with
        | p when p >= 10 -> sumTheDigits product
        | _              -> product

    let generateChecksum (bin: string) (individualNumbers: string) =
        let weight = [| 2; 1; 2; 1; 2; 1; 2; 1; 2; 1; 2; 1; 2; 1; 2 |];
        let numbers = (bin + individualNumbers).ToCharArray ()

        let mapped = Array.map2(luhn) weight numbers
        let sum = mapped |> Array.sum |> sprintf "%d"
        let lastDigit = intFromChar sum.[sum.Length - 1]
        let tenMinusLastDigit = 10 - lastDigit
        let tenMinusLastDigitAsStr = sprintf "%d" tenMinusLastDigit
        let lastDigitOfDifference = tenMinusLastDigitAsStr.[tenMinusLastDigitAsStr.Length - 1]
        let cs = intFromChar lastDigitOfDifference

        sprintf "%d" cs

    let bin = generateBIN random MasterCard
    let individualNumbers = generateIndividualNumbers 9
    let checksum = generateChecksum bin individualNumbers

    let panWithoutHyphens = sprintf "%s%s%s" bin individualNumbers checksum

    match removeHyphenFromPAN with
    | true  -> panWithoutHyphens
    | false -> panWithoutHyphens |> addHyphensToPAN  

let generatePIN (random: Random) (length: int) = generateRandomNumberString random length 0 10

let generateExpiry (random: Random) (yearsIntoFuture: int) =
    let addLeadingZeroIfNeeded (numberAsStr: string) =
        if numberAsStr.Length = 1 then sprintf "0%s" numberAsStr else numberAsStr

    let month = generateRandomNumberString random 1 0 13 |> addLeadingZeroIfNeeded
    let year = (DateTime.Now.Year + yearsIntoFuture).ToString("D4").Substring(2)

    sprintf "%s/%s" month year

let generateCVV () =
    "123"
