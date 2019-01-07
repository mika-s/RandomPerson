module internal SwedenSSNValidation

open System
open System.Globalization
open System.Text.RegularExpressions
open SwedenSSNGeneration
open SwedenSSNParameters
open Util
open StringUtil

let (|OldSSNForSweden|NewSSNForSweden|NotSSN|) (potentialSSN: string) =
    let regexMatchOld = Regex.Match(potentialSSN, "^\d{6}-\d{4}$")
    let regexMatchNew = Regex.Match(potentialSSN, "^\d{8}-\d{4}$")

    match (regexMatchOld.Success, regexMatchNew.Success) with
    | (_, true) -> NewSSNForSweden
    | (true, _) -> OldSSNForSweden
    | _         -> NotSSN

let (|HasDate|_|) (_: string) (p: ssnParams) (ssn: string)  =
    let datePart = ssn |> substring p.DateStart p.DateLength
    let isDate, _ = DateTime.TryParseExact(datePart, p.DateFormat, CultureInfo.InvariantCulture, DateTimeStyles.None)

    match isDate with
    | true  -> Some(ssn.Substring(p.IndividualNumberStart, ssn.Length - p.IndividualNumberStart))
    | false -> None

let (|HasIndividualNumber|_|) (_: string) (p: ssnParams) (s: string) =
    let individualNumberPart = s |> substring 0 p.IndividualNumberLength
    let isInt, _ = Int32.TryParse(individualNumberPart)

    match isInt with
    | true  -> Some(s.Substring(p.IndividualNumberLength, s.Length - p.IndividualNumberLength))
    | false -> None

let (|HasCorrectChecksum|_|) (csFromSSN: string) (ssn: string) (p: ssnParams) (_: string)  =
    let birthDate = match p.SsnLength with
                    | Equals oldSsnParams.SsnLength -> ssn |> substring p.DateStart p.DateLength
                    | Equals newSsnParams.SsnLength -> ssn |> substring (p.DateStart + 2) (p.DateLength - 2)   // omit two first digits in the date
                    | _  -> invalidOp "Wrong SSN length in parameters."

    let individualNumber = ssn |> substring p.IndividualNumberStart p.IndividualNumberLength

    let cs = generateChecksum (birthDate + individualNumber)

    match csFromSSN with
    | Equals cs -> Some(cs)
    | _         -> None

let performSwedishSSNValidation (ssn: string) (p: ssnParams) =
    match ssn with
    | HasDate ssn p rest ->
        match rest with
        | HasIndividualNumber rest p newRest ->
            match newRest with
            | HasCorrectChecksum newRest ssn p _ -> true
            | _ -> false
        | _ -> false
    | _ -> false

let validateSSNForSweden (ssn: string) = 
    match ssn with
    | OldSSNForSweden -> performSwedishSSNValidation ssn oldSsnParams
    | NewSSNForSweden -> performSwedishSSNValidation ssn newSsnParams
    | NotSSN          -> false
