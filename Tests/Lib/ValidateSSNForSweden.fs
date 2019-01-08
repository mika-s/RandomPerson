namespace Tests

open Microsoft.VisualStudio.TestTools.UnitTesting

open SwedenSSNValidation

[<TestClass>]
type ``validateSSNForSweden should`` () =

    [<TestMethod>]
    member __.``return false for "180401-3911"`` () =
        let isReal, _ = validateSSNForSweden "180401-3911"
        Assert.IsFalse(isReal)

    [<TestMethod>]
    member __.``return true for "980401-3911"`` () =
        let isReal, _ = validateSSNForSweden "980401-3911"
        Assert.IsTrue(isReal)

    [<TestMethod>]
    member __.``return true for "500618-7057"`` () =
        let isReal, _ = validateSSNForSweden "500618-7057"
        Assert.IsTrue(isReal)

    [<TestMethod>]
    member __.``return true for "911024-2659"`` () =
        let isReal, _ = validateSSNForSweden "911024-2659"
        Assert.IsTrue(isReal)

    [<TestMethod>]
    member __.``return false for "911024-2650"`` () =
        let isReal, _ = validateSSNForSweden "911024-2650"
        Assert.IsFalse(isReal)
