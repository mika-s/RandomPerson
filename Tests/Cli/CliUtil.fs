namespace Tests

open System
open Microsoft.VisualStudio.TestTools.UnitTesting
open CliUtil

[<TestClass>]
type ``b2i should`` () =

    [<TestMethod>]
    member __.``return 1 if true`` () =
        let boolean = true
        let result = b2i boolean
        Assert.AreEqual(1, result)

    [<TestMethod>]
    member __.``return 0 if false`` () =
        let boolean = false
        let result = b2i boolean
        Assert.AreEqual(0, result)

[<TestClass>]
type ``|Int|_| should`` () =

    [<TestMethod>]
    member __.``match with Int when int as string`` () =
        let isMatch = match "1" with
                      | Int _ -> true
                      | _     -> false

        Assert.IsTrue(isMatch)

    [<TestMethod>]
    member __.``not match with Int when not int as string`` () =
        let isMatch = match "a" with
                      | Int _ -> true
                      | _     -> false

        Assert.IsFalse(isMatch)

[<TestClass>]
type ``|Filename|_| should`` () =

    [<TestMethod>]
    member __.``match with Filename when input string is larger "test,txt"`` () =
        let isMatch = match "test.txt" with
                      | Filename _ -> true
                      | _          -> false

        Assert.IsTrue(isMatch)

    [<TestMethod>]
    member __.``not match with Filename when input string is empty`` () =
        let isMatch = match "" with
                      | Filename _ -> true
                      | _          -> false

        Assert.IsFalse(isMatch)

[<TestClass>]
type ``nullCoalesce should`` () =

    [<TestMethod>]
    member __.``return first parameter when first is not null`` () =
        let first = Nullable<int>(1)
        let second = 123
        
        let result = nullCoalesce first second

        Assert.AreEqual(first, result)

    [<TestMethod>]
    member __.``return second parameter when first is null`` () =
        let first = Nullable<int>()
        let second = 123
        
        let result = nullCoalesce first second

        Assert.AreEqual(second, result)
