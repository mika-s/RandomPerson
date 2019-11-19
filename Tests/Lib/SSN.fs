namespace Tests

open System
open System.Text.RegularExpressions
open Microsoft.VisualStudio.TestTools.UnitTesting
open RandomPersonLib
open Util
open MathUtil
open StringUtil
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

        let d = ssn |> substring 0 2
        let m = ssn |> substring 2 2
        let y = ssn |> substring 4 2
        let individualNumber = ssn |> substring DenmarkSSNParameters.IndividualNumberStart DenmarkSSNParameters.IndividualNumberLength |> int
        let checksum         = ssn |> substring DenmarkSSNParameters.ChecksumStart         DenmarkSSNParameters.ChecksumLength         |> int

        Assert.AreEqual(DenmarkSSNParameters.SsnLength, ssn.Length)
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

        let d = ssn |> substring 0 2
        let m = ssn |> substring 2 2
        let y = ssn |> substring 4 2
        let individualNumber = ssn |> substring DenmarkSSNParameters.IndividualNumberStart DenmarkSSNParameters.IndividualNumberLength |> int
        let checksum         = ssn |> substring DenmarkSSNParameters.ChecksumStart         DenmarkSSNParameters.ChecksumLength         |> int

        Assert.AreEqual(DenmarkSSNParameters.SsnLength, ssn.Length)
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

        let individualNumber = ssn |> substring NetherlandsSSNParameters.IndividualNumberStart NetherlandsSSNParameters.IndividualNumberLength |> int
        let checksum         = ssn |> substring NetherlandsSSNParameters.ChecksumStart         NetherlandsSSNParameters.ChecksumLength         |> int

        Assert.AreEqual(NetherlandsSSNParameters.SsnLength, ssn.Length)
        Assert.IsTrue(0 <= individualNumber && individualNumber <= 99999999)
        Assert.IsTrue(0 <= checksum && checksum <= 9)

    [<TestMethod>]
    member __.``return a correct SSN for Finnish male`` () =
        let country = Country.Finland
        let birthdate = DateTime(1925, 8, 7)
        let gender = Gender.Male
        let random = getRandom false 100
        let ssn = generateSSN random country birthdate gender false false

        let d = ssn |> substring 0 2
        let m = ssn |> substring 2 2
        let y = ssn |> substring 4 2
        let centurySign      = ssn |> substring FinlandSSNParameters.CenturySignStart      FinlandSSNParameters.CenturySignLength
        let individualNumber = ssn |> substring FinlandSSNParameters.IndividualNumberStart FinlandSSNParameters.IndividualNumberLength |> int
        let checksum         = ssn |> substring FinlandSSNParameters.ChecksumStart         FinlandSSNParameters.ChecksumLength

        let checksumPattern = "^(\d|[A-Y])$"
        let checksumRegex = Regex checksumPattern

        Assert.AreEqual(FinlandSSNParameters.SsnLength, ssn.Length)
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

        let d = ssn |> substring 0 2
        let m = ssn |> substring 2 2
        let y = ssn |> substring 4 2
        let centurySign      = ssn |> substring FinlandSSNParameters.CenturySignStart      FinlandSSNParameters.CenturySignLength
        let individualNumber = ssn |> substring FinlandSSNParameters.IndividualNumberStart FinlandSSNParameters.IndividualNumberLength |> int
        let checksum         = ssn |> substring FinlandSSNParameters.ChecksumStart         FinlandSSNParameters.ChecksumLength

        let checksumPattern = "^(\d|[A-Y])$"
        let checksumRegex = Regex checksumPattern

        Assert.AreEqual(FinlandSSNParameters.SsnLength, ssn.Length)
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

        let d = ssn |> substring 0 2
        let m = ssn |> substring 2 2
        let y = ssn |> substring 4 2
        let individualNumber = ssn |> substring IcelandSSNParameters.IndividualNumberStart IcelandSSNParameters.IndividualNumberLength |> int
        let checksum         = ssn |> substring IcelandSSNParameters.ChecksumStart         IcelandSSNParameters.ChecksumLength         |> int
        let centurySign      = ssn |> substring IcelandSSNParameters.CenturySignStart      IcelandSSNParameters.CenturySignLength

        Assert.AreEqual(IcelandSSNParameters.SsnLength, ssn.Length)
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

        let d = ssn |> substring 0 2
        let m = ssn |> substring 2 2
        let y = ssn |> substring 4 2
        let individualNumber = ssn |> substring IcelandSSNParameters.IndividualNumberStart IcelandSSNParameters.IndividualNumberLength |> int
        let checksum         = ssn |> substring IcelandSSNParameters.ChecksumStart         IcelandSSNParameters.ChecksumLength         |> int
        let centurySign      = ssn |> substring IcelandSSNParameters.CenturySignStart      IcelandSSNParameters.CenturySignLength

        Assert.AreEqual(IcelandSSNParameters.SsnLength, ssn.Length)
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

        let d = ssn |> substring 0 2
        let m = ssn |> substring 2 2
        let y = ssn |> substring 4 2
        let individualNumber = ssn |> substring NorwaySSNParameters.IndividualNumberStart NorwaySSNParameters.IndividualNumberLength |> int
        let checksum         = ssn |> substring NorwaySSNParameters.ChecksumStart         NorwaySSNParameters.ChecksumLength         |> int

        Assert.AreEqual(NorwaySSNParameters.SsnLength, ssn.Length)
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

        let d = ssn |> substring 0 2
        let m = ssn |> substring 2 2
        let y = ssn |> substring 4 2
        let individualNumber = ssn |> substring NorwaySSNParameters.IndividualNumberStart NorwaySSNParameters.IndividualNumberLength |> int
        let checksum         = ssn |> substring NorwaySSNParameters.ChecksumStart         NorwaySSNParameters.ChecksumLength         |> int

        Assert.AreEqual(NorwaySSNParameters.SsnLength, ssn.Length)
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

        let y = ssn |> substring 0 2
        let m = ssn |> substring 2 2
        let d = ssn |> substring 4 2
        
        let individualNumber = ssn |> substring SwedenSSNParameters.oldSsnParams.IndividualNumberStart SwedenSSNParameters.oldSsnParams.IndividualNumberLength |> int
        let checksum         = ssn |> substring SwedenSSNParameters.oldSsnParams.ChecksumStart         SwedenSSNParameters.oldSsnParams.ChecksumLength         |> int

        Assert.AreEqual(SwedenSSNParameters.oldSsnParams.SsnLength, ssn.Length)
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

        let y = ssn |> substring 0 2
        let m = ssn |> substring 2 2
        let d = ssn |> substring 4 2
        
        let individualNumber = ssn |> substring SwedenSSNParameters.oldSsnParams.IndividualNumberStart SwedenSSNParameters.oldSsnParams.IndividualNumberLength |> int
        let checksum         = ssn |> substring SwedenSSNParameters.oldSsnParams.ChecksumStart         SwedenSSNParameters.oldSsnParams.ChecksumLength         |> int

        Assert.AreEqual(SwedenSSNParameters.oldSsnParams.SsnLength, ssn.Length)
        Assert.AreEqual("30", d)
        Assert.AreEqual("11", m)
        Assert.AreEqual("82", y)
        Assert.IsTrue(0 <= individualNumber && individualNumber <= 999)
        Assert.IsTrue(0 <= checksum && checksum <= 9)
        Assert.IsTrue(isEven individualNumber)
