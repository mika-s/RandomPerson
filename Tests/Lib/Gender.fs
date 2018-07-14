namespace Tests

open Microsoft.VisualStudio.TestTools.UnitTesting
open RandomPersonLib
open Gender
open Util

[<TestClass>]
type ``generateGender should`` () =

    [<TestMethod>]
    member this.``return either male or female`` () =
        let random = getRandom false 100
        let result = generateGender random
        Assert.IsTrue(result = Gender.Male || result = Gender.Female)
