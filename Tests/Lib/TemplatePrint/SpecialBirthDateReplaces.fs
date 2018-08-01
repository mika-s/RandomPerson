namespace Tests

open System
open Microsoft.VisualStudio.TestTools.UnitTesting
open SpecialBirthDateReplaces
open System.Text.RegularExpressions

[<TestClass>]
type ``replaceWithoutCulture should`` () =

    let birthDateRegex = Regex "#{BirthDate\(\s?'([dfFghHKmMstyz ,\/-]+)'\s?\)}"

    [<TestMethod>]
    member __.``replace the birthdate in a string with #{BirthDate(ddMMyy)}`` () =
        let birthDate = DateTime(1977, 5, 17)
        let replacedBirthDate = replaceWithoutCulture birthDateRegex birthDate "#{BirthDate('ddMMyy')}"

        Assert.AreEqual("170577", replacedBirthDate)

    [<TestMethod>]
    member __.``replace the birthdate in a string with #{BirthDate(yyyy-MM-dd)}`` () =
        let birthDate = DateTime(1965, 3, 21)
        let replacedBirthDate = replaceWithoutCulture birthDateRegex birthDate "#{BirthDate('yyyy-MM-dd')}"

        Assert.AreEqual("1965-03-21", replacedBirthDate)

[<TestClass>]
type ``replaceWithCulture should`` () =

    let birthDateWithCultureRegex = Regex "#{BirthDate\(\s?'([dfFghHKmMstyz ,\/-]+)'\s?,\s?'([a-zA-Z-]+)'\s?\)}"

    [<TestMethod>]
    member __.``replace the birthdate in a string with #{BirthDate(MMM)}`` () =
        let birthDate = DateTime(1977, 5, 17)
        let replacedBirthDate = replaceWithCulture birthDateWithCultureRegex birthDate "#{BirthDate('MMM', 'da-DK')}"

        Assert.AreEqual("maj", replacedBirthDate)

    [<TestMethod>]
    member __.``replace the birthdate in a string with #{BirthDate(MMM, ddd)}`` () =
        let birthDate = DateTime(1965, 2, 21)
        let replacedBirthDate = replaceWithCulture birthDateWithCultureRegex birthDate "#{BirthDate('MMM, ddd', 'da-DK')}"

        Assert.AreEqual("feb, sø", replacedBirthDate)

[<TestClass>]
type ``performSpecialBirthDateReplaces should`` () =

    [<TestMethod>]
    member __.``return find and replace #{BirthDate(ddMMyy)} in a string with the birthdate on ddMMyy format`` () =
        let stringToDoReplaces = "Birthdate: #{BirthDate('ddMMyy')}, married: Random(switch,true,false)"

        let birthDate = DateTime(1997, 03, 11)
        let returnString = performSpecialBirthDateReplaces birthDate stringToDoReplaces

        let firstPart = returnString.Substring(0, 11)
        let birthDatePart = returnString.Split(',').[0].Split(' ').[1]

        Assert.AreEqual("Birthdate: ", firstPart)
        Assert.AreEqual("110397", birthDatePart)

    [<TestMethod>]
    member __.``return find and replace #{BirthDate(MMMM dd yyyy)} in a string with the birthdate on MMMM dd yyyy format`` () =
        let stringToDoReplaces = "Birthdate: #{BirthDate('MMMM dd yyyy')}, married: Random(switch,true,false)"

        let birthDate = DateTime(1987, 12, 01)
        let returnString = performSpecialBirthDateReplaces birthDate stringToDoReplaces

        let firstPart = returnString.Substring(0, 11)
        let birthDatePart = returnString.Split(',').[0].Split(':').[1].Trim()

        Assert.AreEqual("Birthdate: ", firstPart)
        Assert.AreEqual("December 01 1987", birthDatePart)

    [<TestMethod>]
    member __.``return find and replace #{BirthDate(MM-dd-yy)} in a string with the birthdate on MM-dd-yy format`` () =
        let stringToDoReplaces = "Birthdate: #{BirthDate('MM-dd-yy')}, married: Random(switch,true,false)"

        let birthDate = DateTime(1952, 07, 23)
        let returnString = performSpecialBirthDateReplaces birthDate stringToDoReplaces

        let firstPart = returnString.Substring(0, 11)
        let birthDatePart = returnString.Split(',').[0].Split(' ').[1]

        Assert.AreEqual("Birthdate: ", firstPart)
        Assert.AreEqual("07-23-52", birthDatePart)

    [<TestMethod>]
    member __.``return find and replace #{BirthDate(dd-MMMM-yyyy, fi-FI)} in a string with the birthdate on Finnish dd-MMMM-yyyy format`` () =
        let stringToDoReplaces = "Birthdate: #{BirthDate('dd-MMMM-yyyy', 'fi-FI')}, married: Random(switch,true,false)"

        let birthDate = DateTime(1947, 1, 11)
        let returnString = performSpecialBirthDateReplaces birthDate stringToDoReplaces

        let firstPart = returnString.Substring(0, 11)
        let birthDatePart = returnString.Split(',').[0].Split(' ').[1]

        Assert.AreEqual("Birthdate: ", firstPart)
        Assert.AreEqual("11-tammikuuta-1947", birthDatePart)
