﻿module TestData

open RandomPersonLib
open PostalCodeAndCity
open FilesForLanguage
open GenericFiles
open PersonData
open Util

type PlainTestClass (firstname: string, lastname: string, isMarried: bool) =
    member this.Firstname = firstname
    member this.Lastname = lastname
    member this.IsMarried = isMarried

let getMiscDetails () =
    {
        CountryNameEnglish = "Norway";
        CountryNameNative = "Norge";
        CountryNameNativeAlternative1 = "Noreg";
        CountryNameNativeAlternative2 = "Norga";
        CountryCode2 = "NO";
        CountryCode3 = "NOR";
        CountryNumber = "578";
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

let getPasswords () = [| "asdf"; "password"; "2134"; "qwerty" |]

let getTestPerson () = 
    let options = RandomPersonOptions()

    let genericFiles = {
        passwords = getPasswords ();
    }

    let langugageFiles = {
        generalData = {
                        Misc = getMiscDetails ();
                        Phone = getPhoneDetails ();
                        EmailEndings = getEmailAddresseses ();
                        MaleFirstNames = [| "Nicolas" |];
                        FemaleFirstNames = [| "Diana" |];
                        LastNames = [| "Smith" |];
                        MaleLastNames = [| |];
                        FemaleLastNames = [| |];
        };
        addresses1 = getAddresses1 ()
        postalCodesAndCities = [| PostalCodeAndCity("0001", "OSLO") |];
    }

    let random = getRandom false 100

    Person(Nationality.Norwegian, genericFiles, langugageFiles, options, random)
