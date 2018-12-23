module internal UsaSSNValidation

open System.Text.RegularExpressions
open UsaSSNParameters

let (|AmericanSSN|_|) (potentialSSN: string) =
    let regexMatch = Regex.Match(potentialSSN, "^\d{3}-\d{2}-\d{4}$")

    match regexMatch.Success with
    | true  -> Some(potentialSSN)
    | false -> None

let (|HasCorrectAreaNumber|_|) (potentialSsn: string) (_: string) =
    let areaNumberString = potentialSsn.Substring(AreaNumberStart, AreaNumberLength)
    let areaNumber = int(areaNumberString)

    match areaNumber with
    | x when x = 666 -> None
    | x when 900 < x -> None
    | _              ->
        let start = AreaNumberStart + AreaNumberStart + 1
        let length = areaNumberString.Length - start
        let rest = potentialSsn.Substring(start, length)
        Some(rest)

let (|HasCorrectGroupNumber|_|) (rest: string) (_: string) =
    let groupNumberString = rest.Substring(0, GroupNumberLength)
    let groupNumber = int(groupNumberString)

    match groupNumber with
    | x when x < 10 -> None
    | _             ->
        let start = GroupNumberLength + 1
        let length = rest.Length - start
        let newRest = rest.Substring(start, length)
        Some(newRest)

let (|HasCorrectSerialNumber|_|) (rest: string) (_: string) =
    let serialNumberString = rest.Substring(0, SerialNumberLength)
    let serialNumber = int(serialNumberString)

    match serialNumber with
    | x when x < 10 -> None
    | _             ->
        let start = GroupNumberLength + 1
        let length = rest.Length - start
        let newRest = rest.Substring(start, length)
        Some(newRest)

let validateAmericanSSN (ssn: string) = 
    match ssn with
    | HasCorrectAreaNumber ssn rest ->
        match rest with
        | HasCorrectGroupNumber rest newRest ->
            match newRest with 
            | HasCorrectSerialNumber newRest _ -> true
            | _ -> false
        | _ -> false
    | _  -> false
