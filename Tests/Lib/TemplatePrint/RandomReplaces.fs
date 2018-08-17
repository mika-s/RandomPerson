namespace Tests

open System
open System.Text.RegularExpressions
open Microsoft.VisualStudio.TestTools.UnitTesting
open RandomReplaces

[<TestClass>]
type ``replace randomInt should`` () =

    let randomIntRegex = Regex "#{Random\(\s?int\s?,\s?(-?\d+)\s?,\s?(-?\d+)\s?\)}"

    [<TestMethod>]
    member __.``return find and replace #{Random(int, 10, 100)} in a string with a random integer`` () =
        let remaining = "Age: #{Random(int, 10, 100)}, fortune: Random(float, 1000.0, 100000.0)"

        let returnString = replace randomInt randomIntRegex remaining

        let firstPart = returnString.Substring(0, 5)
        let randomPart = Convert.ToInt32 (returnString.Split(',').[0].Split(' ').[1])

        Assert.AreEqual("Age: ", firstPart)
        Assert.IsTrue(10 <= randomPart && randomPart <= 100)

    [<TestMethod>]
    member __.``return find and replace #{Random(int,-10,0)} in a string with a random integer`` () =
        let remaining = "DLA: #{Random(int,-10,0)}, fortune: Random(float, 1000.0, 100000.0)"

        let returnString = replace randomInt randomIntRegex remaining

        let firstPart = returnString.Substring(0, 5)
        let randomPart = Convert.ToInt32 (returnString.Split(',').[0].Split(' ').[1])

        Assert.AreEqual("DLA: ", firstPart)
        Assert.IsTrue(-10 <= randomPart && randomPart <= 0)

[<TestClass>]
type ``replace randomIntWithStep should`` () =

    let randomIntWithStepSizeRegex = Regex "#{Random\(\s?int\s?,\s?(-?\d+)\s?,\s?(-?\d+)\s?,\s?(-?\d+)\s?\)}"

    [<TestMethod>]
    member __.``return find and replace #{Random(int, 10, 100)} in a string with a random integer`` () =
        let remaining = "Age: #{Random(int, 10, 20, 100)}, fortune: Random(float, 1000.0, 100000.0)"

        let returnString = replace randomIntWithStep randomIntWithStepSizeRegex remaining

        let firstPart = returnString.Substring(0, 5)
        let randomPart = Convert.ToInt32 (returnString.Split(',').[0].Split(' ').[1])

        Assert.AreEqual("Age: ", firstPart)
        Assert.IsTrue(randomPart = 10 || randomPart = 30 || randomPart = 50 || randomPart = 70 || randomPart = 90)

    [<TestMethod>]
    member __.``return find and replace #{Random(int,-10,5,0)} in a string with a random integer`` () =
        let remaining = "DLA: #{Random(int,-10,5,0)}, fortune: Random(float, 1000.0, 100000.0)"

        let returnString = replace randomIntWithStep randomIntWithStepSizeRegex remaining 

        let firstPart = returnString.Substring(0, 5)
        let randomPart = Convert.ToInt32 (returnString.Split(',').[0].Split(' ').[1])

        Assert.AreEqual("DLA: ", firstPart)
        Assert.IsTrue(randomPart = -10 || randomPart = -5 || randomPart = 0)

    [<TestMethod>]
    member __.``return find and replace #{Random(int, 10, 20, 100)} in a string with a random integer`` () =
        let remaining = "DLA: #{Random(int, 10, 20, 100)}, fortune: Random(float, 1000.0, 100000.0)"

        let returnString = replace randomIntWithStep randomIntWithStepSizeRegex remaining 

        let firstPart = returnString.Substring(0, 5)
        let randomPart = Convert.ToInt32 (returnString.Split(',').[0].Split(' ').[1])

        Assert.AreEqual("DLA: ", firstPart)
        Assert.IsTrue(randomPart = 10 || randomPart = 30 || randomPart = 50 || randomPart = 70 || randomPart = 90)

