module internal Validate

open DenmarkSSNValidation
open FinlandSSNValidation
open IcelandSSNValidation
open NetherlandsSSNValidation
open NorwaySSNValidation
open SwedenSSNValidation
open UsaSSNValidation

let validateDK (ssn: string) =
    match ssn with
    | SSNForDenmark readSSN -> validateSSNForDenmark readSSN
    | _ -> false

let validateFI (ssn: string) =
    match ssn with
    | SSNForFinland readSSN -> validateSSNForFinland readSSN
    | _ -> false

let validateIC (ssn: string) =
    match ssn with
    | SSNForIceland readSSN -> validateSSNForIceland readSSN
    | _ -> false

let validateNL (ssn: string) =
    match ssn with
    | SSNForNetherlands readSSN -> validateSSNForNetherlands readSSN
    | _ -> false

let validateNO (ssn: string) =
    match ssn with
    | SSNForNorway ssn -> validateSSNForNorway ssn
    | _ -> false

let validateUS (ssn: string) =
    match ssn with
    | SSNForUSA ssn -> validateSSNForUSA ssn
    | _ -> false

let validateSE (ssn: string) =
    match ssn with
    | OldSSNForSweden -> validateSSNForSweden ssn false
    | NewSSNForSweden -> validateSSNForSweden ssn true
    | NotSSN        -> false
