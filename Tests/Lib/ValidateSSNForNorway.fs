namespace Tests

open Microsoft.VisualStudio.TestTools.UnitTesting

open NorwaySSNValidation

[<TestClass>]
type ``validateSSNForNorway should`` () =

    [<TestMethod>]
    member __.``return false for "12121212345"`` () =
        let isReal, _ = validateSSNForNorway "12121212345"
        Assert.IsFalse(isReal)

    [<TestMethod>]
    member __.``return true for "23071053008"`` () =
        let isReal, _ = validateSSNForNorway "23071053008"
        Assert.IsTrue(isReal)

    [<TestMethod>]
    member __.``return true for "15092913888"`` () =
        let isReal, _ = validateSSNForNorway "15092913888"
        Assert.IsTrue(isReal)

    [<TestMethod>]
    member __.``return true for "31031005542"`` () =
        let isReal, _ = validateSSNForNorway "31031005542"
        Assert.IsTrue(isReal)

    [<TestMethod>]
    member __.``return false for "11031005542"`` () =
        let isReal, _ = validateSSNForNorway "11031005542"
        Assert.IsFalse(isReal)
