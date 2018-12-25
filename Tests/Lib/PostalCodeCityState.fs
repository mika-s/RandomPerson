namespace Tests

open Microsoft.VisualStudio.TestTools.UnitTesting
open RandomPersonLib
open PostalCodeCityStatesGen

[<TestClass>]
type ``stringToPostalCodeCityState should`` () =

    [<TestMethod>]
    member __.``split a string of Norwegian data into a PostalCodeCityState object`` () =
        let result = stringToPostalCodeCityState Country.Norway "0801	OSLO"

        Assert.AreEqual("0801", result.PostalCode)
        Assert.AreEqual("OSLO", result.City)
        Assert.AreEqual("", result.State)

    [<TestMethod>]
    member __.``split a string of Swedish data into a PostalCodeCityState object`` () =
        let result = stringToPostalCodeCityState Country.Sweden "SE	186 00	Vallentuna	Stockholm	AB	Vallentuna	0115			59.5344	18.0776	4"

        Assert.AreEqual("186 00", result.PostalCode)
        Assert.AreEqual("Vallentuna", result.City)
        Assert.AreEqual("", result.State)

        
