namespace Tests

open System.Linq
open System.Text.RegularExpressions
open Microsoft.VisualStudio.TestTools.UnitTesting
open Address
open TestData
open Util

[<TestClass>]
type ``generateAddress1 should`` () =

    let random = getRandom false 100

    [<TestMethod>]
    member __.``return a random value from addresses and add a number between 1 and 50`` () =
        let addresses = getAddresses1 ()
        let address1 = generateAddress1 random addresses

        let numberRegex = Regex "(\d{1}|\d{2})$"
        let numberMatch = numberRegex.Match address1

        match numberMatch.Success with
        | true  -> Assert.IsTrue(0 < int numberMatch.Value && int numberMatch.Value < 50)
        | false -> Assert.IsTrue(false)

        let streetnameRegex = Regex "^\D+"
        let streetnameMatch = streetnameRegex.Match address1

        match streetnameMatch.Success with
        | true  -> Assert.IsTrue((Array.toList addresses).Contains(streetnameMatch.Value.Substring(0, streetnameMatch.Value.Length - 1)))
        | false -> Assert.IsTrue(false)


[<TestClass>]
type ``generateAddress2 should`` () =

    [<TestMethod>]
    member __.``return an emtpy string`` () =
        let result = generateAddress2 ()

        Assert.AreEqual("", result)
