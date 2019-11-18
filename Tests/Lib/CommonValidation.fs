namespace Tests

open Microsoft.VisualStudio.TestTools.UnitTesting

open CommonValidation
open Types.SSNTypes

[<TestClass>]
type ``hasCorrectShape should`` () =

    [<TestMethod>]
    member __.``return Success when the shape is proper`` () =
        let ssn = "41098812346"
        let shape = "^\d{11}$"

        let result = hasCorrectShape shape ssn

        match result with
        | Success _ -> Assert.IsTrue(true)
        | Failure _ -> Assert.IsTrue(false)

    [<TestMethod>]
    member __.``return Success when the shape is improper`` () =
        let ssn = "4109881234"
        let shape = "^\d{11}$"

        let result = hasCorrectShape shape ssn

        match result with
        | Success _ -> Assert.IsTrue(false)
        | Failure f -> Assert.AreEqual(f, InvalidShape)

[<TestClass>]
type ``hasDate should`` () =

    [<TestMethod>]
    member __.``return Success when the date when the potential SSN has a proper date`` () =
        let ssn = "2109881234"
        let dateStart = 0
        let dateLength = 6
        let datePattern = "ddMMyy"
        
        let result = hasDate datePattern dateStart dateLength ssn

        match result with
        | Success _ -> Assert.IsTrue(true)
        | Failure _ -> Assert.IsTrue(false)

    [<TestMethod>]
    member __.``return Failure when the potential SSN doesn't have a proper date`` () =
        let ssn = "4109881234"
        let dateStart = 0
        let dateLength = 6
        let datePattern = "ddMMyy"
        
        let result = hasDate datePattern dateStart dateLength ssn

        match result with
        | Success _ -> Assert.IsTrue(false)
        | Failure f -> Assert.AreEqual(f, InvalidDate)

[<TestClass>]
type ``hasIndividualNumber should`` () =

    [<TestMethod>]
    member __.``return Success when the individual number is proper`` () =
        let ssn = "4109881234"
        let individualNumberStart = 0
        let individualNumberLength = 6

        let result = hasIndividualNumber individualNumberStart individualNumberLength ssn

        match result with
        | Success _ -> Assert.IsTrue(true)
        | Failure _ -> Assert.IsTrue(false)

    [<TestMethod>]
    member __.``return Failure when the individual number is improper`` () =
        let ssn = "4109881+34"
        let individualNumberStart = 6
        let individualNumberLength = 3

        let result = hasIndividualNumber individualNumberStart individualNumberLength ssn

        match result with
        | Success _ -> Assert.IsTrue(false)
        | Failure f -> Assert.AreEqual(f, InvalidIndividualNumber)
