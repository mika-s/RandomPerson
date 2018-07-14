﻿namespace Tests

open System
open Microsoft.VisualStudio.TestTools.UnitTesting
open TemplatePrint
open TestData

[<TestClass>]
type ``parseOrdinaryReplaces should`` () =

    [<TestMethod>]
    member this.``return a string with #{SSN} replaced with SSN`` () =
        let person = getTestPerson ()
        let replaced = parseOrdinaryReplaces "SSN: #{SSN}" person

        let expectedString = sprintf "SSN: %s" person.SSN

        Assert.AreEqual(expectedString, replaced)

[<TestClass>]
type ``cleanupNumber should`` () =

    [<TestMethod>]
    member this.``return "123" when given " 123"`` () =
        let clean = cleanupValue " 123"

        Assert.AreEqual("123", clean)

    [<TestMethod>]
    member this.``return "123" when given "123 "`` () =
        let clean = cleanupValue "123 "

        Assert.AreEqual("123", clean)
        

[<TestClass>]
type ``getValueForRandomInt should`` () =

    [<TestMethod>]
    member this.``return a random integer between 0 and 100 when given Random(int, 0, 100)`` () =
        let randomNumber = getValueForRandomInt "Random(int, 0, 100)"

        Assert.IsTrue(0 <= randomNumber && randomNumber < 100)

    [<TestMethod>]
    member this.``return a random integer between 50 and 200 when given Random(int,50,200)`` () =
        let randomNumber = getValueForRandomInt "Random(int,50,200)"

        Assert.IsTrue(50 <= randomNumber && randomNumber < 200)

    [<TestMethod>]
    member this.``return a random integer between -100 and 1000 when given Random(int,-100,1000)`` () =
        let randomNumber = getValueForRandomInt "Random(int,-100,1000)"

        Assert.IsTrue(-100 <= randomNumber && randomNumber < 1000)

    [<TestMethod>]
    member this.``return a random integer between -20 and -10 when given Random(int,-20,-10)`` () =
        let randomNumber = getValueForRandomInt "Random(int,-20,-10)"

        Assert.IsTrue(-20 <= randomNumber && randomNumber < -10)

[<TestClass>]
type ``getValueForRandomFloat should`` () =

    [<TestMethod>]
    member this.``return a random float between 0.0 and 100.0 when given Random(float, 0, 100)`` () =
        let randomNumber = getValueForRandomFloat "Random(float, 0, 100)"

        Assert.IsTrue(0.0 <= randomNumber && randomNumber < 100.0)

    [<TestMethod>]
    member this.``return a random float between 50.0 and 200.0 when given Random(float,50.0,200.0)`` () =
        let randomNumber = getValueForRandomFloat "Random(float,50.0,200.0)"

        Assert.IsTrue(50.0 <= randomNumber && randomNumber < 200.0)

    [<TestMethod>]
    member this.``return a random float between -100.0 and 1000.0 when given Random(float,-100.0,1000.0)`` () =
        let randomNumber = getValueForRandomFloat "Random(float,-100.0,1000.0)"

        Assert.IsTrue(-100.0 <= randomNumber && randomNumber < 1000.0)

    [<TestMethod>]
    member this.``return a random float between -100.0 and 1000.0 when given Random(float,-100,1000)`` () =
        let randomNumber = getValueForRandomFloat "Random(float,-100,1000)"

        Assert.IsTrue(-100.0 <= randomNumber && randomNumber < 1000.0)

    [<TestMethod>]
    member this.``return a random float between -1000.0 and 1000.0 when given Random(float:5,-1000,1000)`` () =
        let randomNumber = getValueForRandomFloat "Random(float:5,-100,1000)"

        Assert.IsTrue(-1000.0 <= randomNumber && randomNumber < 1000.0)

    [<TestMethod>]
    member this.``return a random float between -2000.0 and 1000.0 when given Random(float:1,-2000,1000)`` () =
        let randomNumber = getValueForRandomFloat "Random(float:1,-100,1000)"

        Assert.IsTrue(-2000.0 <= randomNumber && randomNumber < 1000.0)

