module PostalCodeAndCityGen

open System
open RandomPersonLib
open PostalCodeAndCity
open Util

let stringToPostalCodeAndCity (nationality: Nationality) (line: string)   =
    let split = line.Split '\t'

    match nationality with
    | Nationality.Danish    -> PostalCodeAndCity(split.[0], split.[1])
    | Nationality.Dutch     -> PostalCodeAndCity(split.[1], split.[2])
    | Nationality.Finnish   -> PostalCodeAndCity(split.[0], split.[1])
    | Nationality.Icelandic -> PostalCodeAndCity(split.[0], split.[1])
    | Nationality.Norwegian -> PostalCodeAndCity(split.[0], split.[1])
    | Nationality.Swedish   -> PostalCodeAndCity(split.[1], split.[2])
    | _ -> invalidArg "nationality" "Illegal nationality."

let generatePostalCodeAndCity (random: Random) (postalCodeAndCities: PostalCodeAndCity[]) (nationality: Nationality) = 
    let randomNumber = random.Next(postalCodeAndCities.Length)
    let postalCodeAndCity = postalCodeAndCities.[randomNumber]

    // Special cases have to be handled.
    match nationality with
    | Nationality.Dutch ->
        let addedLetters = (randomUppercaseLetter random).ToString() + (randomUppercaseLetter random).ToString()
        let withAddedLetters = postalCodeAndCity.PostalCode + " " + addedLetters
        PostalCodeAndCity(withAddedLetters, postalCodeAndCity.City)
    | _ -> postalCodeAndCity
