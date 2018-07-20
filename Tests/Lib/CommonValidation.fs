namespace Tests

open Microsoft.VisualStudio.TestTools.UnitTesting

open CommonValidation

[<TestClass>]
type ``HasCorrectLength should`` () =

    [<TestMethod>]
    member __.``return 123456 when the potential SSN = 123456 and ssnLength = 6`` () =
        let ssn = "123456"
        let ssnLength = ssn.Length
        
        match ssn with
        | HasCorrectLength ssnLength ssn rest ->
            Assert.AreEqual(ssn, rest)
        | _  -> Assert.IsTrue(false)