namespace Tests

open System.Text.RegularExpressions
open Microsoft.VisualStudio.TestTools.UnitTesting
open MacAddress
open Util

[<TestClass>]
type ``generateMacAddress should`` () =

    [<Literal>]
    let MacAddressLength = 17

    [<Literal>]
    let NumberOfSeparators = 5

    let random = getRandom false 100

    let isMacAddressWithLowercaseOnly (potentialMacAddress: string) =
        let macAddressRegex = Regex "([0-9a-f]{2}[:-]){5}([0-9a-f]{2})"
        macAddressRegex.IsMatch potentialMacAddress

    let isMacAddressWithUppercaseOnly (potentialMacAddress: string) =
        let macAddressRegex = Regex "([0-9A-F]{2}[:-]){5}([0-9A-F]{2})"
        macAddressRegex.IsMatch potentialMacAddress

    [<TestMethod>]
    member __.``return a random MAC address with hyphens when useColons is false and useUppercase is false`` () =
        let generatedMacAddress = generateMacAddress random false false

        Assert.AreEqual(generatedMacAddress.Length, MacAddressLength)
        Assert.AreEqual(generatedMacAddress.Split('-').Length - 1, NumberOfSeparators)
        Assert.IsTrue(isMacAddressWithLowercaseOnly generatedMacAddress)


    [<TestMethod>]
    member __.``return a random MAC address with colons when useColons is true and useUppercase is false`` () =
        let generatedMacAddress = generateMacAddress random true false

        Assert.AreEqual(generatedMacAddress.Length, MacAddressLength)
        Assert.AreEqual(generatedMacAddress.Split(':').Length - 1, NumberOfSeparators)
        Assert.IsTrue(isMacAddressWithLowercaseOnly generatedMacAddress)

    [<TestMethod>]
    member __.``return a random MAC address with hyphens when useColons is false and useUppercase is true`` () =
        let generatedMacAddress = generateMacAddress random false true

        Assert.AreEqual(generatedMacAddress.Length, MacAddressLength)
        Assert.AreEqual(generatedMacAddress.Split('-').Length - 1, NumberOfSeparators)
        Assert.IsTrue(isMacAddressWithUppercaseOnly generatedMacAddress)


    [<TestMethod>]
    member __.``return a random MAC address with colons when useColons is true and useUppercase is true`` () =
        let generatedMacAddress = generateMacAddress random true true

        Assert.AreEqual(generatedMacAddress.Length, MacAddressLength)
        Assert.AreEqual(generatedMacAddress.Split(':').Length - 1, NumberOfSeparators)
        Assert.IsTrue(isMacAddressWithUppercaseOnly generatedMacAddress)