namespace Tests

open System
open Microsoft.VisualStudio.TestTools.UnitTesting
open Password
open TestData
open Util

[<TestClass>]
type ``generatePassword should`` () =

    let random = getRandom false 100

    [<TestMethod>]
    member __.``return a random password`` () =
        let firstName = "Test"
        let lastName  = "Tester"
        let birthDate = DateTime(1999, 05, 12)
        let passwords = getPasswords ()

        let generatedPassword = generatePassword random passwords firstName lastName birthDate

        Assert.IsTrue(4 < generatedPassword.Length && generatedPassword.Length < 50)
