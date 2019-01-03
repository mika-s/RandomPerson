namespace Tests

open System
open Microsoft.VisualStudio.TestTools.UnitTesting
open RandomPersonLib
open Util
open IcelandSSNParameters
open IcelandSSNGeneration

[<TestClass>]
type ``generateIndividualNumber for Icelandic SSNs should`` () =

    let random = getRandom false 100

    [<TestMethod>]
    member __.``return between 20 and 99`` () =
        let individualNumber = int (generateIndividualNumber random)
        Assert.IsTrue(20 <= individualNumber && individualNumber <= 99)
        Assert.IsFalse(individualNumber > 1000) // negative test

[<TestClass>]
type ``generateChecksum should`` () =

    [<TestMethod>]
    member __.``return a correct checksum for 310896-2099`` () =
        let birthdate = DateTime(1996, 08, 31)
        let individualNumber = "20"
        let checksum = generateChecksum birthdate individualNumber
        Assert.AreEqual("9", checksum)

    [<TestMethod>]
    member __.``return a correct checksum for 211085-8439`` () =
        let birthdate = DateTime(1985, 10, 21)
        let individualNumber = "84"
        let checksum = generateChecksum birthdate individualNumber
        Assert.AreEqual("3", checksum)

[<TestClass>]
type ``anonymizeSSN for Icelandic SSNs should`` () =

    [<TestMethod>]
    member __.``return 310896-2223 when given 310896-2123`` () =
        let ssn = "310896-2123"
        let fake = anonymizeSSN ssn

        Assert.AreEqual("310896-2223", fake)

    [<TestMethod>]
    member __.``return 2110881-0245 when given 211088-0145`` () =
        let ssn = "211088-0145"
        let fake = anonymizeSSN ssn

        Assert.AreEqual("211088-0245", fake)

    [<TestMethod>]
    member __.``return 211088-9045 when given 211088-9945`` () =
        let ssn = "211088-9945"
        let fake = anonymizeSSN ssn

        Assert.AreEqual("211088-9045", fake)

[<TestClass>]
type ``generateIcelandicSSN should`` () =

    let random = getRandom false 100

    [<TestMethod>]
    member __.``return a correct SSN 1`` () =
        let birthdate = DateTime(1996, 08, 31)

        let ssn = generateIcelandicSSN random birthdate false

        let d = ssn.Substring(0, 2)
        let m = ssn.Substring(2, 2)
        let y = ssn.Substring(4, 2)
        let individualNumber = int (ssn.Substring(IndividualNumberStart, IndividualNumberLength))
        let checksum = int (ssn.Substring(ChecksumStart, ChecksumLength))

        Assert.AreEqual(SsnLength, ssn.Length)
        Assert.AreEqual("31", d)
        Assert.AreEqual("08", m)
        Assert.AreEqual("96", y)
        Assert.IsTrue(20 <= individualNumber && individualNumber <= 99)
        Assert.IsTrue(0 <= checksum && checksum <= 9)

    [<TestMethod>]
    member __.``return a correct SSN 2`` () =
        let birthdate = DateTime(1952, 2, 6)
        let ssn = generateIcelandicSSN random birthdate false

        let d = ssn.Substring(0, 2)
        let m = ssn.Substring(2, 2)
        let y = ssn.Substring(4, 2)
        let individualNumber = int (ssn.Substring(IndividualNumberStart, IndividualNumberLength))
        let checksum = int (ssn.Substring(ChecksumStart, ChecksumLength))

        Assert.AreEqual(SsnLength, ssn.Length)
        Assert.AreEqual("06", d)
        Assert.AreEqual("02", m)
        Assert.AreEqual("52", y)
        Assert.IsTrue(20 <= individualNumber && individualNumber <= 99)
        Assert.IsTrue(0 <= checksum && checksum <= 9)

    [<TestMethod>]
    member __.``return a correct SSN 3`` () =
        let birthdate = DateTime(2000, 9, 15)
        let ssn = generateIcelandicSSN random birthdate false

        let d = ssn.Substring(0, 2)
        let m = ssn.Substring(2, 2)
        let y = ssn.Substring(4, 2)
        let individualNumber = int (ssn.Substring(IndividualNumberStart, IndividualNumberLength))
        let checksum = int (ssn.Substring(ChecksumStart, ChecksumLength))

        Assert.AreEqual(SsnLength, ssn.Length)
        Assert.AreEqual("15", d)
        Assert.AreEqual("09", m)
        Assert.AreEqual("00", y)
        Assert.IsTrue(20 <= individualNumber && individualNumber <= 99)
        Assert.IsTrue(0 <= checksum && checksum <= 9)

    [<TestMethod>]
    member __.``return a correct SSN 4`` () =
        let birthdate = DateTime(1999, 1, 1)
        let ssn = generateIcelandicSSN random birthdate false

        let d = ssn.Substring(0, 2)
        let m = ssn.Substring(2, 2)
        let y = ssn.Substring(4, 2)
        let individualNumber = int (ssn.Substring(IndividualNumberStart, IndividualNumberLength))
        let checksum = int (ssn.Substring(ChecksumStart, ChecksumLength))

        Assert.AreEqual(SsnLength, ssn.Length)
        Assert.AreEqual("01", d)
        Assert.AreEqual("01", m)
        Assert.AreEqual("99", y)
        Assert.IsTrue(20 <= individualNumber && individualNumber <= 99)
        Assert.IsTrue(0 <= checksum && checksum <= 9)

    [<TestMethod>]
    member __.``return an incorrect SSN when asking for fake SSN`` () =
        let birthdate = DateTime(1999, 1, 1)
        let ssnReal = generateIcelandicSSN random birthdate false
        let ssnFake = generateIcelandicSSN random birthdate true

        let validatePerson = ValidatePerson()
        let isRealValidating = validatePerson.ValidateSSN(Country.Iceland, ssnReal)
        let isFakeValidating = validatePerson.ValidateSSN(Country.Iceland, ssnFake)

        let d = ssnFake.Substring(0, 2)
        let m = ssnFake.Substring(2, 2)
        let y = ssnFake.Substring(4, 2)
        let individualNumber = int (ssnFake.Substring(IndividualNumberStart, IndividualNumberLength))
        let checksum = int (ssnFake.Substring(ChecksumStart, ChecksumLength))

        Assert.AreEqual(true,  isRealValidating)
        Assert.AreEqual(false, isFakeValidating)
        Assert.AreNotEqual(ssnReal, ssnFake)
        Assert.AreEqual(SsnLength, ssnFake.Length)
        Assert.AreEqual("01", d)
        Assert.AreEqual("01", m)
        Assert.AreEqual("99", y)
        Assert.IsTrue(20 <= individualNumber && individualNumber <= 99)
        Assert.IsTrue(0 <= checksum && checksum <= 9)
