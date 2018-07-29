namespace Tests

open System
open Microsoft.VisualStudio.TestTools.UnitTesting
open RandomPersonLib
open CommonTemplatePrint
open SpecialGenderReplaces
open SpecialBirthDateReplaces
open OrdinaryReplaces
open RandomReplaces
open TestData
open System.Text.RegularExpressions


[<TestClass>]
type ``parseOrdinaryReplaces should`` () =

    [<TestMethod>]
    member __.``return a string with #{SSN} replaced by SSN`` () =
        let person = getTestPerson ()
        let replaced = performOrdinaryReplaces person "SSN: #{SSN}"

        let expectedString = sprintf "SSN: %s" person.SSN

        Assert.AreEqual(expectedString, replaced)

    [<TestMethod>]
    member __.``return a string with #{FirstName.ToLower()} replaced by the FirstName in all lower caps`` () =
        let person = getTestPerson ()
        let replaced = performOrdinaryReplaces person "First name: #{FirstName.ToLower()}"

        let expectedString = sprintf "First name: %s" (person.FirstName.ToLower())

        Assert.AreEqual(expectedString, replaced)

    [<TestMethod>]
    member __.``return a string with #{LastName.ToUpper()} replaced by the FirstName in all upper caps`` () =
        let person = getTestPerson ()
        let replaced = performOrdinaryReplaces person "Last name: #{LastName.ToUpper()}"

        let expectedString = sprintf "Last name: %s" (person.LastName.ToUpper())

        Assert.AreEqual(expectedString, replaced)

[<TestClass>]
type ``cleanupNumber should`` () =

    [<TestMethod>]
    member __.``return "123" when given " 123"`` () =
        let clean = cleanupValue " 123"

        Assert.AreEqual("123", clean)

    [<TestMethod>]
    member __.``return "123" when given "123 "`` () =
        let clean = cleanupValue "123 "

        Assert.AreEqual("123", clean)   

[<TestClass>]
type ``getValueForRandomInt should`` () =

    [<TestMethod>]
    member __.``return a random integer between 0 and 100 when given Random(int, 0, 100)`` () =
        let randomNumber = getValueForRandomInt "Random(int, 0, 100)"

        Assert.IsTrue(0 <= randomNumber && randomNumber <= 100)

    [<TestMethod>]
    member __.``return a random integer between 50 and 200 when given Random(int,50,200)`` () =
        let randomNumber = getValueForRandomInt "Random(int,50,200)"

        Assert.IsTrue(50 <= randomNumber && randomNumber <= 200)

    [<TestMethod>]
    member __.``return a random integer between -100 and 1000 when given Random(int,-100,1000)`` () =
        let randomNumber = getValueForRandomInt "Random(int,-100,1000)"

        Assert.IsTrue(-100 <= randomNumber && randomNumber <= 1000)

    [<TestMethod>]
    member __.``return a random integer between -20 and -10 when given Random(int,-20,-10)`` () =
        let randomNumber = getValueForRandomInt "Random(int,-20,-10)"

        Assert.IsTrue(-20 <= randomNumber && randomNumber <= -10)

[<TestClass>]
type ``getValueForRandomIntWithStep should`` () =

    [<TestMethod>]
    member __.``return a random integer between 0 and 100, with step 25, when given Random(int, 0, 25, 100)`` () =
        let randomNumber = getValueForRandomIntWithStep "Random(int, 0, 25, 100)"

        Assert.IsTrue(randomNumber = 0 || randomNumber = 25 || randomNumber = 50 || randomNumber = 75 || randomNumber = 100)

    [<TestMethod>]
    member __.``return a random integer between 50 and 200, with step 50, when given Random(int,50,50,200)`` () =
        let randomNumber = getValueForRandomIntWithStep "Random(int,50,50,200)"

        Assert.IsTrue(randomNumber = 50 || randomNumber = 100 || randomNumber = 150 || randomNumber = 200)

    [<TestMethod>]
    member __.``return a random integer between -500 and 1000, with step 500, when given Random(int,-500,500,1000)`` () =
        let randomNumber = getValueForRandomIntWithStep "Random(int,-500,500,1000)"

        Assert.IsTrue(randomNumber = -500 || randomNumber = 0 || randomNumber = 500 || randomNumber = 1000)

    [<TestMethod>]
    member __.``return a random integer between -20 and -10, with step 10, when given Random(int,-20,10,-10)`` () =
        let randomNumber = getValueForRandomIntWithStep "Random(int,-20,10,-10)"

        Assert.IsTrue(randomNumber = -20 || randomNumber = -10)

    [<TestMethod>]
    member __.``return a random integer between 0 and 100, with step 45, when given Random(int,0,45, 100)`` () =
        let randomNumber = getValueForRandomIntWithStep "Random(int,0,45, 100)"

        Assert.IsTrue(randomNumber = 0 || randomNumber = 45 || randomNumber = 90)

    [<TestMethod>]
    member __.``return a random integer between 20 and 100, with step 45, when given Random(int, 20,45, 100)`` () =
        let randomNumber = getValueForRandomIntWithStep "Random(int, 20,45, 100)"

        Assert.IsTrue(randomNumber = 20 || randomNumber = 65)