[<TestClass>]
type ``replace randomFloat should`` () =

    let randomFloatRegex = Regex "#{Random\(\s?float\s?,\s?(-?\d+.\d+|-?\d+)\s?,\s?(-?\d+.\d+|-?\d+)\s?\)}"

    [<TestMethod>]
    member __.``return find and replace #{Random(float, 1000, 100000)} in a string with a random float`` () =
        let remaining = "Income: #{Random(float, 1000, 100000)}, married: Random(switch,true,false)"

        let returnString = replace randomFloat randomFloatRegex remaining

        let firstPart = returnString.Substring(0, 8)
        let randomPart = float (returnString.Split(',').[0].Split(' ').[1])

        Assert.AreEqual("Income: ", firstPart)
        Assert.IsTrue(1000.0 <= randomPart && randomPart <= 100000.0)

    [<TestMethod>]
    member __.``return find and replace #{Random(float,-1000, 0)} in a string with a random float`` () =
        let remaining = "Income: #{Random(float,-1000, 0)}, married: Random(switch,true,false)"

        let returnString = replace randomFloat randomFloatRegex remaining

        let firstPart = returnString.Substring(0, 8)
        let randomPart = float (returnString.Split(',').[0].Split(' ').[1])

        Assert.AreEqual("Income: ", firstPart)
        Assert.IsTrue(-1000.0 <= randomPart && randomPart <= 0.0)

    [<TestMethod>]
    member __.``return find and replace #{Random(float,-20.0, 20.0)} in a string with a random float`` () =
        let remaining = "Income: #{Random(float,-20.0, 20.0)}, married: Random(switch,true,false)"

        let returnString = replace randomFloat randomFloatRegex remaining

        let firstPart = returnString.Substring(0, 8)
        let randomPart = float (returnString.Split(',').[0].Split(' ').[1])

        Assert.AreEqual("Income: ", firstPart)
        Assert.IsTrue(-20.0 <= randomPart && randomPart <= 20.0)

    [<TestMethod>]
    member __.``return find and replace #{Random(float,-1.01,1)} in a string with a random float`` () =
        let remaining = "Income: #{Random(float,-1.01,1)}, married: Random(switch,true,false)"

        let returnString = replace randomFloat randomFloatRegex remaining

        let firstPart = returnString.Substring(0, 8)
        let randomPart = float (returnString.Split(',').[0].Split(' ').[1])

        Assert.AreEqual("Income: ", firstPart)
        Assert.IsTrue(-1.01 <= randomPart && randomPart <= 1.0)

[<TestClass>]
type ``replace randomFloatWithStep should`` () =

    let randomFloatWithStepRegex = Regex "#{Random\(\s?float\s?,\s?(-?\d+.\d+|-?\d+)\s?,\s?(-?\d+.\d+|-?\d+)\s?,\s?(-?\d+.\d+|-?\d+)\s?\)}"

    [<TestMethod>]
    member __.``return find and replace #{Random(float, 2.0, 0.5, 3.0)} in a string with a random float`` () =
        let remaining = "Income: #{Random(float, 2.0, 0.5, 3.0)}, married: Random(switch,true,false)"

        let returnString = replace randomFloatWithStep randomFloatWithStepRegex remaining

        let firstPart = returnString.Substring(0, 8)
        let randomPart = float (returnString.Split(',').[0].Split(' ').[1])

        Assert.AreEqual("Income: ", firstPart)
        Assert.IsTrue(randomPart = 2.0 || randomPart = 2.5 || randomPart = 3.0)

    [<TestMethod>]
    member __.``return find and replace #{Random(float, 0.0, 2.0, 10.0)} in a string with a random float`` () =
        let remaining = "Income: #{Random(float, 0.0, 2.0, 10.0)}, married: Random(switch,true,false)"

        let returnString = replace randomFloatWithStep randomFloatWithStepRegex remaining

        let firstPart = returnString.Substring(0, 8)
        let randomPart = float (returnString.Split(',').[0].Split(' ').[1])

        Assert.AreEqual("Income: ", firstPart)
        Assert.IsTrue(randomPart = 0.0 || randomPart = 2.0 || randomPart = 4.0 || randomPart = 6.0 || randomPart = 8.0 || randomPart = 10.0)

    [<TestMethod>]
    member __.``return find and replace #{Random(float,-20.0,10, 20.0)} in a string with a random float`` () =
        let remaining = "Income: #{Random(float,-20.0,10, 20.0)}, married: Random(switch,true,false)"

        let returnString = replace randomFloatWithStep randomFloatWithStepRegex remaining

        let firstPart = returnString.Substring(0, 8)
        let randomPart = float (returnString.Split(',').[0].Split(' ').[1])

        Assert.AreEqual("Income: ", firstPart)
        Assert.IsTrue(randomPart = -20.0 || randomPart = -10.0 || randomPart = 0.0 || randomPart = 10.0 || randomPart = 20.0)

    [<TestMethod>]
    member __.``return find and replace #{Random(float,-1.05,0.01,-1.00)} in a string with a random float`` () =
        let remaining = "Income: #{Random(float,-1.01,0.01,-1.00)}, married: Random(switch,true,false)"

        let returnString = replace randomFloatWithStep randomFloatWithStepRegex remaining

        let firstPart = returnString.Substring(0, 8)
        let randomPart = float (returnString.Split(',').[0].Split(' ').[1])

        Assert.AreEqual("Income: ", firstPart)
        Assert.IsTrue(randomPart = -1.05 || randomPart = -1.04 || randomPart = -1.03 || randomPart = -1.02 || randomPart = -1.01 || randomPart = -1.00)

