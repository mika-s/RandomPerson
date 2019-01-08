namespace Tests

open Microsoft.VisualStudio.TestTools.UnitTesting

open IcelandSSNValidation

[<TestClass>]
type ``validateSSNForIceland should`` () =

    [<TestMethod>]
    member __.``return true for "310896-2099"`` () =
        let isReal, _ = validateSSNForIceland "310896-2099"
        Assert.IsTrue(isReal)

    [<TestMethod>]
    member __.``return false for "310896-3099"`` () =
        let isReal, _ = validateSSNForIceland "310896-3099"
        Assert.IsFalse(isReal)

    [<TestMethod>]
    member __.``return false for "310896-2199"`` () =
        let isReal, _ = validateSSNForIceland "310896-2199"
        Assert.IsFalse(isReal)

    [<TestMethod>]
    member __.``return false for "320896-2099"`` () =
        let isReal, _ = validateSSNForIceland "320896-2099"
        Assert.IsFalse(isReal)
