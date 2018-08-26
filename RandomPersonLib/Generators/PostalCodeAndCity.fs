module PostalCodeAndCityGen

open System
open RandomPersonLib
open PostalCodeAndCity

let stringToPostalCodeAndCity (nationality: Nationality) (line: string)   =
    let split = line.Split '\t'

    match nationality with
    | Nationality.Danish    -> PostalCodeAndCity(split.[0], split.[1])
    | Nationality.Dutch     -> PostalCodeAndCity(split.[0], split.[1])
    | Nationality.Finnish   -> PostalCodeAndCity(split.[0], split.[1])
    | Nationality.Icelandic -> PostalCodeAndCity(split.[0], split.[1])
    | Nationality.Norwegian -> PostalCodeAndCity(split.[0], split.[1])
    | Nationality.Swedish   -> PostalCodeAndCity(split.[1], split.[2])
    | _ -> invalidArg "nationality" "Illegal nationality."

let generatePostalCodeAndCity (random: Random) (postalCodeAndCities: PostalCodeAndCity[]) = 
    let randomNumber = random.Next(postalCodeAndCities.Length)
    postalCodeAndCities.[randomNumber]