[<TestClass>]
type ``getNumbersAfterDecimal should`` () =

    [<TestMethod>]
    member this.``return 2 when given Random(float:2, 0, 100)`` () =
        let numberOfDecimals = getNumbersAfterDecimal "Random(float:2, 0, 100)"

        Assert.AreEqual(2, numberOfDecimals)

    [<TestMethod>]
    member this.``return 3 when given Random(float:3, -1000, 100)`` () =
        let numberOfDecimals = getNumbersAfterDecimal "Random(float:3, -1000, 100)"

        Assert.AreEqual(3, numberOfDecimals)

    [<TestMethod>]
    member this.``return 1 when given Random(float:1,10,20)`` () =
        let numberOfDecimals = getNumbersAfterDecimal "Random(float:1,10,20)"

        Assert.AreEqual(1, numberOfDecimals)

    [<TestMethod>]
    member this.``return 2 when given Random(float:2,-10,-9)`` () =
        let numberOfDecimals = getNumbersAfterDecimal "Random(float:2,-10,-9)"

        Assert.AreEqual(2, numberOfDecimals)

[<TestClass>]
type ``replaceRandomInt should`` () =

    let randomIntPattern = "#{Random\(\s?int\s?,\s?-?\d+\s?,\s?-?\d+\s?\)}"

    [<TestMethod>]
    member this.``return find and replace #{Random(int, 10, 100)} in a string with a random integer`` () =
        let remaining = "Age: #{Random(int, 10, 100)}, fortune: Random(float, 1000.0, 100000.0)"
        let randomString = "Random(int, 10, 100)"

        let returnString = replaceRandomInt remaining randomIntPattern randomString

        let firstPart = returnString.Substring(0, 5)
        let randomPart = Convert.ToInt32 (returnString.Split(',').[0].Split(' ').[1])

        Assert.AreEqual("Age: ", firstPart)
        Assert.IsTrue(10 <= randomPart && randomPart < 100)

    [<TestMethod>]
    member this.``return find and replace #{Random(int,-10,0)} in a string with a random integer`` () =
        let remaining = "DLA: #{Random(int,-10,0)}, fortune: Random(float, 1000.0, 100000.0)"
        let randomString = "Random(int,-10,0)"

        let returnString = replaceRandomInt remaining randomIntPattern randomString

        let firstPart = returnString.Substring(0, 5)
        let randomPart = Convert.ToInt32 (returnString.Split(',').[0].Split(' ').[1])

        Assert.AreEqual("DLA: ", firstPart)
        Assert.IsTrue(-10 <= randomPart && randomPart < 0)

[<TestClass>]
type ``replaceRandomFloat should`` () =

    let randomFloatPattern = "#{Random\(\s?float\s?,\s?(-?\d+.\d+|-?\d+)\s?,\s?(-?\d+.\d+|-?\d+)\s?\)}"

    [<TestMethod>]
    member this.``return find and replace #{Random(float, 1000, 100000)} in a string with a random float`` () =
        let remaining = "Income: #{Random(float, 1000, 100000)}, married: Random(switch,true,false)"
        let randomString = "Random(float, 1000, 100000)"

        let returnString = replaceRandomFloat remaining randomFloatPattern randomString

        let firstPart = returnString.Substring(0, 8)
        let randomPart = float (returnString.Split(',').[0].Split(' ').[1])

        Assert.AreEqual("Income: ", firstPart)
        Assert.IsTrue(1000.0 <= randomPart && randomPart < 100000.0)

    [<TestMethod>]
    member this.``return find and replace #{Random(float,-1000, 0)} in a string with a random float`` () =
        let remaining = "Income: #{Random(float,-1000, 0)}, married: Random(switch,true,false)"
        let randomString = "Random(float,-1000, 0)"

        let returnString = replaceRandomFloat remaining randomFloatPattern randomString

        let firstPart = returnString.Substring(0, 8)
        let randomPart = float (returnString.Split(',').[0].Split(' ').[1])

        Assert.AreEqual("Income: ", firstPart)
        Assert.IsTrue(-1000.0 <= randomPart && randomPart < 0.0)

