namespace Tests

open System
open Microsoft.VisualStudio.TestTools.UnitTesting
open RandomPersonLib
open Util
open NorwegianSSNGeneration
open TestData

[<TestClass>]
type ``norwegianGetIndividualNumber should`` () =

    let random = getRandom false 100

    [<TestMethod>]
    member this.``return between 500 and 749 when year is 1880`` () =
        let individualNumber = getIndividualNumber random 1880
        Assert.IsTrue(500 <= individualNumber && individualNumber < 749)
        Assert.IsFalse(individualNumber > 1000) // negative test

    [<TestMethod>]
    member this.``return between 000 and 499 when year is 1950`` () =
        let individualNumber = getIndividualNumber random 1950
        Assert.IsTrue(0 <= individualNumber && individualNumber < 499)

    [<TestMethod>]
    member this.``return between 500 and 999 when year is 2010`` () =
        let individualNumber = getIndividualNumber random 2010
        Assert.IsTrue(500 <= individualNumber && individualNumber < 999)

    [<TestMethod>]
    [<ExpectedException(typedefof<Exception>)>]
    member this.``fail when outside range`` () =
        getIndividualNumber random 20000 |> ignore

[<TestClass>]
type ``norwegianGetIndividualNumberMale should`` () =

    let random = getRandom false 100

    [<TestMethod>]
    member this.``return an odd number`` () =
        let individualNumber = getIndividualNumberMale random 2010
        Assert.IsTrue(isOdd individualNumber)

    [<TestMethod>]
    member this.``not return an even number`` () =
        let individualNumber = getIndividualNumberMale random 2011
        Assert.IsFalse(isEven individualNumber)

    [<TestMethod>]
    member this.``return a number between 000 and 999`` () =
        let individualNumber = getIndividualNumberMale random 1950
        Assert.IsTrue(0 <= individualNumber && individualNumber <= 999)

[<TestClass>]
type ``norwegianGetIndividualNumberFemale should`` () =

    let random = getRandom false 100

    [<TestMethod>]
    member this.``return an even number`` () =
        let individualNumber = getIndividualNumberFemale random 1986
        Assert.IsTrue(isEven individualNumber)

    [<TestMethod>]
    member this.``not return an odd number`` () =
        let individualNumber = getIndividualNumberFemale random 1950
        Assert.IsFalse(isOdd individualNumber)

    [<TestMethod>]
    member this.``return a number between 000 and 999`` () =
        let individualNumber = getIndividualNumberFemale random 1950
        Assert.IsTrue(0 <= individualNumber && individualNumber <= 999)


[<TestClass>]
type ``generateNorwegianIndividualNumber should`` () =

    let random = getRandom false 100

    [<TestMethod>]
    member this.``return an odd number when male`` () =
        let individualNumber = generateNorwegianIndividualNumber random 1967 Gender.Male
        let individualNumberAsInt = Convert.ToInt32(individualNumber)
        Assert.IsTrue(isOdd individualNumberAsInt)

[<TestClass>]
type ``generateNorwegianChecksum should`` () =

    let random = getRandom false 100

    [<TestMethod>]
    member this.``return a correct checksum for 04128533988`` () =
        let birthdate = DateTime(1985, 12, 04)
        let individualNumber = "339"
        let checksum = generateNorwegianChecksum birthdate individualNumber
        Assert.AreEqual("88", checksum)

    [<TestMethod>]
    member this.``return a correct checksum for 09035702757`` () =
        let birthdate = DateTime(1957, 03, 09)
        let individualNumber = "027"
        let checksum = generateNorwegianChecksum birthdate individualNumber
        Assert.AreEqual("57", checksum)

    [<TestMethod>]
    member this.``return a correct checksum for 18028211372`` () =
        let birthdate = DateTime(1982, 02, 18)
        let individualNumber = "113"
        let checksum = generateNorwegianChecksum birthdate individualNumber
        Assert.AreEqual("72", checksum)

[<TestClass>]
type ``anonymizeSSN for Norwegian SSNs should`` () =

    let random = getRandom false 100

    [<TestMethod>]
    member this.``return 21108812345 when given 21108811345`` () =
        let ssn = "21108811345"
        let fake = anonymizeSSN ssn

        Assert.AreEqual("21108812345", fake)

    [<TestMethod>]
    member this.``return 21108811145 when given 21108810145`` () =
        let ssn = "21108810145"
        let fake = anonymizeSSN ssn

        Assert.AreEqual("21108811145", fake)

    [<TestMethod>]
    member this.``return 21108811145 when given 21108819145`` () =
        let ssn = "21108819145"
        let fake = anonymizeSSN ssn

        Assert.AreEqual("21108810145", fake)

