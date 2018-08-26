namespace Tests

open Microsoft.VisualStudio.TestTools.UnitTesting

open DutchSSNValidation

[<TestClass>]
type ``validateDutchSSN should`` () =

    [<TestMethod>]
    member __.``return false for "12121212345"`` () =
        let isReal = validateDutchSSN "12121212345"
        Assert.IsFalse(isReal)

    [<TestMethod>]
    member __.``return true for "269740533"`` () =
        let isReal = validateDutchSSN "269740533"
        Assert.IsTrue(isReal)

    //[<TestMethod>]
    //member __.``return true for "15092913888"`` () =
    //    let isReal = validateDutchSSN "15092913888"
    //    Assert.IsTrue(isReal)

    //[<TestMethod>]
    //member __.``return true for "31031005542"`` () =
    //    let isReal = validateDutchSSN "31031005542"
    //    Assert.IsTrue(isReal)

    //[<TestMethod>]
    //member __.``return false for "11031005542"`` () =
    //    let isReal = validateDutchSSN "11031005542"
    //    Assert.IsFalse(isReal)
