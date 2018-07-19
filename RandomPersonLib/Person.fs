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
    
    let postalCodeAndCity = generatePostalCodeAndCity random languageFiles.postalCodesAndCities

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

    member this.Gender = gender
    member this.FirstName = firstName
    member this.LastName = lastName
    member this.Address1 = address1
    member this.Address2 = address2
    member this.PostalCode = postalCode
    member this.City = city
    member this.Nationality = nationality
    member this.BirthDate = birthDate
    member this.SSN = ssn
    member this.Email = email
    member this.Password = password
    member this.MobilePhone = mobilePhone
    member this.HomePhone = homePhone
