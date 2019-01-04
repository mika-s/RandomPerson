namespace Tests

open Microsoft.VisualStudio.TestTools.UnitTesting

open ValidatePAN

[<TestClass>]
type ``validatePAN should`` () =

    [<TestMethod>]
    member __.``return true for "5555555555554444" (proper MasterCard)`` () =
        let isReal = validatePAN "5555555555554444"
        Assert.IsTrue(isReal)

    [<TestMethod>]
    member __.``return false for "5555555555554446" (improper MasterCard)`` () =
        let isReal = validatePAN "5555555555554446"
        Assert.IsFalse(isReal)

    [<TestMethod>]
    member __.``return true for "5555-5555-5555-4444" (proper MasterCard with hyphens)`` () =
        let isReal = validatePAN "5555-5555-5555-4444"
        Assert.IsTrue(isReal)

    [<TestMethod>]
    member __.``return true for "5555 5555 5555 4444" (proper MasterCard with spaces)`` () =
        let isReal = validatePAN "5555 5555 5555 4444"
        Assert.IsTrue(isReal)

    [<TestMethod>]
    member __.``return true for "4111111111111111" (proper Visa)`` () =
        let isReal = validatePAN "4111111111111111"
        Assert.IsTrue(isReal)

    [<TestMethod>]
    member __.``return false for "4111111111111112" (improper Visa)`` () =
        let isReal = validatePAN "4111111111111112"
        Assert.IsFalse(isReal)
