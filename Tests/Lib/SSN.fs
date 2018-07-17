namespace Tests

open System
open Microsoft.VisualStudio.TestTools.UnitTesting
open RandomPersonLib
open Util
open SSN
open TestData

[<TestClass>]
type ``generateSSN should`` () =

    [<TestMethod>]
    member __.``return a correct SSN for Norwegian female 1`` () =
        let nationality = Nationality.Norwegian
        let birthdate = DateTime(1990, 5, 14)
        let gender = Gender.Female
        let random = getRandom false 100
        let ssn = generateSSN random nationality birthdate gender false false

        let d = ssn.Substring(0, 2)
        let m = ssn.Substring(2, 2)
        let y = ssn.Substring(4, 2)
        let individualNumber = Convert.ToInt32(ssn.Substring(6, 3))
        let checksum = Convert.ToInt32(ssn.Substring(9, 2))

        Assert.AreEqual(11, ssn.Length)
        Assert.AreEqual("14", d)
        Assert.AreEqual("05", m)
        Assert.AreEqual("90", y)
        Assert.IsTrue(0 <= individualNumber && individualNumber <= 499)
        Assert.IsTrue(0 <= checksum && checksum <= 99)
        Assert.IsTrue(isEven individualNumber)
