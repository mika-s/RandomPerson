namespace Tests

open System
open Microsoft.VisualStudio.TestTools.UnitTesting
open RandomReplaces
open System.Text.RegularExpressions

[<TestClass>]
type ``replaceRandomInt should`` () =

    let randomIntRegex = Regex "#{Random\(\s?int\s?,\s?(-?\d+)\s?,\s?(-?\d+)\s?\)}"

    [<TestMethod>]
    member __.``return find and replace #{Random(int, 10, 100)} in a string with a random integer`` () =
        let remaining = "Age: #{Random(int, 10, 100)}, fortune: Random(float, 1000.0, 100000.0)"

        let returnString = replaceRandomInt randomIntRegex remaining

        let firstPart = returnString.Substring(0, 5)
        let randomPart = Convert.ToInt32 (returnString.Split(',').[0].Split(' ').[1])

        Assert.AreEqual("Age: ", firstPart)
        Assert.IsTrue(10 <= randomPart && randomPart <= 100)

    [<TestMethod>]
    member __.``return find and replace #{Random(int,-10,0)} in a string with a random integer`` () =
        let remaining = "DLA: #{Random(int,-10,0)}, fortune: Random(float, 1000.0, 100000.0)"

        let returnString = replaceRandomInt randomIntRegex remaining

        let firstPart = returnString.Substring(0, 5)
        let randomPart = Convert.ToInt32 (returnString.Split(',').[0].Split(' ').[1])

        Assert.AreEqual("DLA: ", firstPart)
        Assert.IsTrue(-10 <= randomPart && randomPart <= 0)

[<TestClass>]
type ``replaceRandomIntWithStep should`` () =

    let randomIntWithStepSizeRegex = Regex "#{Random\(\s?int\s?,\s?(-?\d+)\s?,\s?(-?\d+)\s?,\s?(-?\d+)\s?\)}"

    [<TestMethod>]
    member __.``return find and replace #{Random(int, 10, 100)} in a string with a random integer`` () =
        let remaining = "Age: #{Random(int, 10, 20, 100)}, fortune: Random(float, 1000.0, 100000.0)"

        let returnString = replaceRandomIntWithStep randomIntWithStepSizeRegex remaining

        let firstPart = returnString.Substring(0, 5)
        let randomPart = Convert.ToInt32 (returnString.Split(',').[0].Split(' ').[1])

        Assert.AreEqual("Age: ", firstPart)
        Assert.IsTrue(randomPart = 10 || randomPart = 30 || randomPart = 50 || randomPart = 70 || randomPart = 90)

    [<TestMethod>]
    member __.``return find and replace #{Random(int,-10,5,0)} in a string with a random integer`` () =
        let remaining = "DLA: #{Random(int,-10,5,0)}, fortune: Random(float, 1000.0, 100000.0)"

        let returnString = replaceRandomIntWithStep randomIntWithStepSizeRegex remaining 

        let firstPart = returnString.Substring(0, 5)
        let randomPart = Convert.ToInt32 (returnString.Split(',').[0].Split(' ').[1])

        Assert.AreEqual("DLA: ", firstPart)
        Assert.IsTrue(randomPart = -10 || randomPart = -5 || randomPart = 0)

    [<TestMethod>]
    member __.``return find and replace #{Random(int, 10, 20, 100)} in a string with a random integer`` () =
        let remaining = "DLA: #{Random(int, 10, 20, 100)}, fortune: Random(float, 1000.0, 100000.0)"

        let returnString = replaceRandomIntWithStep randomIntWithStepSizeRegex remaining 

        let firstPart = returnString.Substring(0, 5)
        let randomPart = Convert.ToInt32 (returnString.Split(',').[0].Split(' ').[1])

        Assert.AreEqual("DLA: ", firstPart)
        Assert.IsTrue(randomPart = 10 || randomPart = 30 || randomPart = 50 || randomPart = 70 || randomPart = 90)
