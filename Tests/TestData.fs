module TestData

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

let getPhoneDetails () =
    { 
        CountryCode = 47;
        TrunkPrefix = "";
        Mobile = { Patterns = [| "4xxxxxxx" |] };
        Home   = { Patterns = [| "2xxxxxxx" |] };
    }

let getEmailAddresseses () = [| "gmail.com"; "hotmail.com"; "msn.com" |]

let getPasswords () = [| "asdf"; "password"; "2134"; "qwerty" |]

let getTestPerson () = 
    let options = RandomPersonOptions()

    let genericFiles = {
        passwords = getPasswords ();
    }

    let langugageFiles = {
        generalData = {
                        Phone = getPhoneDetails ();
                        EmailEndings = getEmailAddresseses ();
                        MaleFirstNames = [| "Nicolas" |];
                        FemaleFirstNames = [| "Diana" |];
                        LastNames = [| "Smith" |];
        };
        addresses1 = [| "Test street" |];
        postalCodesAndCities = [| PostalCodeAndCity("0001", "OSLO") |];
    }

    let random = getRandom false 100

    Person(Nationality.Norwegian, genericFiles, langugageFiles, options, random)
