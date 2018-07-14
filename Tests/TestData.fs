module TestData

open RandomPersonLib
open PostalCodeAndCity
open FilesForLanguage
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

let getTestPerson () = 
    let phoneDetails = getPhoneDetails ()
    let emailEndings = getEmailAddresseses ()
    let generalData = {
        Phone = phoneDetails;
        EmailEndings = emailEndings;
        MaleFirstNames = [| "Nicolas" |];
        FemaleFirstNames = [| "Diana" |];
        LastNames = [| "Smith" |];
    }
    let addresses = [| "Test street" |]
    let postalCodesAndCities = [| PostalCodeAndCity("0001", "OSLO") |]
    let options = RandomPersonOptions()

    let files = {
        generalData = generalData;
        addresses1 = addresses;
        postalCodesAndCities = postalCodesAndCities;
    }

    let random = getRandom false 100

    Person(Nationality.Norwegian, files, options, random)
