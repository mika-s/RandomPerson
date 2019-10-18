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
    member __.``return a date between 1920 and 18 years ago when BirthDateModeOptions = DefaultYearRangeBirthDateOptions and isAllowingUnder18 is false`` () =
        let birthDateOptions = DefaultYearRangeBirthDateOptions(false)

        let result = generateBirthDate random birthDateOptions
        Assert.IsTrue(1920 <= result.Year && result.Year < DateTime.Today.Year - 18)

    [<TestMethod>]
    member __.``return a date between 1920 and now when BirthDateModeOptions = DefaultYearRangeBirthDateOptions and isAllowingUnder18 is true`` () =
        let birthDateOptions = DefaultYearRangeBirthDateOptions(true)

        let result = generateBirthDate random birthDateOptions
        Assert.IsTrue(1920 <= result.Year && result.Year <= DateTime.Today.Year)

    [<TestMethod>]
    member __.``return a date between Low and High when BirthDateModeOptions = CalendarYearRangeBirthDateOptions`` () =
        let birthDateOptions = CalendarYearRangeBirthDateOptions(1940, 1945)

        let result = generateBirthDate random birthDateOptions
        Assert.IsTrue(birthDateOptions.Low <= result.Year && result.Year <= birthDateOptions.High)

    [<TestMethod>]
    member __.``return a date between (now - High) and (now - Low) when BirthDateModeOptions = AgeRangeBirthDateOptions`` () =
        let birthDateOptions = AgeRangeBirthDateOptions(25, 30)

        let result = generateBirthDate random birthDateOptions
        Assert.IsTrue(DateTime.Today.Year - birthDateOptions.High <= result.Year && result.Year <= DateTime.Today.Year - birthDateOptions.Low)
