namespace Tests

open Microsoft.VisualStudio.TestTools.UnitTesting

open SwedenSSNValidation

[<TestClass>]
type ``validateSSNForSweden should`` () =

    [<TestMethod>]
    member __.``return false for "180401-3911"`` () =
        let isReal = validateSSNForSweden "180401-3911" false
        Assert.IsFalse(isReal)

    [<TestMethod>]
    member __.``return true for "980401-3911"`` () =
        let isReal = validateSSNForSweden "980401-3911" false
        Assert.IsTrue(isReal)

    [<TestMethod>]
    member __.``return true for "500618-7057"`` () =
        let isReal = validateSSNForSweden "500618-7057" false
        Assert.IsTrue(isReal)

    [<TestMethod>]
    member __.``return true for "911024-2659"`` () =
        let isReal = validateSSNForSweden "911024-2659" false
        Assert.IsTrue(isReal)

    [<TestMethod>]
    member __.``return false for "911024-2650"`` () =
        let isReal = validateSSNForSweden "911024-2650" false
        Assert.IsFalse(isReal)
