namespace Tests

open Microsoft.VisualStudio.TestTools.UnitTesting

open UsaSSNValidation

[<TestClass>]
type ``validateSSNForUSA should`` () =

    [<TestMethod>]
    member __.``return true for "123-45-1234"`` () =
        let isReal = validateSSNForUSA "123-45-1234"
        Assert.IsTrue(isReal)

    [<TestMethod>]
    member __.``return false for "000-45-1234"`` () =
        let isReal = validateSSNForUSA "000-45-1234"
        Assert.IsFalse(isReal)

    [<TestMethod>]
    member __.``return false for "666-45-1234"`` () =
        let isReal = validateSSNForUSA "666-45-1234"
        Assert.IsFalse(isReal)

    [<TestMethod>]
    member __.``return false for "900-45-1234"`` () =
        let isReal = validateSSNForUSA "900-45-1234"
        Assert.IsFalse(isReal)

    [<TestMethod>]
    member __.``return false for "999-45-1234"`` () =
        let isReal = validateSSNForUSA "999-45-1234"
        Assert.IsFalse(isReal)

    [<TestMethod>]
    member __.``return false for "950-45-1234"`` () =
        let isReal = validateSSNForUSA "950-45-1234"
        Assert.IsFalse(isReal)

    [<TestMethod>]
    member __.``return false for "123-00-1234"`` () =
        let isReal = validateSSNForUSA "123-00-1234"
        Assert.IsFalse(isReal)

    [<TestMethod>]
    member __.``return false for "123-45-0000"`` () =
        let isReal = validateSSNForUSA "123-45-0000"
        Assert.IsFalse(isReal)