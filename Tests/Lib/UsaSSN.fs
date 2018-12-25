namespace Tests

open Microsoft.VisualStudio.TestTools.UnitTesting
open Util
open UsaSSNGeneration

[<TestClass>]
type ``generateAreaNumber for American SSNs should`` () =

    let random = getRandom false 100

    [<TestMethod>]
    member __.``return a number between 1 and 900`` () =
        let areaNumber = generateAreaNumber random |> int

        Assert.IsTrue((1 <= areaNumber && areaNumber <= 899) && areaNumber <> 666)
        Assert.AreNotEqual(950, areaNumber) // negative test

[<TestClass>]
type ``generateGroupNumber for American SSNs should`` () =

    let random = getRandom false 100

    [<TestMethod>]
    member __.``return a number between 1 and 99`` () =
        let groupNumber = generateGroupNumber random |> int

        Assert.IsTrue((1 <= groupNumber && groupNumber <= 99))
        Assert.AreNotEqual(-1, groupNumber) // negative test

[<TestClass>]
type ``generateSerialNumber for American SSNs should`` () =

    let random = getRandom false 100

    [<TestMethod>]
    member __.``return a number between 0001 and 9999`` () =
        let serialNumber = generateSerialNumber random |> int

        Assert.IsTrue((1 <= serialNumber && serialNumber <= 9999))
        Assert.AreNotEqual(10000, serialNumber) // negative test

[<TestClass>]
type ``anonymizeSSN for American SSNs should`` () =

    let random = getRandom false 100

    [<TestMethod>]
    member __.``return either 4, 5, 6, 7, 8, 9`` () =
        let oldSsn = "234-28-1234"
        let newSsn = anonymizeSSN random oldSsn
        let splitNewSsn = newSsn.Split('-')
        Assert.AreNotEqual(splitNewSsn.[0], "234")
        Assert.AreEqual(splitNewSsn.[1], "28")
        Assert.AreEqual(splitNewSsn.[2], "1234")

[<TestClass>]
type ``generateAmericanSSN for American SSNs should`` () =

    let random = getRandom false 100

    [<TestMethod>]
    member __.``return either 4, 5, 6, 7, 8, 9`` () =
        let ssn = generateAmericanSSN random false
        let splitSsn = ssn.Split('-')
        let areaNumber   = int splitSsn.[0]
        let groupNumber  = int splitSsn.[1]
        let serialNumber = int splitSsn.[2]

        Assert.AreEqual(3, splitSsn.Length)
        Assert.IsTrue((1 <= areaNumber   && areaNumber <= 899))
        Assert.IsTrue((1 <= groupNumber  && groupNumber <= 99))
        Assert.IsTrue((1 <= serialNumber && serialNumber <= 9999))
