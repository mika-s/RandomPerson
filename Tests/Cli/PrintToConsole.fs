﻿namespace Tests

open Microsoft.VisualStudio.TestTools.UnitTesting
open Settings
open PrintToConsole

[<TestClass>]
type ``isPrintingMoreThanOneLine should`` () =

    [<TestMethod>]
    member __.``return true if more than one genericPrintSettings boolean is true`` () =
        let genericPrintSettings = {
            Label = true;
            FirstName = true;
            LastName = true;
            SSN = false;
            Country = false;
            Address1 = false;
            Address2 = false;
            PostalCode = false;
            City = false;
            State = false;
            BirthDate = false;
            Gender = false;
            Email = false;
            Password = false;
            MacAddress = false;
            MobilePhone = false;
            HomePhone = false;
            PIN = false;
            PAN = false;
            Expiry = false;
            CVV = false;
            CountryNameEnglish = false;
            CountryNameNative = false;
            CountryNameNativeAlternative1 = false;
            CountryNameNativeAlternative2 = false;
            CountryCode2 = false;
            CountryCode3 = false;
            CountryNumber = false;
            TLD = false;
        }

        let result = isPrintingMoreThanOneLine genericPrintSettings

        Assert.AreEqual(true, result)

    [<TestMethod>]
    member __.``return false if only one genericPrintSettings booleans is true`` () =
        let genericPrintSettings = {
            Label = true;
            FirstName = true;
            LastName = false;
            SSN = false;
            Country = false;
            Address1 = false;
            Address2 = false;
            PostalCode = false;
            City = false;
            State = false;
            BirthDate = false;
            Gender = false;
            Email = false;
            Password = false;
            MacAddress = false;
            MobilePhone = false;
            HomePhone = false;
            PIN = false;
            PAN = false;
            Expiry = false;
            CVV = false;
            CountryNameEnglish = false;
            CountryNameNative = false;
            CountryNameNativeAlternative1 = false;
            CountryNameNativeAlternative2 = false;
            CountryCode2 = false;
            CountryCode3 = false;
            CountryNumber = false;
            TLD = false;
        }

        let result = isPrintingMoreThanOneLine genericPrintSettings

        Assert.AreEqual(false, result)

    [<TestMethod>]
    member __.``return false if all genericPrintSettings booleans are false`` () =
        let genericPrintSettings = {
            Label = true;
            FirstName = false;
            LastName = false;
            SSN = false;
            Country = false;
            Address1 = false;
            Address2 = false;
            PostalCode = false;
            City = false;
            State = false;
            BirthDate = false;
            Gender = false;
            Email = false;
            Password = false;
            MacAddress = false;
            MobilePhone = false;
            HomePhone = false;
            PIN = false;
            PAN = false;
            Expiry = false;
            CVV = false;
            CountryNameEnglish = false;
            CountryNameNative = false;
            CountryNameNativeAlternative1 = false;
            CountryNameNativeAlternative2 = false;
            CountryCode2 = false;
            CountryCode3 = false;
            CountryNumber = false;
            TLD = false;
        }

        let result = isPrintingMoreThanOneLine genericPrintSettings

        Assert.AreEqual(false, result)