[<TestClass>]
type ``getValueForRandomFloat should`` () =

    [<TestMethod>]
    member __.``return a random float between 0,0 and 100,0 when given Random(float, 0, 100)`` () =
        let randomNumber = getValueForRandomFloat "Random(float, 0, 100)"

        Assert.IsTrue(0.0 <= randomNumber && randomNumber < 100.0)

    [<TestMethod>]
    member __.``return a random float between 50,0 and 200,0 when given Random(float,50.0,200.0)`` () =
        let randomNumber = getValueForRandomFloat "Random(float,50.0,200.0)"

        Assert.IsTrue(50.0 <= randomNumber && randomNumber < 200.0)

    [<TestMethod>]
    member __.``return a random float between -100,0 and 1000,0 when given Random(float,-100.0,1000.0)`` () =
        let randomNumber = getValueForRandomFloat "Random(float,-100.0,1000.0)"

        Assert.IsTrue(-100.0 <= randomNumber && randomNumber < 1000.0)

    [<TestMethod>]
    member __.``return a random float between -100,0 and 1000,0 when given Random(float,-100,1000)`` () =
        let randomNumber = getValueForRandomFloat "Random(float,-100,1000)"

        Assert.IsTrue(-100.0 <= randomNumber && randomNumber < 1000.0)

    [<TestMethod>]
    member __.``return a random float between -1000,0 and 1000,0 when given Random(float:5,-1000,1000)`` () =
        let randomNumber = getValueForRandomFloat "Random(float:5,-100,1000)"

        Assert.IsTrue(-1000.0 <= randomNumber && randomNumber < 1000.0)

    [<TestMethod>]
    member __.``return a random float between -2000,0 and 1000,0 when given Random(float:1,-2000,1000)`` () =
        let randomNumber = getValueForRandomFloat "Random(float:1,-100,1000)"

        Assert.IsTrue(-2000.0 <= randomNumber && randomNumber < 1000.0)

[<TestClass>]
type ``getNumbersAfterDecimal should`` () =

    [<TestMethod>]
    member __.``return 2 when given Random(float:2, 0, 100)`` () =
        let numberOfDecimals = getNumbersAfterDecimal "Random(float:2, 0, 100)"

        Assert.AreEqual(2, numberOfDecimals)

    [<TestMethod>]
    member __.``return 3 when given Random(float:3, -1000, 100)`` () =
        let numberOfDecimals = getNumbersAfterDecimal "Random(float:3, -1000, 100)"

        Assert.AreEqual(3, numberOfDecimals)

    [<TestMethod>]
    member __.``return 1 when given Random(float:1,10,20)`` () =
        let numberOfDecimals = getNumbersAfterDecimal "Random(float:1,10,20)"

        Assert.AreEqual(1, numberOfDecimals)

    [<TestMethod>]
    member __.``return 2 when given Random(float:2,-10,-9)`` () =
        let numberOfDecimals = getNumbersAfterDecimal "Random(float:2,-10,-9)"

        Assert.AreEqual(2, numberOfDecimals)

[<TestClass>]
type ``getValueForGender should`` () =

    [<TestMethod>]
    member __.``return mann or kvinne when given Gender(mann,kvinne)`` () =
        let genders = getValuesForGender "Gender(mann,kvinne)"

        Assert.AreEqual("mann",   genders.[0])
        Assert.AreEqual("kvinne", genders.[1])

    [<TestMethod>]
    member __.``return 3 when given Gender(Herr, Frau)`` () =
        let genders = getValuesForGender "Gender(Herr, Frau)"

        Assert.AreEqual("Herr", genders.[0])
        Assert.AreEqual("Frau", genders.[1])

