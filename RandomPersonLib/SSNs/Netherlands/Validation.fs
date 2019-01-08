module internal NetherlandsSSNValidation

open CommonValidation
open NetherlandsSSNGeneration
open NetherlandsSSNParameters
open Util
open StringUtil

let getCalculatedCs (ssn: string) =
    let individualNumber = ssn |> substring IndividualNumberStart IndividualNumberLength
    
    generateChecksum individualNumber

let validateSSNForNetherlands =
    hasCorrectShape "^\d{9}$"
    >> bind (hasIndividualNumber IndividualNumberStart IndividualNumberLength)
    >> bind (hasCorrectChecksum getCalculatedCs ChecksumStart ChecksumLength)
    >> toOutputResult
