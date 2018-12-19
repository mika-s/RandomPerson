namespace Tests

open Microsoft.VisualStudio.TestTools.UnitTesting
open OrdinaryReplaces
open TestData

[<TestClass>]
type ``parseOrdinaryReplaces should`` () =

    [<TestMethod>]
    member __.``return a string with #{SSN} replaced by SSN`` () =
        let person = getTestPerson ()
        let replaced = performOrdinaryReplaces person "SSN: #{SSN}"

        let expectedString = sprintf "SSN: %s" person.SSN

        Assert.AreEqual(expectedString, replaced)

    [<TestMethod>]
    member __.``return a string with #{FirstName,ToLower()} replaced by the FirstName in all lower caps`` () =
        let person = getTestPerson ()
        let replaced = performOrdinaryReplaces person "First name: #{FirstName.ToLower()}"

        let expectedString = sprintf "First name: %s" (person.FirstName.ToLower())

        Assert.AreEqual(expectedString, replaced)

    [<TestMethod>]
    member __.``return a string with #{LastName,ToUpper()} replaced by the FirstName in all upper caps`` () =
        let person = getTestPerson ()
        let replaced = performOrdinaryReplaces person "Last name: #{LastName.ToUpper()}"

        let expectedString = sprintf "Last name: %s" (person.LastName.ToUpper())

        Assert.AreEqual(expectedString, replaced)

    [<TestMethod>]
    member __.``return a string with #{City,Capitalize()} replaced by the City with first letter uppercase`` () =
        let person = getTestPerson ()
        let replaced = performOrdinaryReplaces person "City: #{City.Capitalize()}"

        let expectedString = "City: OSLO"

        Assert.AreEqual(expectedString, replaced)