[<TestClass>]
type ``replaceRandomInt should`` () =

    let randomIntPattern = "#{Random\(\s?int\s?,\s?-?\d+\s?,\s?-?\d+\s?\)}"

    [<TestMethod>]
    member __.``return find and replace #{Random(int, 10, 100)} in a string with a random integer`` () =
        let remaining = "Age: #{Random(int, 10, 100)}, fortune: Random(float, 1000.0, 100000.0)"
        let randomString = "Random(int, 10, 100)"

        let returnString = replaceRandomInt remaining randomIntPattern randomString

        let firstPart = returnString.Substring(0, 5)
        let randomPart = Convert.ToInt32 (returnString.Split(',').[0].Split(' ').[1])

        Assert.AreEqual("Age: ", firstPart)
        Assert.IsTrue(10 <= randomPart && randomPart <= 100)

    [<TestMethod>]
    member __.``return find and replace #{Random(int,-10,0)} in a string with a random integer`` () =
        let remaining = "DLA: #{Random(int,-10,0)}, fortune: Random(float, 1000.0, 100000.0)"
        let randomString = "Random(int,-10,0)"

        let returnString = replaceRandomInt remaining randomIntPattern randomString

        let firstPart = returnString.Substring(0, 5)
        let randomPart = Convert.ToInt32 (returnString.Split(',').[0].Split(' ').[1])

        Assert.AreEqual("DLA: ", firstPart)
        Assert.IsTrue(-10 <= randomPart && randomPart <= 0)

[<TestClass>]
type ``replaceRandomIntWithStep should`` () =

    let randomIntWithStepPattern = "#{Random\(\s?int\s?,\s?-?\d+\s?,\s?-?\d+\s?,\s?-?\d+\s?\)}"

    [<TestMethod>]
    member __.``return find and replace #{Random(int, 10, 100)} in a string with a random integer`` () =
        let remaining = "Age: #{Random(int, 10, 20, 100)}, fortune: Random(float, 1000.0, 100000.0)"
        let randomString = "Random(int, 10, 20, 100)"

        let returnString = replaceRandomIntWithStep remaining randomIntWithStepPattern randomString

        let firstPart = returnString.Substring(0, 5)
        let randomPart = Convert.ToInt32 (returnString.Split(',').[0].Split(' ').[1])

        Assert.AreEqual("Age: ", firstPart)
        Assert.IsTrue(randomPart = 10 || randomPart = 30 || randomPart = 50 || randomPart = 70 || randomPart = 90)

    [<TestMethod>]
    member __.``return find and replace #{Random(int,-10,5,0)} in a string with a random integer`` () =
        let remaining = "DLA: #{Random(int,-10,5,0)}, fortune: Random(float, 1000.0, 100000.0)"
        let randomString = "Random(int,-10,5,0)"

        let returnString = replaceRandomIntWithStep remaining randomIntWithStepPattern randomString

        let firstPart = returnString.Substring(0, 5)
        let randomPart = Convert.ToInt32 (returnString.Split(',').[0].Split(' ').[1])

        Assert.AreEqual("DLA: ", firstPart)
        Assert.IsTrue(randomPart = -10 || randomPart = -5 || randomPart = 0)

