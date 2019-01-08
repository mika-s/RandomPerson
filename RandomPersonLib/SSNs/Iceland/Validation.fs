module internal IcelandSSNValidation

open System
open System.Globalization
open System.Text.RegularExpressions
open CommonValidation
open IcelandSSNGeneration
open IcelandSSNParameters
open Util
open StringUtil
open Types.SSNTypes

let hasCorrectShape (ssn: string) = 
    let regexMatch = Regex.Match(ssn, "^\d{6}-\d{4}$")

    match regexMatch.Success with
    | true  -> Success ssn
    | false -> Failure WrongShape

let hasCorrectChecksum (ssn: string) =
    let datePart = ssn |> substring DateStart DateLength
    let datePattern = "ddMMyy"

    let _, birthDate = DateTime.TryParseExact(datePart, datePattern, CultureInfo.InvariantCulture, DateTimeStyles.None)
    let individualNumber = ssn.Substring(IndividualNumberStart, IndividualNumberLength)

    let cs = generateChecksum birthDate individualNumber
    let csFromSSN = ssn |> substring ChecksumStart ChecksumLength

    match csFromSSN with
    | Equals cs -> Success ssn
    | _         -> Failure WrongChecksum

let hasProperCenturyNumber (ssn: string) =
    let centuryNumber = ssn |> substring CenturySignStart CenturySignLength

    match centuryNumber with
    | "8" | "9" | "0" -> Success ssn
    | _               -> Failure WrongCenturyNumber

let toString (result: SSNValidationResult<string>) =
    match result with
    | Success _ -> true
    | Failure _ -> false

let validateSSNForIceland = 
    let hasDateForIceland = hasDate DateStart DateLength
    let hasIndividualNumberForIceland = hasIndividualNumber IndividualNumberStart IndividualNumberLength

    hasCorrectShape
    >> bind hasDateForIceland
    >> bind hasIndividualNumberForIceland
    >> bind hasCorrectChecksum
    >> bind hasProperCenturyNumber
    >> toString
