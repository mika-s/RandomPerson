namespace Tests

open System
open System.Text.RegularExpressions
open Microsoft.VisualStudio.TestTools.UnitTesting
open RandomPersonLib
open Util
open SSN

[<TestClass>]
type ``generateSSN should`` () =

    [<TestMethod>]
    member __.``return a correct SSN for Danish male`` () =
        let country = Country.Denmark
        let birthdate = DateTime(1933, 7, 31)
        let gender = Gender.Male
        let random = getRandom false 100
        let ssn = generateSSN random country birthdate gender false false

        let d = ssn.Substring(0, 2)
        let m = ssn.Substring(2, 2)
        let y = ssn.Substring(4, 2)
        let individualNumber = Convert.ToInt32(ssn.Substring(DanishSSNParameters.IndividualNumberStart,
                                                             DanishSSNParameters.IndividualNumberLength))
        let checksum = Convert.ToInt32(ssn.Substring(DanishSSNParameters.ChecksumStart, DanishSSNParameters.ChecksumLength))

        Assert.AreEqual(DanishSSNParameters.SsnLength, ssn.Length)
        Assert.AreEqual("31", d)
        Assert.AreEqual("07", m)
        Assert.AreEqual("33", y)
        Assert.IsTrue(0 <= individualNumber && individualNumber <= 399)
        Assert.IsTrue(0 <= checksum && checksum <= 9)
        Assert.IsTrue(isOdd checksum)

    [<TestMethod>]
    member __.``return a correct SSN for Danish female`` () =
        let country = Country.Denmark
        let birthdate = DateTime(1962, 2, 3)
        let gender = Gender.Female
        let random = getRandom false 100
        let ssn = generateSSN random country birthdate gender false false

        let d = ssn.Substring(0, 2)
        let m = ssn.Substring(2, 2)
        let y = ssn.Substring(4, 2)
        let individualNumber = Convert.ToInt32(ssn.Substring(DanishSSNParameters.IndividualNumberStart,
                                                             DanishSSNParameters.IndividualNumberLength))
        let checksum = Convert.ToInt32(ssn.Substring(DanishSSNParameters.ChecksumStart, DanishSSNParameters.ChecksumLength))

        Assert.AreEqual(DanishSSNParameters.SsnLength, ssn.Length)
        Assert.AreEqual("03", d)
        Assert.AreEqual("02", m)
        Assert.AreEqual("62", y)
        Assert.IsTrue(0 <= individualNumber && individualNumber <= 499 || 900 <= individualNumber && individualNumber <= 999)
        Assert.IsTrue(0 <= checksum && checksum <= 9)
        Assert.IsTrue(isEven checksum)

    [<TestMethod>]
    member __.``return a correct SSN for Dutch person`` () =
        let country = Country.Netherlands
        let birthdate = DateTime(1962, 2, 3)
        let gender = Gender.Female
        let random = getRandom false 100
        let ssn = generateSSN random country birthdate gender false false

        let individualNumber = Convert.ToInt32(ssn.Substring(DutchSSNParameters.IndividualNumberStart,
                                                             DutchSSNParameters.IndividualNumberLength))
        let checksum = Convert.ToInt32(ssn.Substring(DutchSSNParameters.ChecksumStart, DutchSSNParameters.ChecksumLength))

        Assert.AreEqual(DutchSSNParameters.SsnLength, ssn.Length)
        Assert.IsTrue(0 <= individualNumber && individualNumber <= 99999999)
        Assert.IsTrue(0 <= checksum && checksum <= 9)

    [<TestMethod>]
    member __.``return a correct SSN for Finnish male`` () =
        let country = Country.Finland
        let birthdate = DateTime(1925, 8, 7)
        let gender = Gender.Male
        let random = getRandom false 100
        let ssn = generateSSN random country birthdate gender false false

        let d = ssn.Substring(0, 2)
        let m = ssn.Substring(2, 2)
        let y = ssn.Substring(4, 2)
        let centurySign = ssn.Substring(FinnishSSNParameters.CenturySignStart, FinnishSSNParameters.CenturySignLength)
        let individualNumber = Convert.ToInt32(ssn.Substring(FinnishSSNParameters.IndividualNumberStart,
                                                             FinnishSSNParameters.IndividualNumberLength))
        let checksum = ssn.Substring(FinnishSSNParameters.ChecksumStart, FinnishSSNParameters.ChecksumLength)
        let checksumPattern = "^(\d|[A-Y])$"
        let checksumRegex = Regex checksumPattern

        Assert.AreEqual(FinnishSSNParameters.SsnLength, ssn.Length)
        Assert.AreEqual("07", d)
        Assert.AreEqual("08", m)
        Assert.AreEqual("25", y)
        Assert.AreEqual("-", centurySign)
        Assert.IsTrue(002 <= individualNumber && individualNumber <= 899)
        Assert.IsTrue((checksumRegex.Match checksum).Success)
        Assert.IsTrue(isOdd individualNumber)

    [<TestMethod>]
    member __.``return a correct SSN for Finnish female`` () =
        let country = Country.Finland
        let birthdate = DateTime(1977, 2, 15)
        let gender = Gender.Female
        let random = getRandom false 100
        let ssn = generateSSN random country birthdate gender false false

        let d = ssn.Substring(0, 2)
        let m = ssn.Substring(2, 2)
        let y = ssn.Substring(4, 2)
        let centurySign = ssn.Substring(FinnishSSNParameters.CenturySignStart, FinnishSSNParameters.CenturySignLength)
        let individualNumber = Convert.ToInt32(ssn.Substring(FinnishSSNParameters.IndividualNumberStart,
                                                             FinnishSSNParameters.IndividualNumberLength))
        let checksum = ssn.Substring(FinnishSSNParameters.ChecksumStart, FinnishSSNParameters.ChecksumLength)
        let checksumPattern = "^(\d|[A-Y])$"
        let checksumRegex = Regex checksumPattern

        Assert.AreEqual(FinnishSSNParameters.SsnLength, ssn.Length)
        Assert.AreEqual("15", d)
        Assert.AreEqual("02", m)
        Assert.AreEqual("77", y)
        Assert.AreEqual("-", centurySign)
        Assert.IsTrue(002 <= individualNumber && individualNumber <= 899)
        Assert.IsTrue((checksumRegex.Match checksum).Success)
        Assert.IsTrue(isEven individualNumber)

    [<TestMethod>]
    member __.``return a correct SSN for Icelandic male`` () =
        let country = Country.Iceland
        let birthdate = DateTime(1945, 10, 28)
        let gender = Gender.Male
        let random = getRandom false 100
        let ssn = generateSSN random country birthdate gender false false

        let d = ssn.Substring(0, 2)
        let m = ssn.Substring(2, 2)
        let y = ssn.Substring(4, 2)
        let individualNumber = Convert.ToInt32(ssn.Substring(IcelandicSSNParameters.IndividualNumberStart,
                                                             IcelandicSSNParameters.IndividualNumberLength))
        let checksum = Convert.ToInt32(ssn.Substring(IcelandicSSNParameters.ChecksumStart, IcelandicSSNParameters.ChecksumLength))
        let centurySign = ssn.Substring(IcelandicSSNParameters.CenturySignStart, IcelandicSSNParameters.CenturySignLength)

        Assert.AreEqual(IcelandicSSNParameters.SsnLength, ssn.Length)
        Assert.AreEqual("28", d)
        Assert.AreEqual("10", m)
        Assert.AreEqual("45", y)
        Assert.IsTrue(20 <= individualNumber && individualNumber < 100)
        Assert.IsTrue(0 <= checksum && checksum <= 9)
        Assert.AreEqual("9", centurySign)

    [<TestMethod>]
    member __.``return a correct SSN for Icelandic female`` () =
        let country = Country.Iceland
        let birthdate = DateTime(1912, 1, 2)
        let gender = Gender.Female
        let random = getRandom false 100
        let ssn = generateSSN random country birthdate gender false false

        let d = ssn.Substring(0, 2)
        let m = ssn.Substring(2, 2)
        let y = ssn.Substring(4, 2)
        let individualNumber = Convert.ToInt32(ssn.Substring(IcelandicSSNParameters.IndividualNumberStart,
                                                             IcelandicSSNParameters.IndividualNumberLength))
        let checksum = Convert.ToInt32(ssn.Substring(IcelandicSSNParameters.ChecksumStart, IcelandicSSNParameters.ChecksumLength))
        let centurySign = ssn.Substring(IcelandicSSNParameters.CenturySignStart, IcelandicSSNParameters.CenturySignLength)

        Assert.AreEqual(IcelandicSSNParameters.SsnLength, ssn.Length)
        Assert.AreEqual("02", d)
        Assert.AreEqual("01", m)
        Assert.AreEqual("12", y)
        Assert.IsTrue(20 <= individualNumber && individualNumber < 100)
        Assert.IsTrue(0 <= checksum && checksum <= 9)
        Assert.AreEqual("9", centurySign)

    [<TestMethod>]
    member __.``return a correct SSN for Norwegian male`` () =
        let country = Country.Norway
        let birthdate = DateTime(1954, 3, 21)
        let gender = Gender.Male
        let random = getRandom false 100
        let ssn = generateSSN random country birthdate gender false false

        let d = ssn.Substring(0, 2)
        let m = ssn.Substring(2, 2)
        let y = ssn.Substring(4, 2)
        let individualNumber = Convert.ToInt32(ssn.Substring(NorwegianSSNParameters.IndividualNumberStart,
                                                             NorwegianSSNParameters.IndividualNumberLength))
        let checksum = Convert.ToInt32(ssn.Substring(NorwegianSSNParameters.ChecksumStart, NorwegianSSNParameters.ChecksumLength))

        Assert.AreEqual(NorwegianSSNParameters.SsnLength, ssn.Length)
        Assert.AreEqual("21", d)
        Assert.AreEqual("03", m)
        Assert.AreEqual("54", y)
        Assert.IsTrue(0 <= individualNumber && individualNumber <= 499)
        Assert.IsTrue(0 <= checksum && checksum <= 99)
        Assert.IsTrue(isOdd individualNumber)

    [<TestMethod>]
    member __.``return a correct SSN for Norwegian female`` () =
        let country = Country.Norway
        let birthdate = DateTime(1990, 5, 14)
        let gender = Gender.Female
        let random = getRandom false 100
        let ssn = generateSSN random country birthdate gender false false

        let d = ssn.Substring(0, 2)
        let m = ssn.Substring(2, 2)
        let y = ssn.Substring(4, 2)
        let individualNumber = Convert.ToInt32(ssn.Substring(NorwegianSSNParameters.IndividualNumberStart,
                                                             NorwegianSSNParameters.IndividualNumberLength))
        let checksum = Convert.ToInt32(ssn.Substring(NorwegianSSNParameters.ChecksumStart, NorwegianSSNParameters.ChecksumLength))

        Assert.AreEqual(NorwegianSSNParameters.SsnLength, ssn.Length)
        Assert.AreEqual("14", d)
        Assert.AreEqual("05", m)
        Assert.AreEqual("90", y)
        Assert.IsTrue(0 <= individualNumber && individualNumber <= 499)
        Assert.IsTrue(0 <= checksum && checksum <= 99)
        Assert.IsTrue(isEven individualNumber)

    [<TestMethod>]
    member __.``return a correct SSN for Swedish male`` () =
        let country = Country.Sweden
        let birthdate = DateTime(1952, 12, 25)
        let gender = Gender.Male
        let random = getRandom false 100
        let ssn = generateSSN random country birthdate gender false false

        let y = ssn.Substring(0, 2)
        let m = ssn.Substring(2, 2)
        let d = ssn.Substring(4, 2)
        
        let individualNumber = Convert.ToInt32(ssn.Substring(SwedishSSNParameters.oldSsnParams.IndividualNumberStart,
                                                             SwedishSSNParameters.oldSsnParams.IndividualNumberLength))
        let checksum = Convert.ToInt32(ssn.Substring(SwedishSSNParameters.oldSsnParams.ChecksumStart, SwedishSSNParameters.oldSsnParams.ChecksumLength))

        Assert.AreEqual(SwedishSSNParameters.oldSsnParams.SsnLength, ssn.Length)
        Assert.AreEqual("25", d)
        Assert.AreEqual("12", m)
        Assert.AreEqual("52", y)
        Assert.IsTrue(0 <= individualNumber && individualNumber <= 999)
        Assert.IsTrue(0 <= checksum && checksum <= 9)
        Assert.IsTrue(isOdd individualNumber)

    [<TestMethod>]
    member __.``return a correct SSN for Swedish female`` () =
        let country = Country.Sweden
        let birthdate = DateTime(1982, 11, 30)
        let gender = Gender.Female
        let random = getRandom false 100
        let ssn = generateSSN random country birthdate gender false false

        let y = ssn.Substring(0, 2)
        let m = ssn.Substring(2, 2)
        let d = ssn.Substring(4, 2)
        
        let individualNumber = Convert.ToInt32(ssn.Substring(SwedishSSNParameters.oldSsnParams.IndividualNumberStart,
                                                             SwedishSSNParameters.oldSsnParams.IndividualNumberLength))
        let checksum = Convert.ToInt32(ssn.Substring(SwedishSSNParameters.oldSsnParams.ChecksumStart, SwedishSSNParameters.oldSsnParams.ChecksumLength))

        Assert.AreEqual(SwedishSSNParameters.oldSsnParams.SsnLength, ssn.Length)
        Assert.AreEqual("30", d)
        Assert.AreEqual("11", m)
        Assert.AreEqual("82", y)
        Assert.IsTrue(0 <= individualNumber && individualNumber <= 999)
        Assert.IsTrue(0 <= checksum && checksum <= 9)
        Assert.IsTrue(isEven individualNumber)