[<TestClass>]
type ``replaceRandomFloat should`` () =

    let randomFloatPattern = "#{Random\(\s?float\s?,\s?(-?\d+.\d+|-?\d+)\s?,\s?(-?\d+.\d+|-?\d+)\s?\)}"

    [<TestMethod>]
    member __.``return find and replace #{Random(float, 1000, 100000)} in a string with a random float`` () =
        let remaining = "Income: #{Random(float, 1000, 100000)}, married: Random(switch,true,false)"
        let randomString = "Random(float, 1000, 100000)"

        let returnString = replaceRandomFloat remaining randomFloatPattern randomString

        let firstPart = returnString.Substring(0, 8)
        let randomPart = float (returnString.Split(',').[0].Split(' ').[1])

        Assert.AreEqual("Income: ", firstPart)
        Assert.IsTrue(1000.0 <= randomPart && randomPart < 100000.0)

    [<TestMethod>]
    member __.``return find and replace #{Random(float,-1000, 0)} in a string with a random float`` () =
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
    member __.``return find and replace #{Random(switch, true, false)} in a string with either true or false`` () =
        let remaining = "Married: #{Random(switch, true, false)}, income: #{Random(float, 1000, 100000)}, "
        let randomString = "Random(switch, true, false)"

        let returnString = replaceRandomSwitch remaining randomSwitchPattern randomString

        let firstPart = returnString.Substring(0, 9)
        let randomPart = returnString.Split(',').[0].Split(' ').[1]

        Assert.AreEqual("Married: ", firstPart)
        Assert.IsTrue(randomPart = "true" || randomPart = "false")

    [<TestMethod>]
    member __.``return find and replace #{Random(switch, yes, no, maybe)} in a string with either yes, no or maybe`` () =
        let remaining = "Married: #{Random(switch, yes, no, maybe)}, income: #{Random(float, 1000, 100000)}, "
        let randomString = "Random(switch, yes, no, maybe)"

        let returnString = replaceRandomSwitch remaining randomSwitchPattern randomString

        let firstPart = returnString.Substring(0, 9)
        let randomPart = returnString.Split(',').[0].Split(' ').[1]

        Assert.AreEqual("Married: ", firstPart)
        Assert.IsTrue(randomPart = "yes" || randomPart = "no" || randomPart = "maybe")

    [<TestMethod>]
    member __.``return find and replace #{Random(switch,yes,no,maybe)} in a string with either yes, no or maybe`` () =
        let remaining = "Married: #{Random(switch,yes,no,maybe)}, income: #{Random(float, 1000, 100000)}, "
        let randomString = "Random(switch,yes,no,maybe)"

        let returnString = replaceRandomSwitch remaining randomSwitchPattern randomString

        let firstPart = returnString.Substring(0, 9)
        let randomPart = returnString.Split(',').[0].Split(' ').[1]

        Assert.AreEqual("Married: ", firstPart)
        Assert.IsTrue(randomPart = "yes" || randomPart = "no" || randomPart = "maybe")

    [<TestMethod>]
    member __.``return find and replace #{Random(switch,one,two,three,four)} in a string with either one, two, three or four`` () =
        let remaining = "Married: #{Random(switch,one,two,three,four)}, income: #{Random(float, 1000, 100000)}, "
        let randomString = "Random(switch,one,two,three,four)"

        let returnString = replaceRandomSwitch remaining randomSwitchPattern randomString

        let firstPart = returnString.Substring(0, 9)
        let randomPart = returnString.Split(',').[0].Split(' ').[1]

        Assert.AreEqual("Married: ", firstPart)
        Assert.IsTrue(randomPart = "one" || randomPart = "two" || randomPart = "three" || randomPart = "four")

    [<TestMethod>]
    member __.``return find and replace #{Random(switch,one\,one,two,three,four)} in a string with either one,one; two; three or four`` () =
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
    member __.``return find and replace #{Random(switch, one\,one\, two, two, three, four)} in a string with either one,one, two; two; three or four`` () =
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
    member __.``return find and replace #{Random(switch, 'one\,one\', 'two\, two')} in a string with either 'one,one' or 'two, two'`` () =
        let remaining = "Married: #{Random(switch, 'one'\,'one\', 'two'\, 'two')}, income: #{Random(float, 1000, 100000)}, "
        let randomString = "Random(switch, 'one'\,'one\', 'two'\, 'two')"

        let returnString = replaceRandomSwitch remaining randomSwitchPattern randomString

        let firstPart = returnString.Substring(0, 9)
        let randomPart = returnString.Split(':').[1].Split(',') |> Array.map (fun x -> x.Trim())

        Assert.AreEqual("Married: ", firstPart)
        Assert.IsTrue((randomPart.[0] = "'one'" && randomPart.[1] = "'one'") ||  (randomPart.[0] = "'two'" && randomPart.[1] = "'two'"))

    [<TestMethod>]
    member __.``find and replace #{Random(switch, 'one', 'two')} in a string with either 'one' or 'two'`` () =
        let remaining = "Married: #{Random(switch, 'one', 'two')}, income: #{Random(float, 1000, 100000)}, "
        let randomString = "Random(switch, 'one', 'two')"

        let returnString = replaceRandomSwitch remaining randomSwitchPattern randomString

        let firstPart = returnString.Substring(0, 9)
        let randomPart = returnString.Split(',').[0].Split(' ').[1]

        Assert.AreEqual("Married: ", firstPart)
        Assert.IsTrue(randomPart = "'one'" || randomPart = "'two'")