[<TestClass>]
type ``replace randomFloatWithDecimalsWithStep should`` () =

    let randomFloatWithDecimalsWithStepRegex  = Regex "#{Random\(\s?float:(\d+)\s?,\s?(-?\d+.\d+|-?\d+)\s?,\s?(-?\d+.\d+|-?\d+)\s?,\s?(-?\d+.\d+|-?\d+)\s?\)}"

    [<TestMethod>]
    member __.``return find and replace #{Random(float, 2.0, 0.5, 3.0)} in a string with a random float`` () =
        let remaining = "Income: #{Random(float:1, 2.0, 0.5, 3.0)}, married: Random(switch,true,false)"

        let returnString = replace randomFloatWithDecimalsWithStep randomFloatWithDecimalsWithStepRegex remaining

        let firstPart = returnString.Substring(0, 8)
        let randomPartAsString = returnString.Split(',').[0].Split(' ').[1]
        let randomPart = float randomPartAsString

        Assert.AreEqual("Income: ", firstPart)
        Assert.IsTrue(randomPartAsString = "2.0" || randomPartAsString = "2.5" || randomPartAsString = "3.0")
        Assert.IsTrue(randomPart = 2.0 || randomPart = 2.5 || randomPart = 3.0)

    [<TestMethod>]
    member __.``return find and replace #{Random(float, 0.0, 2.0, 10.0)} in a string with a random float`` () =
        let remaining = "Income: #{Random(float:2, 0.0, 2.0, 10.0)}, married: Random(switch,true,false)"

        let returnString = replace randomFloatWithDecimalsWithStep randomFloatWithDecimalsWithStepRegex remaining

        let firstPart = returnString.Substring(0, 8)
        let randomPartAsString = returnString.Split(',').[0].Split(' ').[1]
        let randomPart = float randomPartAsString

        Assert.AreEqual("Income: ", firstPart)
        Assert.IsTrue(randomPartAsString = "0.00" || randomPartAsString = "2.00" || randomPartAsString = "4.00" ||
            randomPartAsString = "6.00" || randomPartAsString = "8.00" || randomPartAsString = "10.00")
        Assert.IsTrue(randomPart = 0.0 || randomPart = 2.0 || randomPart = 4.0 || randomPart = 6.0 || randomPart = 8.0 || randomPart = 10.0)

    [<TestMethod>]
    member __.``return find and replace #{Random(float,-20.0,10, 20.0)} in a string with a random float`` () =
        let remaining = "Income: #{Random(float:3,-20.0,10, 20.0)}, married: Random(switch,true,false)"

        let returnString = replace randomFloatWithDecimalsWithStep randomFloatWithDecimalsWithStepRegex remaining

        let firstPart = returnString.Substring(0, 8)
        let randomPartAsString = returnString.Split(',').[0].Split(' ').[1]
        let randomPart = float randomPartAsString

        Assert.AreEqual("Income: ", firstPart)
        Assert.IsTrue(randomPartAsString = "-20.000" || randomPartAsString = "-10.000" || randomPartAsString = "0.000" ||
            randomPartAsString = "10.000" || randomPartAsString = "20.000")
        Assert.IsTrue(randomPart = -20.0 || randomPart = -10.0 || randomPart = 0.0 || randomPart = 10.0 || randomPart = 20.0)

    [<TestMethod>]
    member __.``return find and replace #{Random(float,-1.05,0.01,-1.00)} in a string with a random float`` () =
        let remaining = "Income: #{Random(float:2,-1.01,0.01,-1.00)}, married: Random(switch,true,false)"

        let returnString = replace randomFloatWithDecimalsWithStep randomFloatWithDecimalsWithStepRegex remaining

        let firstPart = returnString.Substring(0, 8)
        let randomPartAsString = returnString.Split(',').[0].Split(' ').[1]
        let randomPart = float randomPartAsString

        Assert.AreEqual("Income: ", firstPart)
        Assert.IsTrue(randomPartAsString = "-1.05" || randomPartAsString = "-1.04" || randomPartAsString = "-1.03" ||
            randomPartAsString = "-1.02" || randomPartAsString = "-1.01" || randomPartAsString = "-1.00")
        Assert.IsTrue(randomPart = -1.05 || randomPart = -1.04 || randomPart = -1.03 || randomPart = -1.02 || randomPart = -1.01 || randomPart = -1.00)

