namespace Tests

open System
open Microsoft.VisualStudio.TestTools.UnitTesting
open RandomPersonLib
open Util
open MathUtil
open StringUtil
open DenmarkSSNParameters
open DenmarkSSNGeneration

[<TestClass>]
type ``numberFor1937to1999 should`` () =

    let random = getRandom false 100

    [<TestMethod>]
    member __.``return either 0, 1, 2, 3, 4 or 9`` () =
        let number = numberFor1937to1999 random
        Assert.IsTrue((0 <= number && number <= 4) || number = 9)
        Assert.AreNotEqual(10, number) // negative test

[<TestClass>]
type ``numberFor2000to2036 should`` () =

    let random = getRandom false 100

    [<TestMethod>]
    member __.``return either 4, 5, 6, 7, 8, 9`` () =
        let number = numberFor2000to2036 random
        Assert.IsTrue((4 <= number && number <= 9))
        Assert.AreNotEqual(10, number) // negative test

[<TestClass>]
type ``getCenturyNumber should`` () =

    let random = getRandom false 100

    [<TestMethod>]
    member __.``return 0, 1, 2, 3, 4 or 9 for year 1940`` () =
        let centuryNumber = getCenturyNumber random 1940
        Assert.IsTrue((0 <= centuryNumber && centuryNumber <= 4) || centuryNumber = 9)
        Assert.AreNotEqual(10, centuryNumber) // negative test

    [<TestMethod>]
    member __.``return 0, 1, 2, 3, 4 or 9 for year 1980`` () =
        let centuryNumber = getCenturyNumber random 1980
        Assert.IsTrue((0 <= centuryNumber && centuryNumber <= 4) || centuryNumber = 9)
        Assert.AreNotEqual(10, centuryNumber) // negative test

    [<TestMethod>]
    member __.``return 0, 1, 2 or 3 for year 1922`` () =
        let centuryNumber = getCenturyNumber random 1922
        Assert.IsTrue(0 <= centuryNumber && centuryNumber <= 3)
        Assert.AreNotEqual(10, centuryNumber) // negative test

    [<TestMethod>]
    member __.``return 5, 6, 7 or 8 for year 1878`` () =
        let centuryNumber = getCenturyNumber random 1878
        Assert.IsTrue(5 <= centuryNumber && centuryNumber <= 8)
        Assert.AreNotEqual(10, centuryNumber) // negative test

    [<TestMethod>]
    member __.``return 4, 5, 6, 7, 8 or 9 for year 2002`` () =
        let centuryNumber = getCenturyNumber random 2002
        Assert.IsTrue(4 <= centuryNumber && centuryNumber <= 9)
        Assert.AreNotEqual(10, centuryNumber) // negative test

    [<TestMethod>]
    member __.``return 5, 6, 7 or 8 for year 2040`` () =
        let centuryNumber = getCenturyNumber random 2040
        Assert.IsTrue(5 <= centuryNumber && centuryNumber <= 8)
        Assert.AreNotEqual(10, centuryNumber) // negative test

[<TestClass>]
type ``generateDanishIndividualNumber should`` () =

    let random = getRandom false 100

    [<TestMethod>]
    member __.``return a number between 100 and 200 when century number is 1`` () =
        let individualNumber = generateIndividualNumber random 1
        let individualNumberAsInt = int individualNumber
        Assert.IsTrue(100 <= individualNumberAsInt && individualNumberAsInt < 200)

[<TestClass>]
type ``generateDanishChecksum should`` () =

    let random = getRandom false 100

    [<TestMethod>]
    member __.``return a correct checksum for 041285-3040`` () =
        let birthdate = DateTime(1985, 12, 04)
        let individualNumber = "304"
        let checksum = generateChecksum random birthdate individualNumber
        Assert.AreEqual("0", checksum)

    [<TestMethod>]
    member __.``return a correct checksum for 090357-6855`` () =
        let birthdate = DateTime(1957, 03, 09)
        let individualNumber = "685"
        let checksum = generateChecksum random birthdate individualNumber
        Assert.AreEqual("5", checksum)

    [<TestMethod>]
    member __.``return a correct checksum for 180282-1804`` () =
        let birthdate = DateTime(1982, 02, 18)
        let individualNumber = "180"
        let checksum = generateChecksum random birthdate individualNumber
        Assert.AreEqual("4", checksum)

[<TestClass>]
type ``anonymizeSSN for Danish SSNs should`` () =

    [<TestMethod>]
    member __.``return 120388-12345 when given 120388-11345`` () =
        let ssn = "120388-11345"
        let fake = anonymizeSSN ssn

        Assert.AreEqual("120388-12345", fake)

    [<TestMethod>]
    member __.``return 120388-11345 when given 120388-10345`` () =
        let ssn = "120388-10345"
        let fake = anonymizeSSN ssn

        Assert.AreEqual("120388-11345", fake)

    [<TestMethod>]
    member __.``return 120388-11345 when given 120388-19345`` () =
        let ssn = "120388-19345"
        let fake = anonymizeSSN ssn

        Assert.AreEqual("120388-10345", fake)

