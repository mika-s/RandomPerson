namespace RandomPersonLib

open System
open SSN
open Address
open PostalCodeAndCityGen
open Name
open Email
open Gender
open Birthdate
open Phone
open FilesForLanguage

/// A class representing a randomly generated person.
type Person(nationality: Nationality, files: filesForLanguage, options: RandomPersonOptions, random: Random) =
    let isAnonymizingSSN = options.AnonymizeSSN
    let isAllowingUnder18 = options.Under18
    let isRemovingHypensFromSSN = options.RemoveHyphenFromSSN
    
    let postalCodeAndCity = generatePostalCodeAndCity random files.postalCodesAndCities

    let gender = generateGender random
    let firstName = generateFirstName random gender files.generalData
    let lastName = generateLastName random files.generalData
    let address1 = generateAddress1 random files.addresses1
    let address2 = generateAddress2 ()
    let postalCode = postalCodeAndCity.PostalCode
    let city = postalCodeAndCity.City
    let birthDate = generateBirthDate random isAllowingUnder18 options.BirthDate
    let ssn = generateSSN random nationality birthDate gender isAnonymizingSSN isRemovingHypensFromSSN
    let email = generateEmailAddress random files.generalData.EmailEndings firstName lastName birthDate
    let mobilePhone = generatePhone
                            random
                            files.generalData.Phone.CountryCode
                            files.generalData.Phone.TrunkPrefix
                            files.generalData.Phone.Mobile.Patterns
                            options.AddCountryCodeToPhoneNumber
                            options.RemoveHyphenFromPhoneNumber
                            options.RemoveSpaceFromPhoneNumber
    let homePhone = generatePhone
                            random
                            files.generalData.Phone.CountryCode
                            files.generalData.Phone.TrunkPrefix
                            files.generalData.Phone.Home.Patterns
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
    member this.MobilePhone = mobilePhone
    member this.HomePhone = homePhone
