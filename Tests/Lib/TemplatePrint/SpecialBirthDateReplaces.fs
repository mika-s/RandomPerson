namespace Tests

open System
open System.Globalization
open System.Text.RegularExpressions
open Microsoft.VisualStudio.TestTools.UnitTesting
open SpecialBirthDateReplaces
open StringUtil

[<TestClass>]
type ``replaceWithoutCulture should`` () =

    let birthDateRegex = Regex "#{BirthDate\(\s?'([dfFghHKmMstyz ,\/-]+)'\s?\)}"

    [<TestMethod>]
    member __.``replace #{BirthDate('ddMMyy')} in a string with the birthdate in ddMMyy format`` () =
        let birthDate = DateTime(1977, 5, 17)
        let replacedBirthDate = replaceWithoutCulture birthDateRegex birthDate "#{BirthDate('ddMMyy')}"

        Assert.AreEqual(birthDate.ToString("ddMMyy"), replacedBirthDate)

    [<TestMethod>]
    member __.``replace #{BirthDate('yyyy-MM-dd')} in a string with the birthdate in yyyy-MM-dd format`` () =
        let birthDate = DateTime(1965, 3, 21)
        let replacedBirthDate = replaceWithoutCulture birthDateRegex birthDate "#{BirthDate('yyyy-MM-dd')}"

        Assert.AreEqual(birthDate.ToString("yyyy-MM-dd"), replacedBirthDate)

[<TestClass>]
type ``replaceWithCulture should`` () =

    let birthDateWithCultureRegex = Regex "#{BirthDate\(\s?'([dfFghHKmMstyz ,\/-]+)'\s?,\s?'([a-zA-Z-]+)'\s?\)}"

    [<TestMethod>]
    member __.``replace #{BirthDate('MMM', 'da-DK')} in a string with the birthdate in Danish MMM format`` () =
        let birthDate = DateTime(1977, 5, 17)
        let replacedBirthDate = replaceWithCulture birthDateWithCultureRegex birthDate "#{BirthDate('MMM', 'da-DK')}"

        Assert.AreEqual(birthDate.ToString("MMM", CultureInfo.CreateSpecificCulture("da-DK")), replacedBirthDate)

    [<TestMethod>]
    member __.``replace #{BirthDate('MMM, ddd')} in a string with the birthdate in Danish MMM, ddd format`` () =
        let birthDate = DateTime(1965, 2, 21)
        let replacedBirthDate = replaceWithCulture birthDateWithCultureRegex birthDate "#{BirthDate('MMM, ddd', 'da-DK')}"

        Assert.AreEqual(birthDate.ToString("MMM, ddd", CultureInfo.CreateSpecificCulture("da-DK")), replacedBirthDate)

[<TestClass>]
type ``performSpecialBirthDateReplaces should`` () =

    [<TestMethod>]
    member __.``find and replace #{BirthDate('ddMMyy')} in a string with the birthdate on ddMMyy format`` () =
        let stringToDoReplaces = "Birthdate: #{BirthDate('ddMMyy')}, married: Random(switch,true,false)"

        let birthDate = DateTime(1997, 03, 11)
        let returnString = performSpecialBirthDateReplaces birthDate stringToDoReplaces

        let firstPart = returnString |> substring 0 11
        let birthDatePart = returnString.Split(',').[0].Split(' ').[1]

        Assert.AreEqual("Birthdate: ", firstPart)
        Assert.AreEqual(birthDate.ToString("ddMMyy"), birthDatePart)

    [<TestMethod>]
    member __.``find and replace #{BirthDate('MMMM dd yyyy')} in a string with the birthdate on MMMM dd yyyy format`` () =
        let stringToDoReplaces = "Birthdate: #{BirthDate('MMMM dd yyyy')}, married: Random(switch,true,false)"

        let birthDate = DateTime(1987, 12, 01)
        let returnString = performSpecialBirthDateReplaces birthDate stringToDoReplaces

        let firstPart = returnString |> substring 0 11
        let birthDatePart = returnString.Split(',').[0].Split(':').[1].Trim()

        Assert.AreEqual("Birthdate: ", firstPart)
        Assert.AreEqual(birthDate.ToString("MMMM dd yyyy"), birthDatePart)

    [<TestMethod>]
    member __.``find and replace #{BirthDate('MM-dd-yy')} in a string with the birthdate on MM-dd-yy format`` () =
        let stringToDoReplaces = "Birthdate: #{BirthDate('MM-dd-yy')}, married: Random(switch,true,false)"

        let birthDate = DateTime(1952, 07, 23)
        let returnString = performSpecialBirthDateReplaces birthDate stringToDoReplaces

        let firstPart = returnString |> substring 0 11
        let birthDatePart = returnString.Split(',').[0].Split(' ').[1]

        Assert.AreEqual("Birthdate: ", firstPart)
        Assert.AreEqual(birthDate.ToString("MM-dd-yy"), birthDatePart)

    [<TestMethod>]
    member __.``find and replace #{BirthDate('dd-MMMM-yyyy', 'fi-FI')} in a string with the birthdate on Finnish dd-MMMM-yyyy format`` () =
        let stringToDoReplaces = "Birthdate: #{BirthDate('dd-MMMM-yyyy', 'fi-FI')}, married: Random(switch,true,false)"

        let birthDate = DateTime(1947, 1, 11)
        let returnString = performSpecialBirthDateReplaces birthDate stringToDoReplaces

        let firstPart = returnString |> substring 0 11
        let birthDatePart = returnString.Split(',').[0].Split(' ').[1]

        Assert.AreEqual("Birthdate: ", firstPart)
        Assert.AreEqual(birthDate.ToString("dd-MMMM-yyyy", CultureInfo.CreateSpecificCulture("fi-FI")), birthDatePart)
