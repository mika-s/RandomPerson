module internal ValidateSSN

open RandomPersonLib
open DenmarkSSNValidation
open FinlandSSNValidation
open FranceSSNValidation
open IcelandSSNValidation
open NetherlandsSSNValidation
open NorwaySSNValidation
open SwedenSSNValidation
open UsaSSNValidation

let validateSSN (country: Country) (ssn: string) =
    match country with
    | Country.Denmark     -> validateSSNForDenmark ssn
    | Country.Finland     -> validateSSNForFinland ssn
    | Country.France      -> validateSSNForFrance ssn
    | Country.Iceland     -> validateSSNForIceland ssn
    | Country.Netherlands -> validateSSNForNetherlands ssn
    | Country.Norway      -> validateSSNForNorway ssn
    | Country.Sweden      -> validateSSNForSweden ssn
    | Country.USA         -> validateSSNForUSA ssn
    | _ -> invalidArg "country" "Illegal country."
