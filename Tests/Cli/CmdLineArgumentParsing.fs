namespace Tests

open Microsoft.VisualStudio.TestTools.UnitTesting
open CliEnums
open CmdLineArgumentParsing
open RandomPersonLib

[<TestClass>]
type ``parseArgs should`` () =

    [<TestMethod>]
    member __.``return a record with parsed command line arguments 1`` () =
        let args = [ "-m"; "V" ]

        let options = parseArgs args defaultOptions

        let expectedOptions = { defaultOptions with mode = Mode.Validation }

        Assert.AreEqual(options, expectedOptions)

    [<TestMethod>]
    member __.``return a record with parsed command line arguments 2`` () =
        let args = [ "-m"; "T"; "-a"; "100" ]

        let options = parseArgs args defaultOptions

        let expectedOptions = { defaultOptions with mode = Mode.Template; amount = 100 }

        Assert.AreEqual(options, expectedOptions)

    [<TestMethod>]
    member __.``return a record with parsed command line arguments 3`` () =
        let args = [ "-m"; "L"; "-c"; "Sweden"; "-a"; "20"; "-f"; "XML" ]

        let options = parseArgs args defaultOptions

        let expectedOptions = { 
            defaultOptions with mode = Mode.List;
                                country = Country.Sweden;
                                amount = 20;
                                fileFormat = FileFormat.XML;
                                outputType = OutputType.File;
        }

        Assert.AreEqual(options, expectedOptions)

    [<TestMethod>]
    member __.``return a record with parsed command line arguments 4`` () =
        let args = [ "-m"; "V"; "980401-3911"; "-c"; "Sweden"; ]

        let options = parseArgs args defaultOptions

        let expectedOptions = { 
            defaultOptions with mode = Mode.Validation;
                                country = Country.Sweden;
                                ssn = "980401-3911";
        }

        Assert.AreEqual(options, expectedOptions)

    [<TestMethod>]
    member __.``return a record with parsed command line arguments 5`` () =
        let args = [ "-m"; "V"; ]

        let options = parseArgs args defaultOptions

        let expectedOptions = { 
            defaultOptions with mode = Mode.Validation;
        }

        Assert.AreEqual(options, expectedOptions)
