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
    member __.``return a date between 1920 and 18 years ago when BirthDateMode = DefaultCalendarYearRange and isAllowingUnder18 is false`` () =
        let birthDateOptions = BirthDateOptions()

        let result = generateBirthDate random false birthDateOptions
        Assert.IsTrue(1920 <= result.Year && result.Year < DateTime.Today.Year - 18)

    [<TestMethod>]
    member __.``return a date between 1920 and now when BirthDateMode = DefaultCalendarYearRange and isAllowingUnder18 is true`` () =
        let birthDateOptions = BirthDateOptions()

        let result = generateBirthDate random true birthDateOptions
        Assert.IsTrue(1920 <= result.Year && result.Year <= DateTime.Today.Year)

    [<TestMethod>]
    member __.``return a date between Low and High when BirthDateMode = ManualCalendarYearRange and isAllowingUnder18 is false`` () =
        let birthDateOptions = BirthDateOptions(BirthDateMode.ManualCalendarYearRange, 1940, 1945)

        let result = generateBirthDate random false birthDateOptions
        Assert.IsTrue(birthDateOptions.Low <= result.Year && result.Year <= birthDateOptions.High)

    [<TestMethod>]
    member __.``return a date between (now - High) and (now - Low) when BirthDateMode = ManualAgeRange and isAllowingUnder18 is false`` () =
        let birthDateOptions = BirthDateOptions(BirthDateMode.ManualAgeRange, 25, 30)

        let result = generateBirthDate random false birthDateOptions
        Assert.IsTrue(DateTime.Today.Year - birthDateOptions.High <= result.Year && result.Year <= DateTime.Today.Year - birthDateOptions.Low)

    [<TestMethod>]
    member __.``return a date between Low and High when BirthDateMode = ManualCalendarYearRange and isAllowingUnder18 is true`` () =
        let birthDateOptions = BirthDateOptions(BirthDateMode.ManualCalendarYearRange, 1940, 1945)

        let result = generateBirthDate random true birthDateOptions
        Assert.IsTrue(birthDateOptions.Low <= result.Year && result.Year <= birthDateOptions.High)

    [<TestMethod>]
    member __.``return a date between (now - High) and (now - Low) when BirthDateMode = ManualAgeRange and isAllowingUnder18 is true`` () =
        let birthDateOptions = BirthDateOptions(BirthDateMode.ManualAgeRange, 20, 25)

        let result = generateBirthDate random true birthDateOptions
        Assert.IsTrue(DateTime.Today.Year - birthDateOptions.High <= result.Year && result.Year <= DateTime.Today.Year - birthDateOptions.Low)
