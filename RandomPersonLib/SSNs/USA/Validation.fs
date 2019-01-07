module internal UsaSSNValidation

open System.Text.RegularExpressions
open UsaSSNParameters
open StringUtil

let (|HasCorrectShape|_|) (potentialSSN: string) (_: string) =
    let regexMatch = Regex.Match(potentialSSN, "^\d{3}-\d{2}-\d{4}$")

    match regexMatch.Success with
    | true  -> Some(potentialSSN)
    | false -> None

let (|HasCorrectAreaNumber|_|) (potentialSsn: string) (_: string) =
    let areaNumber = potentialSsn |> substring AreaNumberStart AreaNumberLength |> int

    match areaNumber with
    | x when x = 0    -> None
    | x when x = 666  -> None
    | x when 900 <= x -> None
    | _               ->
        let start = AreaNumberStart + AreaNumberLength + 1
        let length = potentialSsn.Length - start
        let rest = potentialSsn |> substring start length
        Some(rest)

let (|HasCorrectGroupNumber|_|) (rest: string) (_: string) =
    let groupNumber = rest |> substring 0 GroupNumberLength |> int

    match groupNumber with
    | x when x = 0  -> None
    | _             ->
        let start = GroupNumberLength + 1
        let length = rest.Length - start
        let newRest = rest |> substring start length
        Some(newRest)

let (|HasCorrectSerialNumber|_|) (rest: string) (_: string) =
    let serialNumber = rest |> substring 0 SerialNumberLength |> int

    match serialNumber with
    | x when x = 0  -> None
    | _             -> Some()

let validateSSNForUSA (ssn: string) = 
    match ssn with
    | HasCorrectShape ssn potentialSSN ->
        match potentialSSN with
        | HasCorrectAreaNumber ssn rest ->
            match rest with
            | HasCorrectGroupNumber rest newRest ->
                match newRest with 
                | HasCorrectSerialNumber newRest _ -> true
                | _ -> false
            | _ -> false
        | _  -> false
    | _ -> false