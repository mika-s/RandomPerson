module internal NorwaySSNValidation

open System
open System.Globalization
open CommonValidation
open NorwaySSNGeneration
open NorwaySSNParameters
open Util
open StringUtil

let getCalculatedCs (ssn: string) =
    let datePart = ssn |> substring DateStart DateLength
    let datePattern = "ddMMyy"
    let _, birthDate = DateTime.TryParseExact(datePart, datePattern, CultureInfo.InvariantCulture, DateTimeStyles.None)
    let individualNumber = ssn |> substring IndividualNumberStart IndividualNumberLength

    generateChecksum birthDate individualNumber

let validateSSNForNorway =
    hasCorrectShape "^\d{11}$"
    >> bind (hasDate "ddMMyy" DateStart DateLength)
    >> bind (hasIndividualNumber IndividualNumberStart IndividualNumberLength)
    >> bind (hasCorrectChecksum getCalculatedCs ChecksumStart ChecksumLength)
    >> toOutputResult
