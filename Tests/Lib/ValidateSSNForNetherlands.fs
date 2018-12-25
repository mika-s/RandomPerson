namespace Tests

open Microsoft.VisualStudio.TestTools.UnitTesting

open NetherlandsSSNValidation

[<TestClass>]
type ``validateSSNForNetherlands should`` () =

    [<TestMethod>]
    member __.``return false for "12121212345"`` () =
        let isReal = validateSSNForNetherlands "12121212345"
        Assert.IsFalse(isReal)

    [<TestMethod>]
    member __.``return true for "269740533"`` () =
        let isReal = validateSSNForNetherlands "269740533"
        Assert.IsTrue(isReal)

