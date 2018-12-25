module TestData

open RandomPersonLib
open PostalCodeCityState
open CountryFiles
open GenericFiles
open PersonData
open Util

let getMiscDetails () =
    {
        CountryNameEnglish = "Norway";
        CountryNameNative = "Norge";
        CountryNameNativeAlternative1 = "Noreg";
        CountryNameNativeAlternative2 = "Norga";
        CountryCode2 = "NO";
        CountryCode3 = "NOR";
        CountryNumber = "578";
        TLD = "no";
    }

let getAddressDetails () =
    {
        NumberLocation = "After";
    }

let getPhoneDetails () =
    { 
        CountryCode = 47;
        TrunkPrefix = "";
        Mobile = { Patterns = [| "4xxxxxxx" |] };
        Home   = { Patterns = [| "2xxxxxxx" |] };
    }

let getAddresses1 () = [| "Test street"; "Test road" |];

let getEmailAddresseses () = [| "gmail.com"; "hotmail.com"; "msn.com" |]

let getMiscData () =
    {
        Misc = getMiscDetails ();
        Address = getAddressDetails ();
        Phone = getPhoneDetails ();
        EmailEndings = getEmailAddresseses ();
        MaleFirstNames = [| "Nicolas" |];
        FemaleFirstNames = [| "Diana" |];
        LastNames = [| "Smith" |];
        MaleLastNames = [| |];
        FemaleLastNames = [| |];
    };

let getPasswords () = [| "asdf"; "password"; "2134"; "qwerty" |]

let getTestPerson () = 
    let options = RandomPersonOptions()

    let genericFiles = {
        passwords = getPasswords ();
    }

    let countryFiles = {
        generalData = getMiscData ()
        addresses1 = getAddresses1 ()
        postalCodeCityStates = [| PostalCodeCityState("0001", "OSLO", "") |];
    }

    let random = getRandom false 100

    Person(Country.Norway, genericFiles, countryFiles, options, random)