[<TestClass>]
type ``generateDanishSSN should`` () =

    let random = getRandom false 100

    [<TestMethod>]
    member __.``return a correct SSN for male 1`` () =
        let birthdate = DateTime(1985, 12, 04)
        let gender = Gender.Male
        let ssn = generateDanishSSN random birthdate gender false

        let d = ssn |> substring 0 2
        let m = ssn |> substring 2 2
        let y = ssn |> substring 4 2
        let individualNumber = ssn |> substring IndividualNumberStart IndividualNumberLength |> int
        let checksum =         ssn |> substring ChecksumStart         ChecksumLength         |> int

        Assert.AreEqual(SsnLength, ssn.Length)
        Assert.AreEqual("04", d)
        Assert.AreEqual("12", m)
        Assert.AreEqual("85", y)
        Assert.IsTrue(0 <= individualNumber && individualNumber <= 999)
        Assert.IsTrue(0 <= checksum && checksum <= 9)
        Assert.IsTrue(isOdd checksum)

    [<TestMethod>]
    member __.``return a correct SSN for male 2`` () =
        let birthdate = DateTime(1952, 2, 6)
        let gender = Gender.Male
        let ssn = generateDanishSSN random birthdate gender false

        let d = ssn |> substring 0 2
        let m = ssn |> substring 2 2
        let y = ssn |> substring 4 2
        let individualNumber = ssn |> substring IndividualNumberStart IndividualNumberLength |> int
        let checksum =         ssn |> substring ChecksumStart         ChecksumLength         |> int

        Assert.AreEqual(SsnLength, ssn.Length)
        Assert.AreEqual("06", d)
        Assert.AreEqual("02", m)
        Assert.AreEqual("52", y)
        Assert.IsTrue(0 <= individualNumber && individualNumber <= 999)
        Assert.IsTrue(0 <= checksum && checksum <= 9)
        Assert.IsTrue(isOdd checksum)

    [<TestMethod>]
    member __.``return a correct SSN for female 1`` () =
        let birthdate = DateTime(2000, 9, 15)
        let gender = Gender.Female
        let ssn = generateDanishSSN random birthdate gender false

        let d = ssn |> substring 0 2
        let m = ssn |> substring 2 2
        let y = ssn |> substring 4 2
        let individualNumber = ssn |> substring IndividualNumberStart IndividualNumberLength |> int
        let checksum =         ssn |> substring ChecksumStart         ChecksumLength         |> int

        Assert.AreEqual(SsnLength, ssn.Length)
        Assert.AreEqual("15", d)
        Assert.AreEqual("09", m)
        Assert.AreEqual("00", y)
        Assert.IsTrue(0 <= individualNumber && individualNumber <= 999)
        Assert.IsTrue(0 <= checksum && checksum <= 9)
        Assert.IsTrue(isEven checksum)

    [<TestMethod>]
    member __.``return a correct SSN for female 2`` () =
        let birthdate = DateTime(1999, 1, 1)
        let gender = Gender.Female
        let ssn = generateDanishSSN random birthdate gender false

        let d = ssn |> substring 0 2
        let m = ssn |> substring 2 2
        let y = ssn |> substring 4 2
        let individualNumber = ssn |> substring IndividualNumberStart IndividualNumberLength |> int
        let checksum =         ssn |> substring ChecksumStart         ChecksumLength         |> int

        Assert.AreEqual(SsnLength, ssn.Length)
        Assert.AreEqual("01", d)
        Assert.AreEqual("01", m)
        Assert.AreEqual("99", y)
        Assert.IsTrue(0 <= individualNumber && individualNumber <= 999)
        Assert.IsTrue(0 <= checksum && checksum <= 9)
        Assert.IsTrue(isEven checksum)

    [<TestMethod>]
    member __.``return an incorrect SSN for female when asking for fake SSN`` () =
        let birthdate = DateTime(2000, 9, 15)
        let gender = Gender.Female
        let ssnReal = generateDanishSSN random birthdate gender false
        let ssnFake = generateDanishSSN random birthdate gender true

        let validatePerson = ValidatePerson()
        let isRealValidating = validatePerson.ValidateSSN(Country.Denmark, ssnReal)
        let isFakeValidating = validatePerson.ValidateSSN(Country.Denmark, ssnFake)

        let d = ssnFake |> substring 0 2
        let m = ssnFake |> substring 2 2
        let y = ssnFake |> substring 4 2
        let individualNumber = ssnFake |> substring IndividualNumberStart IndividualNumberLength |> int
        let checksum =         ssnFake |> substring ChecksumStart         ChecksumLength         |> int

        Assert.AreEqual(true,  isRealValidating)
        Assert.AreEqual(false, isFakeValidating)
        Assert.AreNotEqual(ssnReal, ssnFake)
        Assert.AreEqual(SsnLength, ssnFake.Length)
        Assert.AreEqual("15", d)
        Assert.AreEqual("09", m)
        Assert.AreEqual("00", y)
        Assert.IsTrue(0 <= individualNumber && individualNumber <= 999)
        Assert.IsTrue(0 <= checksum && checksum <= 9)
        Assert.IsTrue(isEven checksum)
