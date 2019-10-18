namespace RandomPersonLib

open System
open SSN
open Address
open Creditcard
open PostalCodeCityStatesGen
open Name
open Email
open Gender
open Birthdate
open Password
open MacAddress
open Phone
open CountryFiles
open GenericFiles

/// A class representing a randomly generated person.
[<NoEquality;NoComparison>]
type Person(country: Country, genericFiles: GenericFiles, countryFiles: CountryFiles, options: RandomPersonOptions, random: Random) =
    let isAnonymizingSSN = options.AnonymizeSSN
    let isAllowingUnder18 = options.Under18
    let isRemovingHypensFromSSN = options.RemoveHyphenFromSSN
    
    let postalCodeCityState = generatePostalCodeCityState random countryFiles.postalCodeCityStates country

    let gender = generateGender random
    let firstName = generateFirstName random gender countryFiles.generalData
    let lastName  = generateLastName  random gender countryFiles.generalData 
    let address1 = generateAddress1 random countryFiles.addresses1 countryFiles.generalData
    let address2 = generateAddress2 ()
    let postalCode = postalCodeCityState.PostalCode
    let city = postalCodeCityState.City
    let state = postalCodeCityState.State
    let birthDate = generateBirthDate random isAllowingUnder18 options.BirthDate
    let ssn = generateSSN random country birthDate gender isAnonymizingSSN isRemovingHypensFromSSN
    let email = generateEmailAddress random countryFiles.generalData.EmailEndings firstName lastName birthDate
    let password = generatePassword random genericFiles.passwords firstName lastName birthDate
    let macAddress = generateMacAddress random options.UseColonsInMacAddress options.UseUppercaseInMacAddress
    let mobilePhone = generatePhone
                            random
                            countryFiles.generalData.Phone.CountryCode
                            countryFiles.generalData.Phone.TrunkPrefix
                            countryFiles.generalData.Phone.Mobile.Patterns
                            options.AddCountryCodeToPhoneNumber
                            options.RemoveHyphenFromPhoneNumber
                            options.RemoveSpaceFromPhoneNumber
    let homePhone = generatePhone
                            random
                            countryFiles.generalData.Phone.CountryCode
                            countryFiles.generalData.Phone.TrunkPrefix
                            countryFiles.generalData.Phone.Home.Patterns
                            options.AddCountryCodeToPhoneNumber
                            options.RemoveHyphenFromPhoneNumber
                            options.RemoveSpaceFromPhoneNumber
    let pin = generatePIN random options.Creditcard.PinLength
    let pan = generatePAN random options.Creditcard.CardIssuer options.RemoveSpacesFromPAN
    let expiry = generateExpiry random 3
    let cvv = generateCVV random
    let countryNameEnglish = countryFiles.generalData.Misc.CountryNameEnglish
    let countryNameNative = countryFiles.generalData.Misc.CountryNameNative
    let countryNameNativeAlternative1 = if countryFiles.generalData.Misc.CountryNameNativeAlternative1 <> null then
                                           countryFiles.generalData.Misc.CountryNameNativeAlternative1 else ""
    let countryNameNativeAlternative2 = if countryFiles.generalData.Misc.CountryNameNativeAlternative2 <> null then
                                           countryFiles.generalData.Misc.CountryNameNativeAlternative2 else ""
    let countryCode2  = countryFiles.generalData.Misc.CountryCode2
    let countryCode3  = countryFiles.generalData.Misc.CountryCode3
    let countryNumber = countryFiles.generalData.Misc.CountryNumber
    let tld = countryFiles.generalData.Misc.TLD

    /// The gender of the randomly generated person.
    member __.Gender = gender

    /// The firstname of the randomly generated person.
    member __.FirstName = firstName

    /// The lastname of the randomly generated person.
    member __.LastName = lastName

    /// The first address line of the randomly generated person.
    member __.Address1 = address1

    /// The second address line of the randomly generated person.
    member __.Address2 = address2

    /// The postal code of the randomly generated person.
    member __.PostalCode = postalCode

    /// The city of the randomly generated person.
    member __.City = city

    /// The state of the randomly generated person.
    member __.State = state

    /// The country of the randomly generated person.
    member __.Country = country

    /// The birthdate of the randomly generated person.
    member __.BirthDate = birthDate

    /// The SSN of the randomly generated person.
    member __.SSN = ssn

    /// The email address of the randomly generated person.
    member __.Email = email

    /// The password of the randomly generated person.
    member __.Password = password

    /// The MAC address of the randomly generated person.
    member __.MacAddress = macAddress

    /// The mobile hpone number of the randomly generated person.
    member __.MobilePhone = mobilePhone

    /// The home phone number of the randomly generated person.
    member __.HomePhone = homePhone

    /// The PIN code of the randomly generated person.
    member __.PIN = pin

    /// The personal account number (creditcard number) of the randomly generated person.
    member __.PAN = pan

    /// The expiry date of the creditcard belonging to the randomly generated person.
    member __.Expiry = expiry

    /// The CVV of the creditcard belonging to the randomly generated person.
    member __.CVV = cvv

    /// The country name in English of the randomly generated person.
    member __.CountryNameEnglish = countryNameEnglish

    /// The country name in the country's native language of the randomly generated person.
    member __.CountryNameNative = countryNameNative

    /// The country name (alternative 1) of the randomly generated person.
    member __.CountryNameNativeAlternative1 = countryNameNativeAlternative1

    /// The country name (alternative 2) of the randomly generated person.
    member __.CountryNameNativeAlternative2 = countryNameNativeAlternative2

    /// The country code (2 digits) of the randomly generated person.
    member __.CountryCode2  = countryCode2

    /// The country code (3 digits) of the randomly generated person.
    member __.CountryCode3  = countryCode3

    /// The country number of the randomly generated person.
    member __.CountryNumber = countryNumber

    /// The TLD belonging to the country of the randomly generated person.
    member __.TLD = tld
