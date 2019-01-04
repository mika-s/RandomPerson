module internal Creditcard

open System
open RandomUtil
open StringUtil
open ChecksumAlgorithms

type CardIssuer = VISA | MasterCard

let generateChecksum (numbers: string) =
    let weights = [| 2; 1; 2; 1; 2; 1; 2; 1; 2; 1; 2; 1; 2; 1; 2 |];
    luhn numbers weights

let generatePAN (random: Random) (removeHyphenFromPAN: bool) =
    let addHyphensToPAN (panWithoutHyphens: string) =
        panWithoutHyphens |> insert 4 "-" |> insert 9 "-" |> insert 14 "-"

    let generateBIN (random: Random) (issuer: CardIssuer) =
        match issuer with
        | VISA       -> "4" + generateRandomNumberString random 5 0 10
        | MasterCard -> generateRandomNumberString random 1 51 55 + generateRandomNumberString random 4 0 10

    let generateIndividualNumbers (amount: int) = generateRandomNumberString random amount 0 10

    let bin = generateBIN random MasterCard
    let individualNumbers = generateIndividualNumbers 9
    let checksum = generateChecksum (bin + individualNumbers)

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

let generateCVV (random: Random) = generateRandomNumberString random 3 0 10
