namespace Tests

open System
open System.Text.RegularExpressions
open Microsoft.VisualStudio.TestTools.UnitTesting
open RandomPersonLib
open Util
open FinlandSSNParameters
open FinlandSSNGeneration

[<TestClass>]
type ``getIndividualNumber for Finnish SSNs should`` () =

    let random = getRandom false 100

    [<TestMethod>]
    member __.``return between 002 and 899`` () =
        let individualNumber = getIndividualNumber random
        Assert.IsTrue(002 <= individualNumber && individualNumber < 899)
        Assert.IsFalse(individualNumber > 1000) // negative test

[<TestClass>]
type ``getIndividualNumberMale for Finnish SSNs should`` () =

    let random = getRandom false 100

    [<TestMethod>]
    member __.``return an odd number`` () =
        let individualNumber = getIndividualNumberMale random
        Assert.IsTrue(isOdd individualNumber)

    [<TestMethod>]
    member __.``not return an even number`` () =
        let individualNumber = getIndividualNumberMale random
        Assert.IsFalse(isEven individualNumber)

    [<TestMethod>]
    member __.``return a number between 002 and 899`` () =
        let individualNumber = getIndividualNumberMale random
        Assert.IsTrue(002 <= individualNumber && individualNumber <= 899)

[<TestClass>]
type ``getIndividualNumberFemale for Finnish SSNs should`` () =

    let random = getRandom false 100

    [<TestMethod>]
    member __.``return an even number`` () =
        let individualNumber = getIndividualNumberFemale random
        Assert.IsTrue(isEven individualNumber)

    [<TestMethod>]
    member __.``not return an odd number`` () =
        let individualNumber = getIndividualNumberFemale random
        Assert.IsFalse(isOdd individualNumber)

    [<TestMethod>]
    member __.``return a number between 002 and 899`` () =
        let individualNumber = getIndividualNumberFemale random
        Assert.IsTrue(002 <= individualNumber && individualNumber <= 899)


[<TestClass>]
type ``generateIndividualNumber for Finnish SSNs should`` () =

    let random = getRandom false 100

    [<TestMethod>]
    member __.``return an odd number when male`` () =
        let individualNumber = generateIndividualNumber random Gender.Male
        let individualNumberAsInt = int individualNumber
        Assert.IsTrue(isOdd individualNumberAsInt)

[<TestClass>]
type ``generateChecksum for Finnish SSNs should`` () =

    [<TestMethod>]
    member __.``return a correct checksum for 311280-888Y`` () =
        let birthdate = DateTime(1980, 12, 31)
        let individualNumber = "888"
        let checksum = generateChecksum birthdate individualNumber
        Assert.AreEqual("Y", checksum)

    [<TestMethod>]
    member __.``return a correct checksum for 241134-008C`` () =
        let birthdate = DateTime(1934, 11, 24)
        let individualNumber = "008"
        let checksum = generateChecksum birthdate individualNumber
        Assert.AreEqual("C", checksum)

    [<TestMethod>]
    member __.``return a correct checksum for 311280-999J`` () =
        let birthdate = DateTime(1980, 12, 31)
        let individualNumber = "999"
        let checksum = generateChecksum birthdate individualNumber
        Assert.AreEqual("J", checksum)

    [<TestMethod>]
    member __.``return a correct checksum for 131052-308T`` () =
        let birthdate = DateTime(1952, 10, 13)
        let individualNumber = "308"
        let checksum = generateChecksum birthdate individualNumber
        Assert.AreEqual("T", checksum)

    [<TestMethod>]
    member __.``return a correct checksum for 290296-7808`` () =
        let birthdate = DateTime(1996, 02, 29)
        let individualNumber = "780"
        let checksum = generateChecksum birthdate individualNumber
        Assert.AreEqual("8", checksum)

[<TestClass>]
type ``anonymizeSSN for Finnish SSNs should`` () =

    [<TestMethod>]
    member __.``return 311280-898T when given 311280-888T`` () =
        let ssn = "311280-888T"
        let fake = anonymizeSSN ssn

        Assert.AreEqual("311280-898T", fake)

    [<TestMethod>]
    member __.``return 131052-318T when given 131052-308T`` () =
        let ssn = "131052-308T"
        let fake = anonymizeSSN ssn

        Assert.AreEqual("131052-318T", fake)

    [<TestMethod>]
    member __.``return 311280-908T when given 311280-998T`` () =
        let ssn = "311280-998T"
        let fake = anonymizeSSN ssn

        Assert.AreEqual("311280-908T", fake)

