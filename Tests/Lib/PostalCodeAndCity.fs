namespace Tests

open Microsoft.VisualStudio.TestTools.UnitTesting
open RandomPersonLib
open PostalCodeAndCityGen

[<TestClass>]
type ``stringToPostalCodeAndCity should`` () =

    [<TestMethod>]
    member __.``split a string of Norwegian data into a PostalCodeAndCity object`` () =
        let result = stringToPostalCodeAndCity Nationality.Norwegian "0801	OSLO"

        Assert.AreEqual("0801", result.PostalCode)
        Assert.AreEqual("OSLO", result.City)

    [<TestMethod>]
    member __.``split a string of Swedish data into a PostalCodeAndCity object`` () =
        let result = stringToPostalCodeAndCity Nationality.Swedish "SE	186 00	Vallentuna	Stockholm	AB	Vallentuna	0115			59.5344	18.0776	4"

        Assert.AreEqual("186 00", result.PostalCode)
        Assert.AreEqual("Vallentuna", result.City)