[<TestClass>]
type ``generateNorwegianSSN should`` () =

    let random = getRandom false 100

    [<TestMethod>]
    member this.``return a correct SSN for male 1`` () =
        let birthdate = DateTime(1985, 12, 04)
        let gender = Gender.Male
        let ssn = generateNorwegianSSN random birthdate gender false

        let d = ssn.Substring(0, 2)
        let m = ssn.Substring(2, 2)
        let y = ssn.Substring(4, 2)
        let individualNumber = Convert.ToInt32(ssn.Substring(6, 3))
        let checksum = Convert.ToInt32(ssn.Substring(9, 2))

        Assert.AreEqual(11, ssn.Length)
        Assert.AreEqual("04", d)
        Assert.AreEqual("12", m)
        Assert.AreEqual("85", y)
        Assert.IsTrue(0 <= individualNumber && individualNumber <= 499)
        Assert.IsTrue(0 <= checksum && checksum <= 99)
        Assert.IsTrue(isOdd individualNumber)

    [<TestMethod>]
    member this.``return a correct SSN for male 2`` () =
        let birthdate = DateTime(1952, 2, 6)
        let gender = Gender.Male
        let ssn = generateNorwegianSSN random birthdate gender false

        let d = ssn.Substring(0, 2)
        let m = ssn.Substring(2, 2)
        let y = ssn.Substring(4, 2)
        let individualNumber = Convert.ToInt32(ssn.Substring(6, 3))
        let checksum = Convert.ToInt32(ssn.Substring(9, 2))

        Assert.AreEqual(11, ssn.Length)
        Assert.AreEqual("06", d)
        Assert.AreEqual("02", m)
        Assert.AreEqual("52", y)
        Assert.IsTrue(0 <= individualNumber && individualNumber <= 499)
        Assert.IsTrue(0 <= checksum && checksum <= 99)
        Assert.IsTrue(isOdd individualNumber)

    [<TestMethod>]
    member this.``return a correct SSN for female 1`` () =
        let birthdate = DateTime(2000, 9, 15)
        let gender = Gender.Female
        let ssn = generateNorwegianSSN random birthdate gender false

        let d = ssn.Substring(0, 2)
        let m = ssn.Substring(2, 2)
        let y = ssn.Substring(4, 2)
        let individualNumber = Convert.ToInt32(ssn.Substring(6, 3))
        let checksum = Convert.ToInt32(ssn.Substring(9, 2))

        Assert.AreEqual(11, ssn.Length)
        Assert.AreEqual("15", d)
        Assert.AreEqual("09", m)
        Assert.AreEqual("00", y)
        Assert.IsTrue(500 <= individualNumber && individualNumber <= 999)
        Assert.IsTrue(0 <= checksum && checksum <= 99)
        Assert.IsTrue(isEven individualNumber)

    [<TestMethod>]
    member this.``return a correct SSN for female 2`` () =
        let birthdate = DateTime(1999, 1, 1)
        let gender = Gender.Female
        let ssn = generateNorwegianSSN random birthdate gender false

        let d = ssn.Substring(0, 2)
        let m = ssn.Substring(2, 2)
        let y = ssn.Substring(4, 2)
        let individualNumber = Convert.ToInt32(ssn.Substring(6, 3))
        let checksum = Convert.ToInt32(ssn.Substring(9, 2))

        Assert.AreEqual(11, ssn.Length)
        Assert.AreEqual("01", d)
        Assert.AreEqual("01", m)
        Assert.AreEqual("99", y)
        Assert.IsTrue(0 <= individualNumber && individualNumber <= 499)
        Assert.IsTrue(0 <= checksum && checksum <= 99)
        Assert.IsTrue(isEven individualNumber)

    [<TestMethod>]
    member this.``return an incorrect SSN for female when asking for fake SSN`` () =
        let birthdate = DateTime(1999, 1, 1)
        let gender = Gender.Female
        let ssnReal = generateNorwegianSSN random birthdate gender false
        let ssnFake = generateNorwegianSSN random birthdate gender true

        let randomPerson = RandomPerson()
        let isRealValidating = randomPerson.ValidateSSN(Nationality.Norwegian, ssnReal)
        let isFakeValidating = randomPerson.ValidateSSN(Nationality.Norwegian, ssnFake)

        let d = ssnFake.Substring(0, 2)
        let m = ssnFake.Substring(2, 2)
        let y = ssnFake.Substring(4, 2)
        let individualNumber = Convert.ToInt32(ssnFake.Substring(6, 3))
        let checksum = Convert.ToInt32(ssnFake.Substring(9, 2))

        Assert.AreEqual(true,  isRealValidating)
        Assert.AreEqual(false, isFakeValidating)
        Assert.AreNotEqual(ssnReal, ssnFake)
        Assert.AreEqual(11, ssnFake.Length)
        Assert.AreEqual("01", d)
        Assert.AreEqual("01", m)
        Assert.AreEqual("99", y)
        Assert.IsTrue(0 <= individualNumber && individualNumber <= 499)
        Assert.IsTrue(0 <= checksum && checksum <= 99)
        Assert.IsTrue(isEven individualNumber)
