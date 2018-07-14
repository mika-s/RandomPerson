namespace Tests

open System
open Microsoft.VisualStudio.TestTools.UnitTesting
open Birthdate
open RandomPersonLib
open Util

[<TestClass>]
type ``generateBirthDate should`` () =

    let random = getRandom false 100

    [<TestMethod>]
    member this.``return a date between 1920 and 18 years ago when isAllowingUnder18 is false and SetYearManually is false`` () =
        let birthDateOptions = 
            {
                SetYearManually = false
                SetUsingAge = false
                Low = 1900
                High = 2000
            }

        let result = generateBirthDate random false birthDateOptions
        Assert.IsTrue(1920 <= result.Year && result.Year < 2000)

    [<TestMethod>]
    member this.``return a date between 1920 and now when isAllowingUnder18 is true and SetYearManually is false`` () =
        let birthDateOptions = 
            {
                SetYearManually = false
                SetUsingAge = false
                Low = 1900
                High = 2000
            }

        let result = generateBirthDate random true birthDateOptions
        Assert.IsTrue(1920 <= result.Year && result.Year <= DateTime.Today.Year)

    [<TestMethod>]
    member this.``return a date between Low and High when isAllowingUnder18 is false and SetYearManually is true and SetUsingAge is false`` () =
        let birthDateOptions = 
            {
                SetYearManually = true
                SetUsingAge = false
                Low = 1940
                High = 1945
            }

        let result = generateBirthDate random false birthDateOptions
        Assert.IsTrue(birthDateOptions.Low <= result.Year && result.Year <= birthDateOptions.High)

    [<TestMethod>]
    member this.``return a date between (now - High) and (now - Low) when isAllowingUnder18 is false and SetYearManually is true and SetUsingAge is true`` () =
        let birthDateOptions = 
            {
                SetYearManually = true
                SetUsingAge = true
                Low = 20
                High = 25
            }

        let result = generateBirthDate random false birthDateOptions
        Assert.IsTrue(DateTime.Today.Year - birthDateOptions.High <= result.Year && result.Year <= DateTime.Today.Year - birthDateOptions.Low)

    [<TestMethod>]
    member this.``return a date between Low and High when isAllowingUnder18 is true and SetYearManually is true and SetUsingAge is false`` () =
        let birthDateOptions = 
            {
                SetYearManually = true
                SetUsingAge = false
                Low = 1940
                High = 1945
            }

        let result = generateBirthDate random true birthDateOptions
        Assert.IsTrue(birthDateOptions.Low <= result.Year && result.Year <= birthDateOptions.High)

    [<TestMethod>]
    member this.``return a date between (now - High) and (now - Low) when isAllowingUnder18 is true and SetYearManually is true and SetUsingAge is true`` () =
        let birthDateOptions = 
            {
                SetYearManually = true
                SetUsingAge = true
                Low = 20
                High = 25
            }

        let result = generateBirthDate random true birthDateOptions
        Assert.IsTrue(DateTime.Today.Year - birthDateOptions.High <= result.Year && result.Year <= DateTime.Today.Year - birthDateOptions.Low)
