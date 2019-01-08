module internal SwedenSSNValidation

open System
open System.Globalization
open System.Text.RegularExpressions
open SwedenSSNGeneration
open SwedenSSNParameters
open Util
open StringUtil
open Types.SSNTypes

let (|OldSSNForSweden|NewSSNForSweden|NotSSN|) (ssn: string) =
    let regexMatchOld = Regex.Match(ssn, "^\d{6}-\d{4}$")
    let regexMatchNew = Regex.Match(ssn, "^\d{8}-\d{4}$")

    match (regexMatchOld.Success, regexMatchNew.Success) with
    | (_, true) -> NewSSNForSweden
    | (true, _) -> OldSSNForSweden
    | _         -> NotSSN

let hasDate (p: SSNParams) (ssn: string) =
    let datePart = ssn |> substring p.DateStart p.DateLength
    let isDate, _ = DateTime.TryParseExact(datePart, p.DateFormat, CultureInfo.InvariantCulture, DateTimeStyles.None)

    match isDate with
    | true  -> Success ssn
    | false -> Failure WrongDate

let hasIndividualNumber (p: SSNParams) (ssn: string) =
    let individualNumberPart = ssn |> substring p.IndividualNumberStart p.IndividualNumberLength
    let isInt, _ = Int32.TryParse(individualNumberPart)

    match isInt with
    | true  -> Success ssn
    | false -> Failure WrongIndividualNumber

let hasCorrectChecksum (p: SSNParams) (ssn: string) =
    let birthDate = match p.SsnLength with
                    | Equals oldSsnParams.SsnLength -> ssn |> substring p.DateStart p.DateLength
                    | Equals newSsnParams.SsnLength -> ssn |> substring (p.DateStart + 2) (p.DateLength - 2)   // omit two first digits in the date
                    | _  -> invalidOp "Wrong SSN length in parameters."

    let individualNumber = ssn |> substring p.IndividualNumberStart p.IndividualNumberLength

    let cs = generateChecksum (birthDate + individualNumber)
    let csFromSSN = ssn |> substring p.ChecksumStart p.ChecksumLength

    match csFromSSN with
    | Equals cs -> Success ssn
    | _         -> Failure WrongChecksum

let toString (result: SSNValidationResult<string>) =
    match result with
    | Success _ -> true
    | Failure _ -> false

let validateSSNForSwedenGivenParams (p: SSNParams) =
    let hasIndividualNumberForParams = hasIndividualNumber p
    let hasCorrectChecksumForParams = hasCorrectChecksum p
    let hasDateForParams = hasDate p

    hasDateForParams
    >> bind hasIndividualNumberForParams
    >> bind hasCorrectChecksumForParams
    >> toString

let validateSSNForSweden (ssn: string) =
    match ssn with 
    | OldSSNForSweden -> validateSSNForSwedenGivenParams oldSsnParams ssn
    | NewSSNForSweden -> validateSSNForSwedenGivenParams newSsnParams ssn
    | NotSSN          -> false
