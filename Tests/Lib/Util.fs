namespace Tests

open System.Collections.Generic
open Microsoft.VisualStudio.TestTools.UnitTesting
open Util

[<TestClass>]
type ``isOdd should`` () =

    [<TestMethod>]
    member __.``return an odd number`` () =
        let number = 201
        let result = isOdd number
        Assert.IsTrue((number % 2 <> 0) = result)

[<TestClass>]
type ``isEven should`` () =

    [<TestMethod>]
    member __.``return an even number`` () =
        let number = 200
        let result = isEven number
        Assert.IsTrue((number % 2 = 0) = result)

[<TestClass>]
type ``randomIntBetween should`` () =

    [<TestMethod>]
    member __.``return an integer between 1 and 10, given 1 and 10`` () =
        let result = randomIntBetween 1 10
        Assert.IsTrue(1 <= result && result < 10)

    [<TestMethod>]
    member __.``return an integer between -100 and 10, given -100 and 10`` () =
        let result = randomIntBetween -100 10
        Assert.IsTrue(-100 <= result && result < 10)

[<TestClass>]
type ``randomFloatBetween should`` () =

    [<TestMethod>]
    member __.``return an random float between -20,0 and 20,0`` () =
        let result = randomFloatBetween -20.0 20.0
        Assert.IsTrue(-20.0 <= result && result < 20.0)

    [<TestMethod>]
    member __.``return an random float between 0,0 and 200,0`` () =
        let result = randomFloatBetween 0.0 200.0
        Assert.IsTrue(0.0 <= result && result < 200.0)

[<TestClass>]
type ``intFromChar should`` () =

    [<TestMethod>]
    member __.``return 1 given '1'`` () =
        let numberAsChar = '1'
        let number = intFromChar numberAsChar
        Assert.AreEqual(1, number)

    [<TestMethod>]
    member __.``return 0 given '0'`` () =
        let numberAsChar = '0'
        let number = intFromChar numberAsChar
        Assert.AreEqual(0, number)

    [<TestMethod>]
    member __.``return 9 given '9'`` () =
        let numberAsChar = '9'
        let number = intFromChar numberAsChar
        Assert.AreEqual(9, number)

    [<TestMethod>]
    member __.``return 5 given '5'`` () =
        let numberAsChar = '5'
        let number = intFromChar numberAsChar
        Assert.AreEqual(5, number)

[<TestClass>]
type ``incrementNumberInString should`` () =

    [<TestMethod>]
    member __.``return "1234" given "1233" and index 3`` () =
        let input = "1233"
        let incremented = incrementNumberInString input 3
        Assert.AreEqual("1234", incremented)

    [<TestMethod>]
    member __.``return "1234" given "1134" and index 1`` () =
        let input = "1134"
        let incremented = incrementNumberInString input 1
        Assert.AreEqual("1234", incremented)

    [<TestMethod>]
    member __.``return "1230" given "1239" and index 3`` () =
        let input = "1239"
        let incremented = incrementNumberInString input 3
        Assert.AreEqual("1230", incremented)

[<TestClass>]
type ``convertDictToMap should`` () =

    [<TestMethod>]
    member __.``convert a Dictionary into a Map`` () =
        let dictionary = Dictionary<string, string>()
        dictionary.Add("key1", "value1")
        dictionary.Add("key2", "value2")

        let map = convertDictToMap dictionary

        Assert.AreEqual("value1", map.["key1"])
        Assert.AreEqual("value2", map.["key2"])
        