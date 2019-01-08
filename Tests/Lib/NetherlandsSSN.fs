namespace Tests

open Microsoft.VisualStudio.TestTools.UnitTesting
open RandomPersonLib
open Util
open NetherlandsSSNParameters
open NetherlandsSSNGeneration

[<TestClass>]
type ``generateIndividualNumber for Dutch SSNs should`` () =

    let random = getRandom false 100

    [<TestMethod>]
    member __.``return a number between 000000000 and 999999999`` () =
        let individualNumber = generateIndividualNumber random
        let individualNumberAsInt = int individualNumber
        Assert.IsTrue(0 <= individualNumberAsInt && individualNumberAsInt <= 99999999)
 
[<TestClass>]
type ``generateChecksum for Dutch SSNs should`` () =

    [<TestMethod>]
    member __.``return a correct checksum for 269740533`` () =
        let individualNumber = "26974053"
        let checksum = generateChecksum individualNumber
        Assert.AreEqual("3", checksum)


[<TestClass>]
type ``anonymizeSSN for Dutch SSNs should`` () =

    [<TestMethod>]
    member __.``return 269741533 when given 269740533`` () =
        let ssn = "269740533"
        let fake = anonymizeSSN ssn

        Assert.AreEqual("269741533", fake)

    [<TestMethod>]
    member __.``return 123457789 when given 123456789`` () =
        let ssn = "123456789"
        let fake = anonymizeSSN ssn

        Assert.AreEqual("123457789", fake)

    [<TestMethod>]
    member __.``return 123451789 when given 123450789`` () =
        let ssn = "123450789"
        let fake = anonymizeSSN ssn

        Assert.AreEqual("123451789", fake)

    [<TestMethod>]
    member __.``return 123450789 when given 123459789`` () =
        let ssn = "123459789"
        let fake = anonymizeSSN ssn

        Assert.AreEqual("123450789", fake)

[<TestClass>]
type ``generateDutchSSN should`` () =

    let random = getRandom false 100

    [<TestMethod>]
    member __.``return a correct SSN`` () =
 
        let ssn = generateDutchSSN random false

        let individualNumber = int (ssn.Substring(IndividualNumberStart, IndividualNumberLength))
        let checksum = int (ssn.Substring(ChecksumStart, ChecksumLength))

        Assert.AreEqual(SsnLength, ssn.Length)
        Assert.IsTrue(0 <= individualNumber && individualNumber <= 99999999)
        Assert.IsTrue(0 <= checksum && checksum <= 9)

    [<TestMethod>]
    member __.``return an incorrect SSN when asking for fake SSN`` () =

        let ssnReal = generateDutchSSN random false
        let ssnFake = generateDutchSSN random true

        let validatePerson = ValidatePerson()
        let isRealValidating, _ = validatePerson.ValidateSSN(Country.Netherlands, ssnReal)
        let isFakeValidating, _ = validatePerson.ValidateSSN(Country.Netherlands, ssnFake)

        let individualNumber = int (ssnFake.Substring(IndividualNumberStart, IndividualNumberLength))
        let checksum = int (ssnFake.Substring(ChecksumStart, ChecksumLength))

        Assert.AreEqual(true,  isRealValidating)
        Assert.AreEqual(false, isFakeValidating)
        Assert.AreNotEqual(ssnReal, ssnFake)
        Assert.AreEqual(SsnLength, ssnFake.Length)
        Assert.IsTrue(0 <= individualNumber && individualNumber <= 99999999)
        Assert.IsTrue(0 <= checksum && checksum <= 9)