[<TestClass>]
type ``replaceRandomSwitch should`` () =

    let randomSwitchPattern = "#{Random\(\s?switch\s?,(\s?['\w\,\\\/]+\s?,)+\s?['\w\,\\\/]+\s?\)}"

    [<TestMethod>]
    member this.``return find and replace #{Random(switch, true, false)} in a string with either true or false`` () =
        let remaining = "Married: #{Random(switch, true, false)}, income: #{Random(float, 1000, 100000)}, "
        let randomString = "Random(switch, true, false)"

        let returnString = replaceRandomSwitch remaining randomSwitchPattern randomString

        let firstPart = returnString.Substring(0, 9)
        let randomPart = returnString.Split(',').[0].Split(' ').[1]

        Assert.AreEqual("Married: ", firstPart)
        Assert.IsTrue(randomPart = "true" || randomPart = "false")

    [<TestMethod>]
    member this.``return find and replace #{Random(switch, yes, no, maybe)} in a string with either yes, no or maybe`` () =
        let remaining = "Married: #{Random(switch, yes, no, maybe)}, income: #{Random(float, 1000, 100000)}, "
        let randomString = "Random(switch, yes, no, maybe)"

        let returnString = replaceRandomSwitch remaining randomSwitchPattern randomString

        let firstPart = returnString.Substring(0, 9)
        let randomPart = returnString.Split(',').[0].Split(' ').[1]

        Assert.AreEqual("Married: ", firstPart)
        Assert.IsTrue(randomPart = "yes" || randomPart = "no" || randomPart = "maybe")

    [<TestMethod>]
    member this.``return find and replace #{Random(switch,yes,no,maybe)} in a string with either yes, no or maybe`` () =
        let remaining = "Married: #{Random(switch,yes,no,maybe)}, income: #{Random(float, 1000, 100000)}, "
        let randomString = "Random(switch,yes,no,maybe)"

        let returnString = replaceRandomSwitch remaining randomSwitchPattern randomString

        let firstPart = returnString.Substring(0, 9)
        let randomPart = returnString.Split(',').[0].Split(' ').[1]

        Assert.AreEqual("Married: ", firstPart)
        Assert.IsTrue(randomPart = "yes" || randomPart = "no" || randomPart = "maybe")

    [<TestMethod>]
    member this.``return find and replace #{Random(switch,one,two,three,four)} in a string with either one, two, three or four`` () =
        let remaining = "Married: #{Random(switch,one,two,three,four)}, income: #{Random(float, 1000, 100000)}, "
        let randomString = "Random(switch,one,two,three,four)"

        let returnString = replaceRandomSwitch remaining randomSwitchPattern randomString

        let firstPart = returnString.Substring(0, 9)
        let randomPart = returnString.Split(',').[0].Split(' ').[1]

        Assert.AreEqual("Married: ", firstPart)
        Assert.IsTrue(randomPart = "one" || randomPart = "two" || randomPart = "three" || randomPart = "four")

    [<TestMethod>]
    member this.``return find and replace #{Random(switch,one\,one,two,three,four)} in a string with either one,one; two; three or four`` () =
        let remaining = "Married: #{Random(switch,one\,one,two,three,four)}, income: #{Random(float, 1000, 100000)}, "
        let randomString = "Random(switch,one\,one,two,three,four)"

        let returnString = replaceRandomSwitch remaining randomSwitchPattern randomString

        let firstPart = returnString.Substring(0, 9)
        let randomPartForNonSpecial = returnString.Split(',').[0].Split(' ').[1]
        let randomPartForSpecial    = returnString.Split(':').[1].Split(',') |> Array.map (fun x -> x.Trim())

        Assert.AreEqual("Married: ", firstPart)

        match randomPartForNonSpecial with
        | "two" | "three" | "four" -> Assert.IsTrue(true)
        | _ -> Assert.IsTrue(randomPartForSpecial.[0] = "one" && randomPartForSpecial.[1] = "one")

    [<TestMethod>]
    member this.``return find and replace #{Random(switch, one\,one\, two, two, three, four)} in a string with either one,one, two; two; three or four`` () =
        let remaining = "Married: #{Random(switch, one\,one\, two, two, three, four)}, income: #{Random(float, 1000, 100000)}, "
        let randomString = "Random(switch, one\,one\, two, two, three, four)"

        let returnString = replaceRandomSwitch remaining randomSwitchPattern randomString

        let firstPart = returnString.Substring(0, 9)
        let randomPartForNonSpecial = returnString.Split(',').[0].Split(' ').[1]
        let randomPartForSpecial    = returnString.Split(':').[1].Split(',') |> Array.map (fun x -> x.Trim())

        Assert.AreEqual("Married: ", firstPart)

        match randomPartForNonSpecial with
        | "two" | "three" | "four" -> Assert.IsTrue(true)
        | _ -> Assert.IsTrue(randomPartForSpecial.[0] = "one" && randomPartForSpecial.[1] = "one" && randomPartForSpecial.[2] = "two")

    [<TestMethod>]
    member this.``return find and replace #{Random(switch, 'one\,one\', 'two\, two')} in a string with either 'one,one' or 'two, two'`` () =
        let remaining = "Married: #{Random(switch, 'one'\,'one\', 'two'\, 'two')}, income: #{Random(float, 1000, 100000)}, "
        let randomString = "Random(switch, 'one'\,'one\', 'two'\, 'two')"

        let returnString = replaceRandomSwitch remaining randomSwitchPattern randomString

        let firstPart = returnString.Substring(0, 9)
        let randomPart = returnString.Split(':').[1].Split(',') |> Array.map (fun x -> x.Trim())

        Assert.AreEqual("Married: ", firstPart)
        Assert.IsTrue((randomPart.[0] = "'one'" && randomPart.[1] = "'one'") ||  (randomPart.[0] = "'two'" && randomPart.[1] = "'two'"))

    [<TestMethod>]
    member this.``Temp test`` () =
        let remaining = "Married: #{Random(switch, 'K01'\,'K01_VG', 'K03'\,'K03_VP')}"
        let randomString = "Random(switch, 'K01'\,'K01_VG', 'K03'\,'K03_VP')"

        let returnString = replaceRandomSwitch remaining randomSwitchPattern randomString

        let firstPart = returnString.Substring(0, 9)
        let randomPart = returnString.Split(':').[1].Split(',') |> Array.map (fun x -> x.Trim())

        Assert.AreEqual("Married: ", firstPart)
        Assert.IsTrue((randomPart.[0] = "'K01'" && randomPart.[1] = "'K01_VG'") ||  (randomPart.[0] = "'K03'" && randomPart.[1] = "'K03_VP'"))

    [<TestMethod>]
    member this.``return find and replace #{Random(switch, 'one', 'two')} in a string with either 'one' or 'two'`` () =
        let remaining = "Married: #{Random(switch, 'one', 'two')}, income: #{Random(float, 1000, 100000)}, "
        let randomString = "Random(switch, 'one', 'two')"

        let returnString = replaceRandomSwitch remaining randomSwitchPattern randomString

        let firstPart = returnString.Substring(0, 9)
        let randomPart = returnString.Split(',').[0].Split(' ').[1]

        Assert.AreEqual("Married: ", firstPart)
        Assert.IsTrue(randomPart = "'one'" || randomPart = "'two'")
        