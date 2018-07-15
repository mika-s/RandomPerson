module internal SwedishSSNValidation

open System
open System.Globalization
open System.Text.RegularExpressions
open SwedishSSNGeneration
open SwedishSSNParameters
open Util

let (|SwedishSSNOld|SwedishSSNNew|NotSSN|) (potentialSSN: string) =
    let regexMatchOld = Regex.Match(potentialSSN, "^\d{6}-\d{4}$")
    let regexMatchNew = Regex.Match(potentialSSN, "^\d{8}-\d{4}$")

    match (regexMatchOld.Success, regexMatchNew.Success) with
    | (_, true) -> SwedishSSNNew
    | (true, _) -> SwedishSSNOld
    | _         -> NotSSN

let (|HasDate|_|) (_: string) (p: ssnParams) (ssn: string)  =
    let datePart = ssn.Substring(p.DateStart, p.DateLength)
    let isDate, _ = DateTime.TryParseExact(datePart, p.DateFormat, CultureInfo.InvariantCulture, DateTimeStyles.None)

    if isDate then
        Some(ssn.Substring(p.IndividualNumberStart, ssn.Length - p.IndividualNumberStart))
    else
        None

let (|HasIndividualNumber|_|) (_: string) (p: ssnParams) (s: string) =
    let individualNumberPart = s.Substring(0, p.IndividualNumberLength)
    let isInt, _ = Int32.TryParse(individualNumberPart)

    if isInt then
        Some(s.Substring(p.IndividualNumberLength, s.Length - p.IndividualNumberLength))
    else
        None

let isCorrectChecksum (csFromSSN: string) (ssn: string) (p: ssnParams)  =
    let birthDate = match p.SsnLength with
                    | Equals oldSsnParams.SsnLength -> ssn.Substring(p.DateStart, p.DateLength)
                    | Equals newSsnParams.SsnLength -> ssn.Substring(p.DateStart + 2, p.DateLength - 2)    // omit two first digits in the date
                    | _  -> invalidOp "Wrong SSN length in parameters."

    let individualNumber = ssn.Substring(p.IndividualNumberStart, p.IndividualNumberLength)

    let cs = generateSwedishChecksum (birthDate + individualNumber)

    match csFromSSN with
    | Equals cs -> true
    | _         -> false

let validateSwedishSSN2 (ssn: string) (p: ssnParams) =
    match ssn.Length with
    | Equals p.SsnLength ->
        match ssn with
        | HasDate ssn p rest ->
            match rest with
            | HasIndividualNumber rest p newRest -> isCorrectChecksum newRest ssn p
            | _ -> false
        | _ -> false
    | _  -> false

let validateSwedishSSN (ssn: string) (isNew: bool) = 
    match isNew with
    | false -> validateSwedishSSN2 ssn oldSsnParams
    | true  -> validateSwedishSSN2 ssn newSsnParams
