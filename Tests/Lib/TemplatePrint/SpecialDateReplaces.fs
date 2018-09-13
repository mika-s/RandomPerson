namespace Tests

open System
open System.Globalization
open System.Text.RegularExpressions
open Microsoft.VisualStudio.TestTools.UnitTesting
open SpecialDateReplaces

[<TestClass>]
type ``replace nowWithoutFormatAndCulture should`` () =

    let dateNowRegex = Regex "#{Date\(\s?'now'\s?\)}"

    [<TestMethod>]
    member __.``replace #{Date('now')} in a string with the current date`` () =
        let replacedDate = replace nowWithoutFormatAndCulture dateNowRegex "#{Date('now')}"

        Assert.AreEqual(DateTime.Now.ToString("yyyy-MM-dd"), replacedDate)

[<TestClass>]
type ``replace nowWithFormat should`` () =

    let dateNowWithFormatRegex = Regex "#{Date\(\s?'now'\s?,\s?'([dfFghHKmMstyz ,\/-]+)'\s?\)}"

    [<TestMethod>]
    member __.``replace #{Date('now', 'MMM')} in a string with the current date in MMM format`` () =
        let replacedDate = replace nowWithFormat dateNowWithFormatRegex "#{Date('now', 'MMM')}"

        Assert.AreEqual(DateTime.Now.ToString("MMM"), replacedDate)

[<TestClass>]
type ``replace nowWithFormatAndCulture should`` () =

    let dateNowWithFormatAndCultureRegex = Regex "#{Date\(\s?'now'\s?,\s?'([dfFghHKmMstyz ,\/-]+)'\s?,\s?'([a-zA-Z-]+)'\s?\)}"

    [<TestMethod>]
    member __.``replace #{Date('now', 'ddd MMM', 'no-NO')} in a string with the current date in Norwegian ddd MMM format`` () =
        let replacedDate = replace nowWithFormatAndCulture dateNowWithFormatAndCultureRegex "#{Date('now', 'ddd MMM', 'no-NO')}"

        Assert.AreEqual(DateTime.Now.ToString("ddd MMM", CultureInfo.CreateSpecificCulture("no-NO")), replacedDate)

[<TestClass>]
type ``replace daysWithoutFormatAndCulture should`` () =

    let dateDaysRegex = Regex "#{Date\(\s?([-+]?\d+)\s?\)}"

    [<TestMethod>]
    member __.``replace #{Date(100)} in a string with the current date`` () =
        let replacedDate = replace daysWithoutFormatAndCulture dateDaysRegex "#{Date(100)}"

        Assert.AreEqual(DateTime.Now.AddDays(100.0).ToString("yyyy-MM-dd"), replacedDate)

[<TestClass>]
type ``replace daysWithFormat should`` () =

    let dateDaysWithFormatRegex = Regex "#{Date\(\s?([-+]?\d+)\s?,\s?'([dfFghHKmMstyz ,\/-]+)'\s?\)}"

    [<TestMethod>]
    member __.``replace #{Date(-1000, 'MMM')} in a string with the current date in MMM format`` () =
        let replacedDate = replace daysWithFormat dateDaysWithFormatRegex "#{Date(-1000, 'MMM')}"

        Assert.AreEqual(DateTime.Now.AddDays(-1000.0).ToString("MMM"), replacedDate)

[<TestClass>]
type ``replace daysWithFormatAndCulture should`` () =

    let dateDaysWithFormatAndCultureRegex = Regex "#{Date\(\s?([-+]?\d+)\s?,\s?'([dfFghHKmMstyz ,\/-]+)'\s?,\s?'([a-zA-Z-]+)'\s?\)}"

    [<TestMethod>]
    member __.``replace #{Date(-1, 'ddd MMM', 'no-NO')} in a string with the current date in Norwegian ddd MMM format`` () =
        let replacedDate = replace daysWithFormatAndCulture dateDaysWithFormatAndCultureRegex "#{Date(-1, 'ddd MMM', 'no-NO')}"

        Assert.AreEqual(DateTime.Now.AddDays(-1.0).ToString("ddd MMM", CultureInfo.CreateSpecificCulture("no-NO")), replacedDate)

[<TestClass>]
type ``performSpecialDateReplaces should`` () =

    [<TestMethod>]
    member __.``return find and replace #{Date('now')} in a string with the current date`` () =
        let stringToDoReplaces = "Date: #{Date('now')}, married: Random(switch,true,false)"

        let returnString = performSpecialDateReplaces stringToDoReplaces

        let firstPart = returnString.Substring(0, 6)
        let datePart = returnString.Split(',').[0].Split(' ').[1]

        Assert.AreEqual("Date: ", firstPart)
        Assert.AreEqual(DateTime.Now.ToString("yyyy-MM-dd"), datePart)

    [<TestMethod>]
    member __.``return find and replace #{Date('now', 'MMMM dd yyyy')} in a string with the current date on MMMM dd yyyy format`` () =
        let stringToDoReplaces = "Date: #{Date('now', 'MMMM dd yyyy')}, married: Random(switch,true,false)"

        let returnString = performSpecialDateReplaces stringToDoReplaces

        let firstPart = returnString.Substring(0, 6)
        let datePart = returnString.Split(',').[0].Split(':').[1].Trim()

        Assert.AreEqual("Date: ", firstPart)
        Assert.AreEqual(DateTime.Now.ToString("MMMM dd yyyy"), datePart)

    [<TestMethod>]
    member __.``return find and replace #{Date(-10, 'MM-dd-yy')} in a string with the current date - 10 days on MM-dd-yy format`` () =
        let stringToDoReplaces = "Date: #{Date(-10, 'MM-dd-yy')}, married: Random(switch,true,false)"

        let returnString = performSpecialDateReplaces stringToDoReplaces

        let firstPart = returnString.Substring(0, 6)
        let datePart = returnString.Split(',').[0].Split(' ').[1]

        Assert.AreEqual("Date: ", firstPart)
        Assert.AreEqual(DateTime.Now.AddDays(-10.0).ToString("MM-dd-yy"), datePart)

    [<TestMethod>]
    member __.``return find and replace #{Date(11, 'dd-MMMM-yyyy', 'fi-FI')} in a string with the current date + 11 days on Finnish dd-MMMM-yyyy format`` () =
        let stringToDoReplaces = "Date: #{Date(11, 'dd-MMMM-yyyy', 'fi-FI')}, married: Random(switch,true,false)"

        let returnString = performSpecialDateReplaces stringToDoReplaces

        let firstPart = returnString.Substring(0, 6)
        let datePart = returnString.Split(',').[0].Split(' ').[1]

        Assert.AreEqual("Date: ", firstPart)
        Assert.AreEqual(DateTime.Now.AddDays(11.0).ToString("dd-MMMM-yyyy", CultureInfo.CreateSpecificCulture("fi-FI")), datePart)
