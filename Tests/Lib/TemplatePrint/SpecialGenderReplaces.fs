namespace Tests

open System.Text.RegularExpressions
open Microsoft.VisualStudio.TestTools.UnitTesting
open RandomPersonLib
open SpecialGenderReplaces
open StringUtil

[<TestClass>]
type ``replaceGender should`` () =

    let genderRegex = Regex "#{Gender\(\s?'([\w-\\\/, ]+)'\s?,\s?'([\w-\\\/, ]+)'\s?\)}"

    [<TestMethod>]
    member __.``return mann or kvinne when given #{Gender('mann','kvinne')}`` () =
        let gender = Gender.Male
        let replaced = replaceGender genderRegex gender "#{Gender('mann','kvinne')}"

        Assert.AreEqual("mann", replaced)

    [<TestMethod>]
    member __.``return Herr or Frau when given #{Gender('Herr','Frau')}`` () =
        let gender = Gender.Female
        let replaced = replaceGender genderRegex gender "#{Gender('Herr', 'Frau')}"

        Assert.AreEqual("Frau", replaced)

[<TestClass>]
type ``performSpecialGenderReplaces should`` () =

    [<TestMethod>]
    member __.``return find and replace #{Gender('Lord', 'Lady')} in a string with "Lord" when given male gender`` () =
        let stringToDoReplaces = "Gender: #{Gender('Lord', 'Lady')}, married: Random(switch,true,false)"

        let gender = Gender.Male
        let returnString = performSpecialGenderReplaces gender stringToDoReplaces

        let firstPart = returnString |> substring 0 8
        let genderPart = returnString.Split(',').[0].Split(' ').[1]

        Assert.AreEqual("Gender: ", firstPart)
        Assert.AreEqual("Lord", genderPart)

    [<TestMethod>]
    member __.``return find and replace #{Gender('Lord', 'Lady')} in a string with "Lady" when given female gender`` () =
        let stringToDoReplaces = "Gender: #{Gender('Lord', 'Lady')}, married: Random(switch,true,false)"

        let gender = Gender.Female
        let returnString = performSpecialGenderReplaces gender stringToDoReplaces

        let firstPart = returnString |> substring 0 8
        let genderPart = returnString.Split(',').[0].Split(' ').[1]

        Assert.AreEqual("Gender: ", firstPart)
        Assert.AreEqual("Lady", genderPart)

    [<TestMethod>]
    member __.``return find and replace #{Gender('Lord, Baron', 'Lady, Baroness')} in a string with "Lady, Baroness" when given female gender`` () =
        let stringToDoReplaces = "Gender:_#{Gender('Lord, Baron', 'Lady, Baroness')}; married: Random(switch,true,false)"

        let gender = Gender.Female
        let returnString = performSpecialGenderReplaces gender stringToDoReplaces

        let firstPart = returnString |> substring 0 8
        let genderPart = returnString.Split(';').[0].Split('_').[1]

        Assert.AreEqual("Gender:_", firstPart)
        Assert.AreEqual("Lady, Baroness", genderPart)

    [<TestMethod>]
    member __.``return find and replace #{Gender('Lord/Baron', 'Lady/Baroness')} in a string with "Lord/Baron" when given male gender`` () =
        let stringToDoReplaces = "Gender:_#{Gender('Lord/Baron', 'Lady/Baroness')}; married: Random(switch,true,false)"

        let gender = Gender.Male
        let returnString = performSpecialGenderReplaces gender stringToDoReplaces

        let firstPart = returnString |> substring 0 8
        let genderPart = returnString.Split(';').[0].Split('_').[1]

        Assert.AreEqual("Gender:_", firstPart)
        Assert.AreEqual("Lord/Baron", genderPart)
