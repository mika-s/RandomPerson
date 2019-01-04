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

    member __.Gender = gender
    member __.FirstName = firstName
    member __.LastName = lastName
    member __.Address1 = address1
    member __.Address2 = address2
    member __.PostalCode = postalCode
    member __.City = city
    member __.State = state
    member __.Country = country
    member __.BirthDate = birthDate
    member __.SSN = ssn
    member __.Email = email
    member __.Password = password
    member __.MobilePhone = mobilePhone
    member __.HomePhone = homePhone
    member __.PIN = pin
    member __.PAN = pan
    member __.Expiry = expiry
    member __.CVV = cvv
    member __.CountryNameEnglish = countryNameEnglish
    member __.CountryNameNative = countryNameNative
    member __.CountryNameNativeAlternative1 = countryNameNativeAlternative1
    member __.CountryNameNativeAlternative2 = countryNameNativeAlternative2
    member __.CountryCode2  = countryCode2
    member __.CountryCode3  = countryCode3
    member __.CountryNumber = countryNumber
    member __.TLD = tld
