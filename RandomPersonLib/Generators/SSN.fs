module internal SSN

open System
open RandomPersonLib
open DenmarkSSNGeneration
open FinlandSSNGeneration
open IcelandSSNGeneration
open NetherlandsSSNGeneration
open NorwaySSNGeneration
open SwedenSSNGeneration
open UsaSSNGeneration

let generateSSN (random: Random) (country: Country) (birthdate : DateTime) (gender: Gender) (isAnonymizingSSN: bool) (isRemovingHypensFromSSN: bool) =
    let ssn = match country with
              | Country.Denmark     -> generateDanishSSN    random birthdate gender isAnonymizingSSN
              | Country.Finland     -> generateFinnishSSN   random birthdate gender isAnonymizingSSN
              | Country.Iceland     -> generateIcelandicSSN random birthdate        isAnonymizingSSN
              | Country.Netherlands -> generateDutchSSN     random                  isAnonymizingSSN
              | Country.Norway      -> generateNorwegianSSN random birthdate gender isAnonymizingSSN
              | Country.Sweden      -> generateSwedishSSN   random birthdate gender isAnonymizingSSN
              | Country.USA         -> generateAmericanSSN  random                  isAnonymizingSSN
              | _ -> invalidArg "country" "Illegal country."

    match isRemovingHypensFromSSN with
    | true  -> ssn.Replace("-", "")
    | false -> ssn
