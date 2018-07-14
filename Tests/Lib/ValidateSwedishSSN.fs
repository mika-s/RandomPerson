namespace Tests

open Microsoft.VisualStudio.TestTools.UnitTesting

open SwedishSSNValidation

[<TestClass>]
type ``validateSwedishSSN should`` () =

    [<TestMethod>]
    member this.``return false for "180401-3911"`` () =
        let isReal = validateSwedishSSN "180401-3911" false
        Assert.IsFalse(isReal)

    [<TestMethod>]
    member this.``return true for "980401-3911"`` () =
        let isReal = validateSwedishSSN "980401-3911" false
        Assert.IsTrue(isReal)

    [<TestMethod>]
    member this.``return true for "500618-7057"`` () =
        let isReal = validateSwedishSSN "500618-7057" false
        Assert.IsTrue(isReal)

    [<TestMethod>]
    member this.``return true for "911024-2659"`` () =
        let isReal = validateSwedishSSN "911024-2659" false
        Assert.IsTrue(isReal)

    [<TestMethod>]
    member this.``return false for "911024-2650"`` () =
        let isReal = validateSwedishSSN "911024-2650" false
        Assert.IsFalse(isReal)
