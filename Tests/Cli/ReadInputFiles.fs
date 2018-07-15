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
            SetYearManually = Nullable<bool>(true);
            SetUsingAge = Nullable<bool>(false);
            Low = Nullable<int>(1910);
            High = Nullable<int>(2002);
        }

        assertDates birthDateOptions

        Assert.AreEqual(1, 1)
        Assert.AreNotEqual(2, 1)    // negative test

    [<TestMethod>]
    [<ExpectedException(typedefof<ArgumentException>)>]
    member __.``throw an exception if Low is below 1800, SetYearManually is true and SetUsingAge is false`` () =
        let birthDateOptions = {
            SetYearManually = Nullable<bool>(true);
            SetUsingAge = Nullable<bool>(false);
            Low = Nullable<int>(1700);
            High = Nullable<int>(2002);
        }

        assertDates birthDateOptions

    [<TestMethod>]
    [<ExpectedException(typedefof<ArgumentException>)>]
    member __.``throw an exception if High is above 2050, SetYearManually is true and SetUsingAge is false`` () =
        let birthDateOptions = {
            SetYearManually = Nullable<bool>(true);
            SetUsingAge = Nullable<bool>(false);
            Low = Nullable<int>(1900);
            High = Nullable<int>(2060);
        }

        assertDates birthDateOptions

    [<TestMethod>]
    [<ExpectedException(typedefof<ArgumentException>)>]
    member __.``throw an exception if Low is below 1, SetYearManually is true and SetUsingAge is true`` () =
        let birthDateOptions = {
            SetYearManually = Nullable<bool>(true);
            SetUsingAge = Nullable<bool>(true);
            Low = Nullable<int>(0);
            High = Nullable<int>(90);
        }

        assertDates birthDateOptions

    [<TestMethod>]
    [<ExpectedException(typedefof<ArgumentException>)>]
    member __.``throw an exception if High is above 150, SetYearManually is true and SetUsingAge is true`` () =
        let birthDateOptions = {
            SetYearManually = Nullable<bool>(true);
            SetUsingAge = Nullable<bool>(true);
            Low = Nullable<int>(18);
            High = Nullable<int>(160);
        }

        assertDates birthDateOptions

    [<TestMethod>]
    [<ExpectedException(typedefof<ArgumentException>)>]
    member __.``throw an exception if Low is above High, SetYearManually is true and SetUsingAge is false`` () =
        let birthDateOptions = {
            SetYearManually = Nullable<bool>(true);
            SetUsingAge = Nullable<bool>(false);
            Low = Nullable<int>(1950);
            High = Nullable<int>(1940);
        }

        assertDates birthDateOptions

    [<TestMethod>]
    [<ExpectedException(typedefof<ArgumentException>)>]
    member __.``throw an exception if Low is above High, SetYearManually is true and SetUsingAge is true`` () =
        let birthDateOptions = {
            SetYearManually = Nullable<bool>(true);
            SetUsingAge = Nullable<bool>(true);
            Low = Nullable<int>(50);
            High = Nullable<int>(40);
        }

        assertDates birthDateOptions
