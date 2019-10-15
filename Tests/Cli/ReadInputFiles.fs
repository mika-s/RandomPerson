namespace Tests

open System
open Microsoft.VisualStudio.TestTools.UnitTesting
open ReadInputFiles
open Settings

[<TestClass>]
type ``assertDates should`` () =

    [<TestMethod>]
    member __.``return unit if the birth date options are correct`` () =
        let birthDateOptions = {
            BirthDateMode = "ManualCalendarYearRange"
            Low = Nullable<int>(1910)
            High = Nullable<int>(2002)
            ManualBirthDate = ""
        }

        assertDates birthDateOptions

        Assert.AreEqual(1, 1)
        Assert.AreNotEqual(2, 1)    // negative test

    [<TestMethod>]
    [<ExpectedException(typedefof<ArgumentException>)>]
    member __.``throw an exception if Low is below 1800 and BirthDateMode = ManualCalendarYearRange`` () =
        let birthDateOptions = {
            BirthDateMode = "ManualCalendarYearRange"
            Low = Nullable<int>(1700)
            High = Nullable<int>(2002)
            ManualBirthDate = ""
        }

        assertDates birthDateOptions

    [<TestMethod>]
    [<ExpectedException(typedefof<ArgumentException>)>]
    member __.``throw an exception if High is above 2050 and BirthDateMode = ManualCalendarYearRange`` () =
        let birthDateOptions = {
            BirthDateMode = "ManualCalendarYearRange"
            Low = Nullable<int>(1900)
            High = Nullable<int>(2060)
            ManualBirthDate = ""
        }

        assertDates birthDateOptions

    [<TestMethod>]
    [<ExpectedException(typedefof<ArgumentException>)>]
    member __.``throw an exception if Low is below 1 and BirthDateMode = ManualAgeRange`` () =
        let birthDateOptions = {
            BirthDateMode = "ManualAgeRange"
            Low = Nullable<int>(0)
            High = Nullable<int>(90)
            ManualBirthDate = ""
        }

        assertDates birthDateOptions

    [<TestMethod>]
    [<ExpectedException(typedefof<ArgumentException>)>]
    member __.``throw an exception if High is above 150 and BirthDateMode = ManualAgeRange`` () =
        let birthDateOptions = {
            BirthDateMode = "ManualAgeRange"
            Low = Nullable<int>(18)
            High = Nullable<int>(160)
            ManualBirthDate = ""
        }

        assertDates birthDateOptions

    [<TestMethod>]
    [<ExpectedException(typedefof<ArgumentException>)>]
    member __.``throw an exception if Low is above High and BirthDateMode = ManualCalendarYearRange`` () =
        let birthDateOptions = {
            BirthDateMode = "ManualCalendarYearRange"
            Low = Nullable<int>(1950)
            High = Nullable<int>(1940)
            ManualBirthDate = ""
        }

        assertDates birthDateOptions

    [<TestMethod>]
    [<ExpectedException(typedefof<ArgumentException>)>]
    member __.``throw an exception if Low is above High and BirthDateMode = ManualAgeRange`` () =
        let birthDateOptions = {
            BirthDateMode = "ManualAgeRange"
            Low = Nullable<int>(50)
            High = Nullable<int>(40)
            ManualBirthDate = ""
        }

        assertDates birthDateOptions
