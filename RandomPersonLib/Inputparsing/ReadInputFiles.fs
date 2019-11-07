module internal ReadInputFiles

open System.IO
open System.Text
open RandomPersonLib
open CountryFiles
open GenericFiles
open PostalCodeCityStatesGen
open PersonData
open Util

[<NoEquality;NoComparison>]
type InputFiles = {
    generic:     GenericFiles
    denmark:     CountryFiles
    finland:     CountryFiles
    iceland:     CountryFiles
    netherlands: CountryFiles
    norway:      CountryFiles
    sweden:      CountryFiles
    usa:         CountryFiles
}

let readInputFiles () =
    let generic = {
        passwords             = File.ReadAllLines("RandomPersonLib/Generic/passwords.txt", Encoding.UTF8);
    }

    let denmark = {
        generalData          = readDataFromJsonFile<PersonData> "RandomPersonLib/Denmark/denmark.json";
        addresses1           = File.ReadAllLines("RandomPersonLib/Denmark/Gader i København.txt", Encoding.UTF8);
        postalCodeCityStates = File.ReadAllLines("RandomPersonLib/Denmark/DK.txt", Encoding.UTF8)
                               |> Array.map(stringToPostalCodeCityState Country.Denmark);
    }

    let finland = {
        generalData          = readDataFromJsonFile<PersonData> "RandomPersonLib/Finland/finland.json";
        addresses1           = File.ReadAllLines("RandomPersonLib/Finland/Streets in Finland.txt", Encoding.UTF8);
        postalCodeCityStates = File.ReadAllLines("RandomPersonLib/Finland/FI.txt", Encoding.UTF8)
                               |> Array.map(stringToPostalCodeCityState Country.Finland);
    }

    let iceland = {
        generalData          = readDataFromJsonFile<PersonData> "RandomPersonLib/Iceland/iceland.json";
        addresses1           = File.ReadAllLines("RandomPersonLib/Iceland/Streets in Iceland.txt", Encoding.UTF8);
        postalCodeCityStates = File.ReadAllLines("RandomPersonLib/Iceland/postnumer.txt", Encoding.UTF8)
                               |> Array.map(stringToPostalCodeCityState Country.Iceland);
    }

    let netherlands = {
        generalData          = readDataFromJsonFile<PersonData> "RandomPersonLib/Netherlands/netherlands.json";
        addresses1           = File.ReadAllLines("RandomPersonLib/Netherlands/Addresses in NL.txt", Encoding.UTF8);
        postalCodeCityStates = File.ReadAllLines("RandomPersonLib/Netherlands/NL.txt", Encoding.UTF8)
                               |> Array.map(stringToPostalCodeCityState Country.Netherlands);
    }

    let norway = {
        generalData          = readDataFromJsonFile<PersonData> "RandomPersonLib/Norway/norway.json";
        addresses1           = File.ReadAllLines("RandomPersonLib/Norway/Gater i Oslo.txt", Encoding.UTF8);
        postalCodeCityStates = File.ReadAllLines("RandomPersonLib/Norway/Postnummerregister-ansi.txt", Encoding.UTF8)
                               |> Array.map(stringToPostalCodeCityState Country.Norway);
    }

    let sweden = {
        generalData          = readDataFromJsonFile<PersonData> "RandomPersonLib/Sweden/sweden.json";
        addresses1           = File.ReadAllLines("RandomPersonLib/Sweden/Gator i Stockholm.txt", Encoding.UTF8);
        postalCodeCityStates = File.ReadAllLines("RandomPersonLib/Sweden/SE.txt", Encoding.UTF8)
                               |> Array.map(stringToPostalCodeCityState Country.Sweden);
    }

    let usa = {
        generalData          = readDataFromJsonFile<PersonData> "RandomPersonLib/USA/usa.json";
        addresses1           = File.ReadAllLines("RandomPersonLib/USA/Streets in Sullivan.txt", Encoding.UTF8);
        postalCodeCityStates = File.ReadAllLines("RandomPersonLib/USA/US.txt", Encoding.UTF8)
                               |> Array.map(stringToPostalCodeCityState Country.USA);
    }

    {
        generic = generic;
        denmark = denmark;
        finland = finland;
        iceland = iceland;
        netherlands = netherlands;
        norway = norway;
        sweden = sweden;
        usa = usa;
    }
