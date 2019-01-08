module internal IcelandSSNValidation

open System
open System.Globalization
open CommonValidation
open IcelandSSNGeneration
open IcelandSSNParameters
open Util
open StringUtil
open Types.SSNTypes

let hasCorrectChecksum (ssn: string) =
    let datePart = ssn |> substring DateStart DateLength
    let datePattern = "ddMMyy"
    let _, birthDate = DateTime.TryParseExact(datePart, datePattern, CultureInfo.InvariantCulture, DateTimeStyles.None)
    let individualNumber = ssn |> substring IndividualNumberStart IndividualNumberLength

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

let validateSSNForIceland = 
    hasCorrectShape "^\d{6}-\d{4}$"
    >> bind (hasDate "ddMMyy" DateStart DateLength)
    >> bind (hasIndividualNumber IndividualNumberStart IndividualNumberLength)
    >> bind hasCorrectChecksum
    >> bind hasProperCenturyNumber
    >> toBool