[<TestClass>]
type ``replace randomSwitch should`` () =

    let randomSwitchRegex = Regex "#{Random\((?:switch,)\s?(?:\s*'([\w- \\\/,]+)',?){2,}\)}"

    [<TestMethod>]
    member __.``return find and replace #{Random(switch, 'true', 'false')} in a string with either true or false`` () =
        let remaining = "Married: #{Random(switch, 'true', 'false')}, income: #{Random(float, 1000, 100000)}, "

        let returnString = replace randomSwitch randomSwitchRegex remaining

        let firstPart = returnString.Substring(0, 9)
        let randomPart = returnString.Split(',').[0].Split(' ').[1]

        Assert.AreEqual("Married: ", firstPart)
        Assert.IsTrue(randomPart = "true" || randomPart = "false")

    [<TestMethod>]
    member __.``return find and replace #{Random(switch, 'yes', 'no', 'maybe')} in a string with either yes, no or maybe`` () =
        let remaining = "Married: #{Random(switch, 'yes', 'no', 'maybe')}, income: #{Random(float, 1000, 100000)}, "

        let returnString = replace randomSwitch randomSwitchRegex remaining

        let firstPart = returnString.Substring(0, 9)
        let randomPart = returnString.Split(',').[0].Split(' ').[1]

        Assert.AreEqual("Married: ", firstPart)
        Assert.IsTrue(randomPart = "yes" || randomPart = "no" || randomPart = "maybe")

    [<TestMethod>]
    member __.``return find and replace #{Random(switch,'yes','no','maybe')} in a string with either yes, no or maybe`` () =
        let remaining = "Married: #{Random(switch,'yes','no','maybe')}, income: #{Random(float, 1000, 100000)}, "

        let returnString = replace randomSwitch randomSwitchRegex remaining

        let firstPart = returnString.Substring(0, 9)
        let randomPart = returnString.Split(',').[0].Split(' ').[1]

        Assert.AreEqual("Married: ", firstPart)
        Assert.IsTrue(randomPart = "yes" || randomPart = "no" || randomPart = "maybe")

    [<TestMethod>]
    member __.``return find and replace #{Random(switch,'one','two','three','four')} in a string with either one,one; two; three or four`` () =
        let remaining = "Married: #{Random(switch,'one','two','three','four')}, income: #{Random(float, 1000, 100000)}, "

        let returnString = replace randomSwitch randomSwitchRegex remaining

        let firstPart = returnString.Substring(0, 9)
        let randomPart = returnString.Split(',').[0].Split(' ').[1]

        Assert.AreEqual("Married: ", firstPart)

        match randomPart with
        | "one" | "two" | "three" | "four" -> Assert.IsTrue(true)
        | _                                -> Assert.IsTrue(false)

    [<TestMethod>]
    member __.``return find and replace #{Random(switch,'one', 'two', 'three', 'four')} in a string with either one, two, three or four`` () =
        let remaining = "Married: #{Random(switch,'one', 'two', 'three', 'four')}, income: #{Random(float, 1000, 100000)}, "

        let returnString = replace randomSwitch randomSwitchRegex remaining

        let firstPart = returnString.Substring(0, 9)
        let randomPart = returnString.Split(',').[0].Split(' ').[1]

        Assert.AreEqual("Married: ", firstPart)

        match randomPart with
        | "one" |"two" | "three" | "four" -> Assert.IsTrue(true)
        | _                               -> Assert.IsTrue(false)

    [<TestMethod>]
    member __.``return find and replace #{Random(switch, 'one, one', 'two, two')} in a string with either 'one, one' or 'two, two'`` () =
        let remaining = "Married: #{Random(switch, 'one, one', 'two, two')}; income: #{Random(float, 1000, 100000)}, "

        let returnString = replace randomSwitch randomSwitchRegex remaining

        let firstPart = returnString.Substring(0, 9)
        let randomPart = returnString.Split(':').[1].Split(';') |> Array.map (fun x -> x.Trim())

        Assert.AreEqual("Married: ", firstPart)
        Assert.IsTrue(randomPart.[0] = "one, one" ||  randomPart.[0] = "two, two")

    [<TestMethod>]
    member __.``find and replace #{Random(switch, 'one/one', 'two/two')} in a string with either 'one/one' or 'two/two'`` () =
        let remaining = "Married: #{Random(switch, 'one/one', 'two/two')}, income: #{Random(float, 1000, 100000)}, "

        let returnString = replace randomSwitch randomSwitchRegex remaining

        let firstPart = returnString.Substring(0, 9)
        let randomPart = returnString.Split(',').[0].Split(' ').[1]

        Assert.AreEqual("Married: ", firstPart)
        Assert.IsTrue(randomPart = "one/one" || randomPart = "two/two")