[<TestClass>]
type ``generateFinnishSSN should`` () =

    let random = getRandom false 100

    let checksumPattern = "^(\d|[A-Y])$"
    let checksumRegex = Regex checksumPattern

    [<TestMethod>]
    member __.``return a correct SSN for male 1`` () =
        let birthdate = DateTime(1985, 12, 04)
        let gender = Gender.Male
        let ssn = generateFinnishSSN random birthdate gender false

        let d = ssn.Substring(0, 2)
        let m = ssn.Substring(2, 2)
        let y = ssn.Substring(4, 2)
        let centurySign = ssn.[CenturySignStart]
        let individualNumber = int (ssn.Substring(IndividualNumberStart, IndividualNumberLength))
        let checksum = ssn.Substring(ChecksumStart, ChecksumLength)

        Assert.AreEqual(SsnLength, ssn.Length)
        Assert.AreEqual("04", d)
        Assert.AreEqual("12", m)
        Assert.AreEqual("85", y)
        Assert.AreEqual('-', centurySign)
        Assert.IsTrue(002 <= individualNumber && individualNumber <= 899)
        Assert.IsTrue((checksumRegex.Match checksum).Success)
        Assert.IsTrue(isOdd individualNumber)

    [<TestMethod>]
    member __.``return a correct SSN for male 2`` () =
        let birthdate = DateTime(1952, 2, 6)
        let gender = Gender.Male
        let ssn = generateFinnishSSN random birthdate gender false

        let d = ssn.Substring(0, 2)
        let m = ssn.Substring(2, 2)
        let y = ssn.Substring(4, 2)
        let centurySign = ssn.[CenturySignStart]
        let individualNumber = int (ssn.Substring(IndividualNumberStart, IndividualNumberLength))
        let checksum = ssn.Substring(ChecksumStart, 1)

        Assert.AreEqual(SsnLength, ssn.Length)
        Assert.AreEqual("06", d)
        Assert.AreEqual("02", m)
        Assert.AreEqual("52", y)
        Assert.AreEqual('-', centurySign)
        Assert.IsTrue(002 <= individualNumber && individualNumber <= 899)
        Assert.IsTrue((checksumRegex.Match checksum).Success)
        Assert.IsTrue(isOdd individualNumber)

    [<TestMethod>]
    member __.``return a correct SSN for female 1`` () =
        let birthdate = DateTime(2000, 9, 15)
        let gender = Gender.Female
        let ssn = generateFinnishSSN random birthdate gender false

        let d = ssn.Substring(0, 2)
        let m = ssn.Substring(2, 2)
        let y = ssn.Substring(4, 2)
        let centurySign = ssn.[CenturySignStart]
        let individualNumber = int (ssn.Substring(IndividualNumberStart, IndividualNumberLength))
        let checksum = ssn.Substring(ChecksumStart, ChecksumLength)

        Assert.AreEqual(SsnLength, ssn.Length)
        Assert.AreEqual("15", d)
        Assert.AreEqual("09", m)
        Assert.AreEqual("00", y)
        Assert.AreEqual('A', centurySign)
        Assert.IsTrue(002 <= individualNumber && individualNumber <= 899)
        Assert.IsTrue((checksumRegex.Match checksum).Success)
        Assert.IsTrue(isEven individualNumber)

    [<TestMethod>]
    member __.``return a correct SSN for female 2`` () =
        let birthdate = DateTime(1850, 1, 1)
        let gender = Gender.Female
        let ssn = generateFinnishSSN random birthdate gender false

        let d = ssn.Substring(0, 2)
        let m = ssn.Substring(2, 2)
        let y = ssn.Substring(4, 2)
        let centurySign = ssn.[CenturySignStart]
        let individualNumber = int (ssn.Substring(IndividualNumberStart, IndividualNumberLength))
        let checksum = ssn.Substring(ChecksumStart, ChecksumLength)

        Assert.AreEqual(SsnLength, ssn.Length)
        Assert.AreEqual("01", d)
        Assert.AreEqual("01", m)
        Assert.AreEqual("50", y)
        Assert.AreEqual('+', centurySign)
        Assert.IsTrue(002 <= individualNumber && individualNumber <= 899)
        Assert.IsTrue((checksumRegex.Match checksum).Success)
        Assert.IsTrue(isEven individualNumber)

    [<TestMethod>]
    member __.``return an incorrect SSN for female when asking for fake SSN`` () =
        let birthdate = DateTime(1999, 1, 1)
        let gender = Gender.Female
        let ssnReal = generateFinnishSSN random birthdate gender false
        let ssnFake = generateFinnishSSN random birthdate gender true

        let randomPerson = RandomPerson()
        let isRealValidating = randomPerson.ValidateSSN(Country.Finland, ssnReal)
        let isFakeValidating = randomPerson.ValidateSSN(Country.Finland, ssnFake)

        let d = ssnFake.Substring(0, 2)
        let m = ssnFake.Substring(2, 2)
        let y = ssnFake.Substring(4, 2)
        let individualNumber = Convert.ToInt32(ssnFake.Substring(IndividualNumberStart, IndividualNumberLength))
        let checksum = ssnFake.Substring(ChecksumStart, ChecksumLength)

        Assert.AreEqual(true,  isRealValidating)
        Assert.AreEqual(false, isFakeValidating)
        Assert.AreNotEqual(ssnReal, ssnFake)
        Assert.AreEqual(SsnLength, ssnFake.Length)
        Assert.AreEqual("01", d)
        Assert.AreEqual("01", m)
        Assert.AreEqual("99", y)
        Assert.IsTrue(002 <= individualNumber && individualNumber <= 899)
        Assert.IsTrue((checksumRegex.Match checksum).Success)
        Assert.IsTrue(isEven individualNumber)
