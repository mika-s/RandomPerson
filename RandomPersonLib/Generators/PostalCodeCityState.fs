module internal PostalCodeCityStatesGen

open System
open RandomPersonLib
open PostalCodeCityState
open Util

let stringToPostalCodeCityState (country: Country) (line: string)   =
    let split = line.Split '\t'

    match country with
    | Country.Denmark     -> PostalCodeCityState(split.[0], split.[1], "")
    | Country.Finland     -> PostalCodeCityState(split.[0], split.[1], "")
    | Country.Iceland     -> PostalCodeCityState(split.[0], split.[1], "")
    | Country.Netherlands -> PostalCodeCityState(split.[1], split.[2], "")
    | Country.Norway      -> PostalCodeCityState(split.[0], split.[1], "")
    | Country.Sweden      -> PostalCodeCityState(split.[1], split.[2], "")
    | Country.USA         -> PostalCodeCityState(split.[1], split.[2], split.[4])
    | _ -> invalidArg "country" "Illegal country."

let generatePostalCodeCityState (random: Random) (postalCodeAndCities: PostalCodeCityState[]) (country: Country) = 
    let randomNumber = random.Next(postalCodeAndCities.Length)
    let postalCodeAndCity = postalCodeAndCities.[randomNumber]

    // Special cases have to be handled.
    match country with
    | Country.Netherlands ->
        let addedLetters = (randomUppercaseLetter random).ToString() + (randomUppercaseLetter random).ToString()
        let withAddedLetters = postalCodeAndCity.PostalCode + " " + addedLetters
        PostalCodeCityState(withAddedLetters, postalCodeAndCity.City, "")
    | _ -> postalCodeAndCity
