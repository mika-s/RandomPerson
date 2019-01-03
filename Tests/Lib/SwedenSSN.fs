namespace Tests

open System
open Microsoft.VisualStudio.TestTools.UnitTesting
open RandomPersonLib
open Util
open SwedenSSNGeneration

[<TestClass>]
type ``getIndividualNumber for Swedish SSNs should`` () =

    let random = getRandom false 100

    [<TestMethod>]
    member __.``return between 0 and 999, 1`` () =
        let individualNumber = getIndividualNumber random
        Assert.IsTrue(0 <= individualNumber && individualNumber < 999)
        Assert.IsFalse(individualNumber > 1000) // negative test

    [<TestMethod>]
    member __.``return between 0 and 999, 2`` () =
        let individualNumber = getIndividualNumber random
        Assert.IsTrue(0 <= individualNumber && individualNumber < 999)

    [<TestMethod>]
    member __.``return between 0 and 999, 3`` () =
        let individualNumber = getIndividualNumber random
        Assert.IsTrue(0 <= individualNumber && individualNumber < 999)

[<TestClass>]
type ``getIndividualNumberMale for Swedish SSNs should`` () =

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
    member __.``not return a number between 000 and 999`` () =
        let individualNumber = getIndividualNumberMale random
        Assert.IsTrue(0 <= individualNumber && individualNumber <= 999)

[<TestClass>]
type ``getIndividualNumberFemale for Swedish SSNs should`` () =

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
    member __.``not return a number between 000 and 999`` () =
        let individualNumber = getIndividualNumberFemale random
        Assert.IsTrue(0 <= individualNumber && individualNumber <= 999)


[<TestClass>]
type ``generateSwedishIndividualNumber for Swedish SSNs should`` () =

    let random = getRandom false 100

    [<TestMethod>]
    member __.``return an odd number when male`` () =
        let individualNumber = generateIndividualNumber random Gender.Male
        let individualNumberAsInt = int (individualNumber)
        Assert.IsTrue(isOdd individualNumberAsInt)

[<TestClass>]
type ``generateChecksum for Swedish SSNs should`` () =

    [<TestMethod>]
    member __.``return a correct checksum for 811228`` () =
        let numbersStr = "811228987"
        let checksum = generateChecksum numbersStr
        Assert.AreEqual("4", checksum)

    [<TestMethod>]
    member __.``Should return a correct checksum for 670919`` () =
        let numbersStr = "670919953"
        let checksum = generateChecksum numbersStr
        Assert.AreEqual("0", checksum)

[<TestClass>]
type ``anonymizeSSN for Swedish SSNs should`` () =

    [<TestMethod>]
    member __.``return 980217-1234 when given 980217-1134`` () =
        let ssn = "980217-1134"
        let fake = anonymizeSSN ssn

        Assert.AreEqual("980217-1234", fake)

    [<TestMethod>]
    member __.``return 980217-1134 when given 980217-1034`` () =
        let ssn = "980217-1034"
        let fake = anonymizeSSN ssn

        Assert.AreEqual("980217-1134", fake)

    [<TestMethod>]
    member __.``return 980217-1134 when given 980217-1934`` () =
        let ssn = "980217-1934"
        let fake = anonymizeSSN ssn

        Assert.AreEqual("980217-1034", fake)

