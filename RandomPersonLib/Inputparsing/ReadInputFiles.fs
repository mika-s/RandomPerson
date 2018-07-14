module internal ReadInputFiles

open System.IO
open System.Text
open RandomPersonLib
open FilesForLanguage
open PostalCodeAndCityGen
open PersonData
open Util

type inputFiles = {
    danish:    filesForLanguage
    finnish:   filesForLanguage
    norwegian: filesForLanguage
    swedish:   filesForLanguage
}

let readInputFiles () =
    let danish = {
        generalData          = readDataFromJsonFile<PersonData> "data/Danish/danish.json";
        addresses1           = File.ReadAllLines("data/Danish/Gader i København.txt", Encoding.UTF8);
        postalCodesAndCities = File.ReadAllLines("data/Danish/DK.txt", Encoding.UTF8)
                               |> Array.map(stringToPostalCodeAndCity Nationality.Danish);
    }

    let finnish = {
        generalData          = readDataFromJsonFile<PersonData> "data/Finnish/finnish.json";
        addresses1           = File.ReadAllLines("data/Finnish/Streets in Finland.txt", Encoding.UTF8);
        postalCodesAndCities = File.ReadAllLines("data/Finnish/FI.txt", Encoding.UTF8)
                               |> Array.map(stringToPostalCodeAndCity Nationality.Finnish);
    }

    let norwegian = {
        generalData          = readDataFromJsonFile<PersonData> "data/Norwegian/norwegian.json";
        addresses1           = File.ReadAllLines("data/Norwegian/Gater i Oslo.txt", Encoding.UTF8);
        postalCodesAndCities = File.ReadAllLines("data/Norwegian/Postnummerregister-ansi.txt", Encoding.UTF8)
                               |> Array.map(stringToPostalCodeAndCity Nationality.Norwegian);
    }

    let swedish = {
        generalData          = readDataFromJsonFile<PersonData> "data/Swedish/swedish.json";
        addresses1           = File.ReadAllLines("data/Swedish/Gator i Stockholm.txt", Encoding.UTF8);
        postalCodesAndCities = File.ReadAllLines("data/Swedish/SE.txt", Encoding.UTF8)
                               |> Array.map(stringToPostalCodeAndCity Nationality.Swedish);
    }

    {
        danish = danish;
        finnish = finnish;
        norwegian = norwegian;
        swedish = swedish;
    }
