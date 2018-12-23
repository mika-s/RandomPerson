module PostalCodeAndCityGen

open System
open RandomPersonLib
open PostalCodeAndCity
open Util

let stringToPostalCodeAndCity (country: Country) (line: string)   =
    let split = line.Split '\t'

    match country with
    | Country.Denmark     -> PostalCodeAndCity(split.[0], split.[1])
    | Country.Finland     -> PostalCodeAndCity(split.[0], split.[1])
    | Country.Iceland     -> PostalCodeAndCity(split.[0], split.[1])
    | Country.Netherlands -> PostalCodeAndCity(split.[1], split.[2])
    | Country.Norway      -> PostalCodeAndCity(split.[0], split.[1])
    | Country.Sweden      -> PostalCodeAndCity(split.[1], split.[2])
    | Country.USA         -> PostalCodeAndCity(split.[1], split.[2])
    | _ -> invalidArg "country" "Illegal country."

let generatePostalCodeAndCity (random: Random) (postalCodeAndCities: PostalCodeAndCity[]) (country: Country) = 
    let randomNumber = random.Next(postalCodeAndCities.Length)
    let postalCodeAndCity = postalCodeAndCities.[randomNumber]

    // Special cases have to be handled.
    match country with
    | Country.Netherlands ->
        let addedLetters = (randomUppercaseLetter random).ToString() + (randomUppercaseLetter random).ToString()
        let withAddedLetters = postalCodeAndCity.PostalCode + " " + addedLetters
        PostalCodeAndCity(withAddedLetters, postalCodeAndCity.City)
    | _ -> postalCodeAndCity
