module internal NetherlandsSSNValidation

open CommonValidation
open NetherlandsSSNGeneration
open NetherlandsSSNParameters
open Util
open StringUtil
open Types.SSNTypes

let hasCorrectChecksum (ssn: string) =
    let individualNumber = ssn |> substring IndividualNumberStart IndividualNumberLength

    let cs = generateChecksum individualNumber
    let csFromSSN = ssn |> substring ChecksumStart ChecksumLength

    match csFromSSN with
    | Equals cs -> Success ssn
    | _         -> Failure WrongChecksum

let validateSSNForNetherlands =
    hasCorrectShape "^\d{9}$"
    >> bind (hasIndividualNumber IndividualNumberStart IndividualNumberLength)
    >> bind hasCorrectChecksum
    >> toBool
