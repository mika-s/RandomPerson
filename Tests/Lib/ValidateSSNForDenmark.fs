namespace Tests

open Microsoft.VisualStudio.TestTools.UnitTesting

open DenmarkSSNValidation

[<TestClass>]
type ``validateSSNForDenmark should`` () =

    [<TestMethod>]
    member __.``return false for "450627-6156"`` () =
        let isReal = validateSSNForDenmark "450627-6156"
        Assert.IsFalse(isReal)

    [<TestMethod>]
    member __.``return true for "151163-0211"`` () =
        let isReal = validateSSNForDenmark "151163-0211"
        Assert.IsTrue(isReal)

    [<TestMethod>]
    member __.``return true for "180766-1716"`` () =
        let isReal = validateSSNForDenmark "180766-1716"
        Assert.IsTrue(isReal)

    [<TestMethod>]
    member __.``return true for "220998-1486"`` () =
        let isReal = validateSSNForDenmark "220998-1486"
        Assert.IsTrue(isReal)

    [<TestMethod>]
    member __.``return false for "220999-1486"`` () =
        let isReal = validateSSNForDenmark "220999-1486"
        Assert.IsFalse(isReal)

    [<TestMethod>]
    member __.``return false for "151163-02111" (too long)`` () =
        let isReal = validateSSNForDenmark "151163-02111"
        Assert.IsFalse(isReal)

    [<TestMethod>]
    member __.``return false for "151163-021" (too short)`` () =
        let isReal = validateSSNForDenmark "151163-021"
        Assert.IsFalse(isReal)