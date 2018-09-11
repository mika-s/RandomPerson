namespace Tests

open System
open System.Text.RegularExpressions
open Microsoft.VisualStudio.TestTools.UnitTesting
open SpecialGuidReplaces

[<TestClass>]
type ``replaceGuidWithoutFormat should`` () =

    let guidWithoutFormatRegex = Regex "#{GUID\(\s?\)}"

    [<TestMethod>]
    member __.``return mann or kvinne when given #{Gender('mann','kvinne')}`` () =
        let replaced = replaceGuidWithoutFormat guidWithoutFormatRegex "#{GUID()}"
        let isGuid, _ = Guid.TryParse(replaced)

        Assert.IsTrue(isGuid)

[<TestClass>]
type ``replaceGuidWithFormat should`` () =

    let guidWithFormatRegex = Regex "#{GUID\(\s?'([NDBPX])'\s?\)}"

    [<TestMethod>]
    member __.``return a valid GUID when given #{GUID('N')}`` () =
        let replaced = replaceGuidWithFormat guidWithFormatRegex "#{GUID('N')}"
        let isGuid, _ = Guid.TryParse(replaced)

        Assert.IsTrue(isGuid)

    [<TestMethod>]
    member __.``return a valid GUID when given #{GUID( 'D' )}`` () =
        let replaced = replaceGuidWithFormat guidWithFormatRegex "#{GUID( 'D' )}"
        let isGuid, _ = Guid.TryParse(replaced)

        Assert.IsTrue(isGuid)

    [<TestMethod>]
    member __.``return a valid GUID when given #{GUID('B' )}`` () =
        let replaced = replaceGuidWithFormat guidWithFormatRegex "#{GUID('B' )}"
        let isGuid, _ = Guid.TryParse(replaced)

        Assert.IsTrue(isGuid)

    [<TestMethod>]
    member __.``return a valid GUID when given #{GUID( 'P')}`` () =
        let replaced = replaceGuidWithFormat guidWithFormatRegex "#{GUID( 'P')}"
        let isGuid, _ = Guid.TryParse(replaced)

        Assert.IsTrue(isGuid)

    [<TestMethod>]
    member __.``return a valid GUID when given #{GUID('X')}`` () =
        let replaced = replaceGuidWithFormat guidWithFormatRegex "#{GUID('X')}"
        let isGuid, _ = Guid.TryParse(replaced)

        Assert.IsTrue(isGuid)

[<TestClass>]
type ``performSpecialGuidReplaces should`` () =

    [<TestMethod>]
    member __.``return find and replace #{GUID()} in a string`` () =
        let stringToDoReplaces = "GUID: #{GUID()}, married: Random(switch,true,false)"

        let returnString = performSpecialGuidReplaces stringToDoReplaces

        let firstPart = returnString.Substring(0, 6)
        let guidPart  = returnString.Split(',').[0].Split(' ').[1]
        let isGuid, _ = Guid.TryParse(guidPart)

        Assert.AreEqual("GUID: ", firstPart)
        Assert.IsTrue(isGuid)

    [<TestMethod>]
    member __.``return find and replace #{GUID('P')} in a string`` () =
        let stringToDoReplaces = "GUID: #{GUID('P')}, married: Random(switch,true,false)"

        let returnString = performSpecialGuidReplaces stringToDoReplaces

        let firstPart = returnString.Substring(0, 6)
        let guidPart  = returnString.Split(',').[0].Split(' ').[1]
        let isGuid, _ = Guid.TryParse(guidPart)

        Assert.AreEqual("GUID: ", firstPart)
        Assert.IsTrue(isGuid)
