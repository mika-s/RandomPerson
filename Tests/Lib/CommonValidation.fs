﻿namespace Tests

open Microsoft.VisualStudio.TestTools.UnitTesting

open CommonValidation

[<TestClass>]
type ``HasCorrectLength should`` () =

    [<TestMethod>]
    member __.``return 123456 when the potential SSN = 123456 and ssnLength = 6`` () =
        let ssn = "123456"
        let ssnLength = ssn.Length
        
        match ssn with
        | HasCorrectLength ssnLength ssn rest -> Assert.AreEqual(ssn, rest)
        | _                                   -> Assert.IsTrue(false)

    [<TestMethod>]
    member __.``return None when the potential SSN = 123456 and ssnLength = 7`` () =
        let ssn = "123456"
        let ssnLength = ssn.Length + 1
        
        match ssn with
        | HasCorrectLength ssnLength ssn _ -> Assert.IsTrue(false)
        | _                                -> Assert.IsTrue(true)

[<TestClass>]
type ``HasDate should`` () =

    [<TestMethod>]
    member __.``return the remaining SSN after the date when the potential SSN has a proper date`` () =
        let ssn = "2109881234"
        let dateStart = 0
        let dateLength = 6
        let individualNumberStart = 6
        let datePattern = "ddMMyy"
        
        match ssn with
        | HasDate dateStart dateLength individualNumberStart datePattern ssn rest -> Assert.AreEqual("1234", rest)
        | _                                                                       -> Assert.IsTrue(false)

    [<TestMethod>]
    member __.``return None when the potential SSN doesn't have a proper date`` () =
        let ssn = "4109881234"
        let dateStart = 0
        let dateLength = 6
        let individualNumberStart = 6
        let datePattern = "ddMMyy"
        
        match ssn with
        | HasDate dateStart dateLength individualNumberStart datePattern ssn _ -> Assert.IsTrue(false)
        | _                                                                    -> Assert.IsTrue(true)

[<TestClass>]
type ``HasIndividualNumber should`` () =

    [<TestMethod>]
    member __.``return the remaining part after the individual number when the individual number is proper`` () =
        let rest = "123456"
        let individualNumberLength = 4

        match rest with
        | HasIndividualNumber individualNumberLength rest newRest -> Assert.AreEqual("56", newRest)
        | _                                                       -> Assert.IsTrue(false)