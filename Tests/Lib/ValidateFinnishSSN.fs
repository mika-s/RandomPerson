namespace Tests

open Microsoft.VisualStudio.TestTools.UnitTesting

open FinnishSSNValidation

[<TestClass>]
type ``validateFinnishSSN should`` () =

    [<TestMethod>]
    member __.``return false for "070969-147B"`` () =
        let isReal = validateFinnishSSN "070969-147B"
        Assert.IsFalse(isReal)

    [<TestMethod>]
    member __.``return true for "070969-147A"`` () =
        let isReal = validateFinnishSSN "070969-147A"
        Assert.IsTrue(isReal)

    [<TestMethod>]
    member __.``return true for "250949-648X"`` () =
        let isReal = validateFinnishSSN "250949-648X"
        Assert.IsTrue(isReal)

    [<TestMethod>]
    member __.``return true for "190846-711H"`` () =
        let isReal = validateFinnishSSN "190846-711H"
        Assert.IsTrue(isReal)

    [<TestMethod>]
    member __.``return false for "410846-711H"`` () =
        let isReal = validateFinnishSSN "410846-711H"
        Assert.IsFalse(isReal)

    [<TestMethod>]
    member __.``return true for "010199-0044"`` () =
        let isReal = validateFinnishSSN "010199-0044"
        Assert.IsTrue(isReal)

    [<TestMethod>]
    member __.``return true for "010199-2502"`` () =
        let isReal = validateFinnishSSN "010199-2502"
        Assert.IsTrue(isReal)
        