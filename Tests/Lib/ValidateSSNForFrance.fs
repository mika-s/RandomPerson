namespace Tests

open Microsoft.VisualStudio.TestTools.UnitTesting

open FranceSSNValidation

[<TestClass>]
type ``validateSSNForFrance should`` () =

    [<TestMethod>]
    member __.``return true for "180126955222380"`` () =
        let isReal, _ = validateSSNForFrance "180126955222380"
        Assert.IsFalse(isReal)

    [<TestMethod>]
    member __.``return true for "283209921625930"`` () =
        let isReal, _ = validateSSNForFrance "283209921625930"
        Assert.IsTrue(isReal)

    [<TestMethod>]
    member __.``return false for "180126955222381"`` () =
        let isReal, _ = validateSSNForFrance "180126955222381"
        Assert.IsFalse(isReal)

    [<TestMethod>]
    member __.``return false for "283209921625931"`` () =
        let isReal, _ = validateSSNForFrance "283209921625931"
        Assert.IsFalse(isReal)
        