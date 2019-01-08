module internal FinlandSSNValidation

open System
open System.Globalization
open CommonValidation
open FinlandSSNGeneration
open FinlandSSNParameters
open Util
open StringUtil

let getCalculatedCs (ssn: string) =
    let datePart = ssn |> substring DateStart DateLength
    let datePattern = "ddMMyy"

    let _, birthDate = DateTime.TryParseExact(datePart, datePattern, CultureInfo.InvariantCulture, DateTimeStyles.None)
    let individualNumber = ssn |> substring IndividualNumberStart IndividualNumberLength

    generateChecksum birthDate individualNumber

let validateSSNForFinland =
    hasCorrectShape "^\d{6}(-|\+|A)\d{3}[\dA-Y]$"
    >> bind (hasDate "ddMMyy" DateStart DateLength)
    >> bind (hasIndividualNumber IndividualNumberStart IndividualNumberLength)
    >> bind (hasCorrectChecksum getCalculatedCs ChecksumStart ChecksumLength)
    >> toOutputResult