[<TestClass>]
type ``replace randomNormallyDistributedInt should`` () =

    let randomNdIntRegex = Regex "#{Random\(\s?nd_int\s?,\s?(-?\d+)\s?,\s?(-?\d+)\s?\)}"

    [<TestMethod>]
    member __.``return find and replace #{Random(nd_int,20,0)} in a string with a normally distributed random int`` () =
        let remaining = "Value: #{Random(nd_int,20,0)}, married: Random(switch,true,false)"

        let returnString = replace randomNormallyDistributedInt randomNdIntRegex remaining

        let firstPart = returnString.Substring(0, 7)
        let randomPart = int (returnString.Split(',').[0].Split(' ').[1])

        Assert.AreEqual("Value: ", firstPart)
        Assert.AreEqual(20, randomPart)

    [<TestMethod>]
    member __.``return find and replace #{Random(nd_int,100,1)} in a string with a normally distributed random int`` () =
        let remaining = "Value: #{Random(nd_int,100,1)}, married: Random(switch,true,false)"

        let returnString = replace randomNormallyDistributedInt randomNdIntRegex remaining

        let firstPart = returnString.Substring(0, 7)
        let randomPart = int (returnString.Split(',').[0].Split(' ').[1])

        Assert.AreEqual("Value: ", firstPart)
        Assert.IsTrue(0 < randomPart && randomPart < 200)

[<TestClass>]
type ``replace randomNdIntWithStepSizeRegex should`` () =

    let randomNdIntWithStepSizeRegex = Regex "#{Random\(\s?nd_int\s?,\s?(-?\d+)\s?,\s?(-?\d+)\s?,\s?(-?\d+)\s?\)}"

    [<TestMethod>]
    member __.``return find and replace #{Random(nd_int,20,0,5)} in a string with a normally distributed random int`` () =
        let remaining = "Value: #{Random(nd_int,20,0,5)}, married: Random(switch,true,false)"

        let returnString = replace randomNormallyDistributedIntWithStep randomNdIntWithStepSizeRegex remaining

        let firstPart = returnString.Substring(0, 7)
        let randomPart = int (returnString.Split(',').[0].Split(' ').[1])

        Assert.AreEqual("Value: ", firstPart)
        Assert.AreEqual(20, randomPart)

    [<TestMethod>]
    member __.``return find and replace #{Random(nd_int,100,1,5)} in a string with a normally distributed random int`` () =
        let remaining = "Value: #{Random(nd_int,100,1,5)}, married: Random(switch,true,false)"

        let returnString = replace randomNormallyDistributedIntWithStep randomNdIntWithStepSizeRegex remaining

        let firstPart = returnString.Substring(0, 7)
        let randomPart = int (returnString.Split(',').[0].Split(' ').[1])

        Assert.AreEqual("Value: ", firstPart)
        Assert.IsTrue(0 < randomPart && randomPart < 200)
        Assert.IsTrue(randomPart % 5 = 0)

[<TestClass>]
type ``replace randomNormallyDistributedFloat should`` () =

    let randomNdFloatRegex = Regex "#{Random\(\s?nd_float\s?,\s?(-?\d+\.\d+|-?\d+)\s?,\s?(-?\d+\.\d+|-?\d+)\s?\)}"

    [<TestMethod>]
    member __.``return find and replace #{Random(nd_float,20.0, 0.0)} in a string with a normally distributed random float`` () =
        let remaining = "Value: #{Random(nd_float,20.0, 0.0)}, married: Random(switch,true,false)"

        let returnString = replace randomNormallyDistributedFloat randomNdFloatRegex remaining

        let firstPart = returnString.Substring(0, 7)
        let randomPart = float (returnString.Split(',').[0].Split(' ').[1])

        Assert.AreEqual("Value: ", firstPart)
        Assert.IsTrue(19.99 <= randomPart && randomPart <= 20.01)
