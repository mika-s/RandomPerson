module internal DenmarkSSNValidation

open System
open System.Globalization
open DenmarkSSNGeneration
open DenmarkSSNParameters
open Util
open StringUtil
open Types.SSNTypes
open CommonValidation

let hasCorrectChecksum (ssn: string) =
    let random = getRandom false 100

    let datePart = ssn |> substring DateStart DateLength
    let datePattern = "ddMMyy"
    let _, birthDate = DateTime.TryParseExact(datePart, datePattern, CultureInfo.InvariantCulture, DateTimeStyles.None)
    let individualNumber = ssn |> substring IndividualNumberStart IndividualNumberLength

    let cs = generateChecksum random birthDate individualNumber
    let csFromSSN = ssn |> substring ChecksumStart ChecksumLength

    match csFromSSN with
    | Equals cs -> Success ssn
    | _         -> Failure WrongChecksum

let validateSSNForDenmark = 
    hasCorrectShape "^\d{6}-\d{4}$"
    >> bind (hasDate "ddMMyy" DateStart DateLength)
    >> bind (hasIndividualNumber IndividualNumberStart IndividualNumberLength)
    >> bind hasCorrectChecksum
    >> toBool
