namespace Tests

open System
open Microsoft.VisualStudio.TestTools.UnitTesting
open Email
open TestData
open Util

[<TestClass>]
type ``generateEmailAddress should`` () =

    let random = getRandom false 100

    [<TestMethod>]
    member this.``return a random email address`` () =
        let firstName = "Test"
        let lastName  = "Tester"
        let birthDate = DateTime(1999, 05, 12)
        let emailAddresseses = getEmailAddresseses ()

        let generatedEmail = generateEmailAddress random emailAddresseses firstName lastName birthDate

        let splitEmail = generatedEmail.Split('@')
        let username = splitEmail.[0]
        let domain = splitEmail.[1]
        let domainArray = domain.Split('.')
        let hostname = domainArray.[0]
        let tld = domainArray.[1]

        Assert.IsTrue(username.Length > 1)
        Assert.IsTrue(hostname.Length > 1)
        Assert.IsTrue(generatedEmail.Contains("@"))
        Assert.AreEqual("com", tld)
        Assert.IsTrue(Array.contains domain emailAddresseses)
