namespace Tests

open Microsoft.VisualStudio.TestTools.UnitTesting
open CliUtil
open TestData

[<TestClass>]
type ``b2i should`` () =

    [<TestMethod>]
    member this.``return 1 if true`` () =
        let boolean = true
        let result = b2i boolean
        Assert.AreEqual(1, result)

    [<TestMethod>]
    member this.``return 0 if false`` () =
        let boolean = false
        let result = b2i boolean
        Assert.AreEqual(0, result)

[<TestClass>]
type ``|Int|_| should`` () =

    [<TestMethod>]
    member this.``match with Int when int as string`` () =
        let isMatch = match "1" with
                      | Int i -> true
                      | _     -> false

        Assert.IsTrue(isMatch)

    [<TestMethod>]
    member this.``not match with Int when not int as string`` () =
        let isMatch = match "a" with
                      | Int i -> true
                      | _     -> false

        Assert.IsFalse(isMatch)

[<TestClass>]
type ``|Filename|_| should`` () =

    [<TestMethod>]
    member this.``match with Filename when input string is larger "test.txt"`` () =
        let isMatch = match "test.txt" with
                      | Filename fn -> true
                      | _           -> false

        Assert.IsTrue(isMatch)

    [<TestMethod>]
    member this.``not match with Filename when input string is empty`` () =
        let isMatch = match "" with
                      | Filename fn -> true
                      | _           -> false

        Assert.IsFalse(isMatch)

