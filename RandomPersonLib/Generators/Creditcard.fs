module internal Creditcard

open System
open RandomUtil
open StringUtil
open ChecksumAlgorithms
open RandomPersonLib

let generateChecksum (numbers: string) =
    let weights =  [ for x in 0 .. numbers.Length - 1 do yield if x % 2 = 0 then 2 else 1 ]
                   |> List.toArray<int>

    luhn numbers weights

let addSpacesToPAN (issuer: CardIssuer) (panWithoutHyphens: string) =
    match issuer with
    | CardIssuer.AmericanExpress -> panWithoutHyphens |> insert 4 " " |> insert 11 " "
    | CardIssuer.DinersClub      -> panWithoutHyphens |> insert 4 " " |> insert 11 " "
    | CardIssuer.Discover        -> panWithoutHyphens |> insert 4 " " |> insert 9 " " |> insert 14 " "
    | CardIssuer.Visa            -> panWithoutHyphens |> insert 4 " " |> insert 9 " " |> insert 14 " "
    | CardIssuer.MasterCard      -> panWithoutHyphens |> insert 4 " " |> insert 9 " " |> insert 14 " "
    | _ -> invalidArg "issuer" "Illegal card issuer."

let generateBIN (random: Random) (issuer: CardIssuer) =
    match issuer with
    | CardIssuer.AmericanExpress ->
        match random.Next(0, 2) with
        | 0 -> "34" + generateRandomNumberString random 4 0 10
        | _ -> "37" + generateRandomNumberString random 4 0 10
    | CardIssuer.DinersClub -> "36" + generateRandomNumberString random 4 0 10
    | CardIssuer.Discover ->
        match random.Next(0, 3) with
        | 0 -> "6011" + generateRandomNumberString random 2 0 10
        | 1 ->   "64" + generateRandomNumberString random 4 0 10
        | _ ->   "65" + generateRandomNumberString random 4 0 10
    | CardIssuer.Visa       -> "4" + generateRandomNumberString random 5 0 10
    | CardIssuer.MasterCard -> generateRandomNumberString random 1 51 55 + generateRandomNumberString random 4 0 10
    | _ -> invalidArg "issuer" "Illegal card issuer."

let generateIndividualNumbers (random: Random) (issuer: CardIssuer) =
    match issuer with
    | CardIssuer.AmericanExpress -> generateRandomNumberString random 8 0 10
    | CardIssuer.DinersClub      -> generateRandomNumberString random 9 0 10
    | CardIssuer.Discover        -> generateRandomNumberString random 8 0 10
    | CardIssuer.Visa            -> generateRandomNumberString random 9 0 10
    | CardIssuer.MasterCard      -> generateRandomNumberString random 9 0 10
    | _ -> invalidArg "issuer" "Illegal card issuer."

let generatePAN (random: Random) (issuer: CardIssuer) (removeSpacesFromPAN: bool) =
    let bin = generateBIN random issuer
    let individualNumbers = generateIndividualNumbers random issuer
    let checksum = generateChecksum (bin + individualNumbers)

    let panWithoutHyphens = sprintf "%s%s%s" bin individualNumbers checksum

    match removeSpacesFromPAN with
    | true  -> panWithoutHyphens
    | false -> panWithoutHyphens |> addSpacesToPAN issuer

let generatePIN (random: Random) (length: int) = generateRandomNumberString random length 0 10

let generateExpiry (random: Random) (yearsIntoFuture: int) =
    let addLeadingZeroIfNeeded (numberAsStr: string) =
        if numberAsStr.Length = 1 then sprintf "0%s" numberAsStr else numberAsStr

    let month = generateRandomNumberString random 1 0 13 |> addLeadingZeroIfNeeded
    let year = (DateTime.Now.Year + yearsIntoFuture).ToString("D4").Substring(2)

    sprintf "%s/%s" month year

let generateCVV (random: Random) = generateRandomNumberString random 3 0 10