[<TestClass>]
type ``replaceGender should`` () =

    let genderPattern = "#{Gender\(\s?\w+\s?,\s?\w+\s?\)}"

    [<TestMethod>]
    member __.``return find and replace #{Gender(mann,kvinne)} in a string with "mann"`` () =
        let remaining = "Gender: #{Gender(mann,kvinne)}, married: Random(switch,true,false)"
        let genderString = "Gender(mann,kvinne)"

        let returnString = replaceGender remaining genderPattern genderString Gender.Male

        let firstPart = returnString.Substring(0, 8)
        let genderPart = returnString.Split(',').[0].Split(' ').[1]

        Assert.AreEqual("Gender: ", firstPart)
        Assert.AreEqual("mann", genderPart)

    [<TestMethod>]
    member __.``return find and replace #{Gender(man, woman)} in a string with "woman"`` () =
        let remaining = "Gender: #{Gender(man, woman)}, married: Random(switch,true,false)"
        let genderString = "Gender(man, woman)"

        let returnString = replaceGender remaining genderPattern genderString Gender.Female

        let firstPart = returnString.Substring(0, 8)
        let genderPart = returnString.Split(',').[0].Split(' ').[1]

        Assert.AreEqual("Gender: ", firstPart)
        Assert.AreEqual("woman", genderPart)
   
[<TestClass>]
type ``modifyWithoutCulture should`` () =

    let birthDateRegex = Regex "#{BirthDate\(\s?([dfFghHKmMstyz ,\/-]+)\s?\)}"

    [<TestMethod>]
    member __.``replace the birthdate in a string with #{BirthDate(ddMMyy)}`` () =
        let birthDate = DateTime(1977, 5, 17)
        let replacedBirthDate = modifyWithoutCulture birthDateRegex birthDate "#{BirthDate(ddMMyy)}"

        Assert.AreEqual("170577", replacedBirthDate)

    [<TestMethod>]
    member __.``replace the birthdate in a string with #{BirthDate(yyyy-MM-dd)}`` () =
        let birthDate = DateTime(1965, 3, 21)
        let replacedBirthDate = modifyWithoutCulture birthDateRegex birthDate "#{BirthDate(yyyy-MM-dd)}"

        Assert.AreEqual("1965-03-21", replacedBirthDate)   

[<TestClass>]
type ``performSpecialBirthDateReplaces should`` () =

    [<TestMethod>]
    member __.``return find and replace #{BirthDate(ddMMyy)} in a string with the birthdate on ddMMyy format`` () =
        let stringToDoReplaces = "Birthdate: #{BirthDate(ddMMyy)}, married: Random(switch,true,false)"

        let birthDate = DateTime(1997, 03, 11)
        let returnString = performSpecialBirthDateReplaces birthDate stringToDoReplaces

        let firstPart = returnString.Substring(0, 11)
        let birthDatePart = returnString.Split(',').[0].Split(' ').[1]

        Assert.AreEqual("Birthdate: ", firstPart)
        Assert.AreEqual("110397", birthDatePart)

    [<TestMethod>]
    member __.``return find and replace #{BirthDate(MMMM dd yyyy)} in a string with the birthdate on MMMM dd yyyy format`` () =
        let stringToDoReplaces = "Birthdate: #{BirthDate(MMMM dd yyyy)}, married: Random(switch,true,false)"

        let birthDate = DateTime(1987, 12, 01)
        let returnString = performSpecialBirthDateReplaces birthDate stringToDoReplaces

        let firstPart = returnString.Substring(0, 11)
        let birthDatePart = returnString.Split(',').[0].Split(':').[1].Trim()

        Assert.AreEqual("Birthdate: ", firstPart)
        Assert.AreEqual("December 01 1987", birthDatePart)

    [<TestMethod>]
    member __.``return find and replace #{BirthDate(MM-dd-yy)} in a string with the birthdate on MM-dd-yy format`` () =
        let stringToDoReplaces = "Birthdate: #{BirthDate(MM-dd-yy)}, married: Random(switch,true,false)"

        let birthDate = DateTime(1952, 07, 23)
        let returnString = performSpecialBirthDateReplaces birthDate stringToDoReplaces

        let firstPart = returnString.Substring(0, 11)
        let birthDatePart = returnString.Split(',').[0].Split(' ').[1]

        Assert.AreEqual("Birthdate: ", firstPart)
        Assert.AreEqual("07-23-52", birthDatePart)