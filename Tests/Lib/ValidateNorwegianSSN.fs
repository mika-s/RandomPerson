namespace Tests

open Microsoft.VisualStudio.TestTools.UnitTesting

open NorwegianSSNValidation

[<TestClass>]
type ``validateNorwegianSSN should`` () =

    [<TestMethod>]
    member this.``return false for "12121212345"`` () =
        let isReal = validateNorwegianSSN "12121212345"
        Assert.IsFalse(isReal)

    [<TestMethod>]
    member this.``return true for "23071053008"`` () =
        let isReal = validateNorwegianSSN "23071053008"
        Assert.IsTrue(isReal)

    [<TestMethod>]
    member this.``return true for "15092913888"`` () =
        let isReal = validateNorwegianSSN "15092913888"
        Assert.IsTrue(isReal)

    [<TestMethod>]
    member this.``return true for "31031005542"`` () =
        let isReal = validateNorwegianSSN "31031005542"
        Assert.IsTrue(isReal)

    [<TestMethod>]
    member this.``return false for "11031005542"`` () =
        let isReal = validateNorwegianSSN "11031005542"
        Assert.IsFalse(isReal)
