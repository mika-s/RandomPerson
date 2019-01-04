namespace Tests

open Microsoft.VisualStudio.TestTools.UnitTesting
open CliEnums
open CmdLineArgumentParsing
open RandomPersonLib

[<TestClass>]
type ``parseArgs should`` () =

    [<TestMethod>]
    member __.``return a record with parsed command line arguments 1`` () =
        let args = [ "-m"; "VS" ]

        let options = parseArgs args defaultOptions

        let expectedOptions = { defaultOptions with mode = ValidateSSN }

        Assert.AreEqual(options, expectedOptions)

    [<TestMethod>]
    member __.``return a record with parsed command line arguments 2`` () =
        let args = [ "-m"; "T"; "-a"; "100" ]

        let options = parseArgs args defaultOptions

        let expectedOptions = { defaultOptions with mode = Template; amount = 100 }

        Assert.AreEqual(options, expectedOptions)

    [<TestMethod>]
    member __.``return a record with parsed command line arguments 3`` () =
        let args = [ "-m"; "L"; "-c"; "Sweden"; "-a"; "20"; "-f"; "XML" ]

        let options = parseArgs args defaultOptions

        let expectedOptions = { 
            defaultOptions with mode = List;
                                country = Country.Sweden;
                                amount = 20;
                                fileFormat = XML;
                                outputType = File;
        }

        Assert.AreEqual(options, expectedOptions)

    [<TestMethod>]
    member __.``return a record with parsed command line arguments 4`` () =
        let args = [ "-m"; "VS"; "980401-3911"; "-c"; "Sweden"; ]

        let options = parseArgs args defaultOptions

        let expectedOptions = { 
            defaultOptions with mode = ValidateSSN;
                                country = Country.Sweden;
                                ssn = "980401-3911";
        }

        Assert.AreEqual(options, expectedOptions)

    [<TestMethod>]
    member __.``return a record with parsed command line arguments 5`` () =
        let args = [ "-m"; "VS"; ]

        let options = parseArgs args defaultOptions

        let expectedOptions = { 
            defaultOptions with mode = ValidateSSN;
        }

        Assert.AreEqual(options, expectedOptions)

    [<TestMethod>]
    member __.``return a record with parsed command line arguments 6`` () =
        let args = [ "-m"; "VP"; ]

        let options = parseArgs args defaultOptions

        let expectedOptions = { 
            defaultOptions with mode = ValidatePAN;
        }

        Assert.AreEqual(options, expectedOptions)

    [<TestMethod>]
    member __.``return a record with parsed command line arguments 7`` () =
        let args = [ "-m"; "VP"; "5555555555554446" ]

        let options = parseArgs args defaultOptions

        let expectedOptions = { 
            defaultOptions with mode = ValidatePAN;
                                pan = "5555555555554446";
        }

        Assert.AreEqual(options, expectedOptions)