[<TestClass>]
type ``generateSwedishSSN should`` () =

    let random = getRandom false 100

    [<TestMethod>]
    member __.``return a correct SSN for male 1`` () =
        let birthdate = DateTime(1985, 12, 04)
        let gender = Gender.Male
        let ssn = generateSwedishSSN random birthdate gender false

        let y = ssn.Substring(0, 2)
        let m = ssn.Substring(2, 2)
        let d = ssn.Substring(4, 2)
        let individualNumber = int (ssn.Substring(7, 3))
        let checksum = int (ssn.Substring(10, 1))

        Assert.AreEqual(11, ssn.Length)
        Assert.AreEqual("04", d)
        Assert.AreEqual("12", m)
        Assert.AreEqual("85", y)
        Assert.IsTrue(0 <= individualNumber && individualNumber <= 999)
        Assert.IsTrue(0 <= checksum && checksum <= 9)
        Assert.IsTrue(isOdd individualNumber)

    [<TestMethod>]
    member __.``return a correct SSN for male 2`` () =
        let birthdate = DateTime(1952, 2, 6)
        let gender = Gender.Male
        let ssn = generateSwedishSSN random birthdate gender false

        let y = ssn.Substring(0, 2)
        let m = ssn.Substring(2, 2)
        let d = ssn.Substring(4, 2)
        let individualNumber = int (ssn.Substring(7, 3))
        let checksum = int (ssn.Substring(10, 1))

        Assert.AreEqual(11, ssn.Length)
        Assert.AreEqual("06", d)
        Assert.AreEqual("02", m)
        Assert.AreEqual("52", y)
        Assert.IsTrue(0 <= individualNumber && individualNumber <= 999)
        Assert.IsTrue(0 <= checksum && checksum <= 9)
        Assert.IsTrue(isOdd individualNumber)

    [<TestMethod>]
    member __.``return a correct SSN for female 1`` () =
        let birthdate = DateTime(2000, 9, 15)
        let gender = Gender.Female
        let ssn = generateSwedishSSN random birthdate gender false

        let y = ssn.Substring(0, 2)
        let m = ssn.Substring(2, 2)
        let d = ssn.Substring(4, 2)
        let individualNumber = int (ssn.Substring(7, 3))
        let checksum = int (ssn.Substring(10, 1))

        Assert.AreEqual(11, ssn.Length)
        Assert.AreEqual("15", d)
        Assert.AreEqual("09", m)
        Assert.AreEqual("00", y)
        Assert.IsTrue(0 <= individualNumber && individualNumber <= 999)
        Assert.IsTrue(0 <= checksum && checksum <= 9)
        Assert.IsTrue(isEven individualNumber)

    [<TestMethod>]
    member __.``return a correct SSN for female 2`` () =
        let birthdate = DateTime(1999, 1, 1)
        let gender = Gender.Female
        let ssn = generateSwedishSSN random birthdate gender false

        let y = ssn.Substring(0, 2)
        let m = ssn.Substring(2, 2)
        let d = ssn.Substring(4, 2)
        let individualNumber = int (ssn.Substring(7, 3))
        let checksum = int (ssn.Substring(10, 1))

        Assert.AreEqual(11, ssn.Length)
        Assert.AreEqual("01", d)
        Assert.AreEqual("01", m)
        Assert.AreEqual("99", y)
        Assert.IsTrue(0 <= individualNumber && individualNumber <= 999)
        Assert.IsTrue(0 <= checksum && checksum <= 9)
        Assert.IsTrue(isEven individualNumber)

    [<TestMethod>]
    member __.``return an incorrect SSN for female when asking for fake SSN`` () =
        let birthdate = DateTime(1999, 1, 1)
        let gender = Gender.Female
        let ssnReal = generateSwedishSSN random birthdate gender false
        let ssnFake = generateSwedishSSN random birthdate gender true

        let validatePerson = ValidatePerson()
        let isRealValidating = validatePerson.ValidateSSN(Country.Sweden, ssnReal)
        let isFakeValidating = validatePerson.ValidateSSN(Country.Sweden, ssnFake)

        let y = ssnFake.Substring(0, 2)
        let m = ssnFake.Substring(2, 2)
        let d = ssnFake.Substring(4, 2)
        let individualNumber = int (ssnFake.Substring(7, 3))
        let checksum = int (ssnFake.Substring(10, 1))

        Assert.AreEqual(true,  isRealValidating)
        Assert.AreEqual(false, isFakeValidating)
        Assert.AreNotEqual(ssnReal, ssnFake)
        Assert.AreEqual(11, ssnFake.Length)
        Assert.AreEqual("01", d)
        Assert.AreEqual("01", m)
        Assert.AreEqual("99", y)
        Assert.IsTrue(0 <= individualNumber && individualNumber <= 999)
        Assert.IsTrue(0 <= checksum && checksum <= 9)
        Assert.IsTrue(isEven individualNumber)
