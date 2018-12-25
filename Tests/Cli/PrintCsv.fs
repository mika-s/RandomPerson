namespace Tests

open Microsoft.VisualStudio.TestTools.UnitTesting
open Settings
open PrintCsv

[<TestClass>]
type ``createHeader should`` () =

    [<TestMethod>]
    member __.``return an array with one string element containg the header with wanted values 1`` () =
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
            MobilePhone = false;
            HomePhone = true;
            CountryNameEnglish = false;
            CountryNameNative = false;
            CountryNameNativeAlternative1 = false;
            CountryNameNativeAlternative2 = false;
            CountryCode2 = false;
            CountryCode3 = false;
            CountryNumber = false;
            TLD = false;
        }

        let header = createHeader genericPrintSettings

        CollectionAssert.AreEqual([| "FirstName,LastName,HomePhone" |], header)

    [<TestMethod>]
    member __.``return an array with one string element containg the header with wanted values 2`` () =
        let genericPrintSettings = {
            Label = false;
            FirstName = false;
            LastName = true;
            SSN = true;
            Country = false;
            Address1 = false;
            Address2 = false;
            PostalCode = true;
            City = false;
            State = false;
            BirthDate = false;
            Gender = false;
            Email = false;
            Password = false;
            MobilePhone = true;
            HomePhone = true;
            CountryNameEnglish = false;
            CountryNameNative = false;
            CountryNameNativeAlternative1 = false;
            CountryNameNativeAlternative2 = false;
            CountryCode2 = false;
            CountryCode3 = false;
            CountryNumber = false;
            TLD = false;
        }

        let header = createHeader genericPrintSettings

        CollectionAssert.AreEqual([| "LastName,PostalCode,SSN,MobilePhone,HomePhone" |], header)

    [<TestMethod>]
    member __.``return an array with one string element containg the header with wanted values 3`` () =
        let genericPrintSettings = {
            Label = false;
            FirstName = false;
            LastName = false;
            SSN = true;
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
            MobilePhone = false;
            HomePhone = false;
            CountryNameEnglish = false;
            CountryNameNative = false;
            CountryNameNativeAlternative1 = false;
            CountryNameNativeAlternative2 = false;
            CountryCode2 = false;
            CountryCode3 = false;
            CountryNumber = false;
            TLD = false;
        }

        let header = createHeader genericPrintSettings

        CollectionAssert.AreEqual([| "SSN" |], header)

    [<TestMethod>]
    member __.``not throw an exception when all print options are false`` () =
        let genericPrintSettings = {
            Label = false;
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
            MobilePhone = false;
            HomePhone = false;
            CountryNameEnglish = false;
            CountryNameNative = false;
            CountryNameNativeAlternative1 = false;
            CountryNameNativeAlternative2 = false;
            CountryCode2 = false;
            CountryCode3 = false;
            CountryNumber = false;
            TLD = false;
        }

        let header = createHeader genericPrintSettings

        CollectionAssert.AreEqual([| "" |], header)