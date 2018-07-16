module internal Validate

open DanishSSNValidation
open FinnishSSNValidation
open IcelandicSSNValidation
open NorwegianSSNValidation
open SwedishSSNValidation

let validateDK (ssn: string) =
    match ssn with
    | DanishSSN readSSN -> validateDanishSSN readSSN
    | _ -> false

let validateFI (ssn: string) =
    match ssn with
    | FinnishSSN readSSN -> validateFinnishSSN readSSN
    | _ -> false

let validateIC (ssn: string) =
    match ssn with
    | IcelandicSSN readSSN -> validateIcelandicSSN readSSN
    | _ -> false

let validateNO (ssn: string) =
    match ssn with
    | NorwegianSSN ssn -> validateNorwegianSSN ssn
    | _ -> false

let validateSE (ssn: string) =
    match ssn with
    | SwedishSSNOld -> validateSwedishSSN ssn false
    | SwedishSSNNew -> validateSwedishSSN ssn true
    | NotSSN        -> false
