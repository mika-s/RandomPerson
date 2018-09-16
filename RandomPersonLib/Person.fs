namespace RandomPersonLib

open System
open SSN
open Address
open PostalCodeAndCityGen
open Name
open Email
open Gender
open Birthdate
open Password
open Phone
open FilesForLanguage
open GenericFiles

/// A class representing a randomly generated person.
type Person(nationality: Nationality, genericFiles: genericFiles, languageFiles: filesForLanguage, options: RandomPersonOptions, random: Random) =
    let isAnonymizingSSN = options.AnonymizeSSN
    let isAllowingUnder18 = options.Under18
    let isRemovingHypensFromSSN = options.RemoveHyphenFromSSN
    
    let postalCodeAndCity = generatePostalCodeAndCity random languageFiles.postalCodesAndCities nationality

    let gender = generateGender random
    let firstName = generateFirstName random gender languageFiles.generalData
    let lastName  = generateLastName  random gender languageFiles.generalData 
    let address1 = generateAddress1 random languageFiles.addresses1
    let address2 = generateAddress2 ()
    let postalCode = postalCodeAndCity.PostalCode
    let city = postalCodeAndCity.City
    let birthDate = generateBirthDate random isAllowingUnder18 options.BirthDate
    let ssn = generateSSN random nationality birthDate gender isAnonymizingSSN isRemovingHypensFromSSN
    let email = generateEmailAddress random languageFiles.generalData.EmailEndings firstName lastName birthDate
    let password = generatePassword random genericFiles.passwords firstName lastName birthDate
    let mobilePhone = generatePhone
                            random
                            languageFiles.generalData.Phone.CountryCode
                            languageFiles.generalData.Phone.TrunkPrefix
                            languageFiles.generalData.Phone.Mobile.Patterns
                            options.AddCountryCodeToPhoneNumber
                            options.RemoveHyphenFromPhoneNumber
                            options.RemoveSpaceFromPhoneNumber
    let homePhone = generatePhone
                            random
                            languageFiles.generalData.Phone.CountryCode
                            languageFiles.generalData.Phone.TrunkPrefix
                            languageFiles.generalData.Phone.Home.Patterns
                            options.AddCountryCodeToPhoneNumber
                            options.RemoveHyphenFromPhoneNumber
                            options.RemoveSpaceFromPhoneNumber
    let countryNameEnglish = languageFiles.generalData.Misc.CountryNameEnglish
    let countryNameNative = languageFiles.generalData.Misc.CountryNameNative
    let countryNameNativeAlternative1 = if languageFiles.generalData.Misc.CountryNameNativeAlternative1 <> null then
                                           languageFiles.generalData.Misc.CountryNameNativeAlternative1 else ""
    let countryNameNativeAlternative2 = if languageFiles.generalData.Misc.CountryNameNativeAlternative2 <> null then
                                           languageFiles.generalData.Misc.CountryNameNativeAlternative2 else ""
    let countryCode2  = languageFiles.generalData.Misc.CountryCode2
    let countryCode3  = languageFiles.generalData.Misc.CountryCode3
    let countryNumber = languageFiles.generalData.Misc.CountryNumber

    member __.Gender = gender
    member __.FirstName = firstName
    member __.LastName = lastName
    member __.Address1 = address1
    member __.Address2 = address2
    member __.PostalCode = postalCode
    member __.City = city
    member __.Nationality = nationality
    member __.BirthDate = birthDate
    member __.SSN = ssn
    member __.Email = email
    member __.Password = password
    member __.MobilePhone = mobilePhone
    member __.HomePhone = homePhone
    member __.CountryNameEnglish = countryNameEnglish
    member __.CountryNameNative = countryNameNative
    member __.CountryNameNativeAlternative1 = countryNameNativeAlternative1
    member __.CountryNameNativeAlternative2 = countryNameNativeAlternative2
    member __.CountryCode2  = countryCode2
    member __.CountryCode3  = countryCode3
    member __.CountryNumber = countryNumber
