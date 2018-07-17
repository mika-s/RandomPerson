namespace Tests

open Microsoft.VisualStudio.TestTools.UnitTesting

open DanishSSNValidation

[<TestClass>]
type ``validateDanishSSN should`` () =

    [<TestMethod>]
    member __.``return false for "450627-6156"`` () =
        let isReal = validateDanishSSN "450627-6156"
        Assert.IsFalse(isReal)

    [<TestMethod>]
    member __.``return true for "151163-0211"`` () =
        let isReal = validateDanishSSN "151163-0211"
        Assert.IsTrue(isReal)

    [<TestMethod>]
    member __.``return true for "180766-1716"`` () =
        let isReal = validateDanishSSN "180766-1716"
        Assert.IsTrue(isReal)

    [<TestMethod>]
    member __.``return true for "220998-1486"`` () =
        let isReal = validateDanishSSN "220998-1486"
        Assert.IsTrue(isReal)

    [<TestMethod>]
    member __.``return false for "220999-1486"`` () =
        let isReal = validateDanishSSN "220999-1486"
        Assert.IsFalse(isReal)
