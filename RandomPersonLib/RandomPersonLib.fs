namespace RandomPersonLib

open System
open System.Runtime.CompilerServices
open RandomPersonLib
open ReadInputFiles
open Validate
open TemplatePrint
open Util

[<assembly: InternalsVisibleTo("Tests")>]
do()

/// <summary>Interface for RandomPerson for use with C#.</summary>
type IRandomPerson =
    abstract member CreatePerson: Country -> Person
    abstract member CreatePerson: Country * RandomPersonOptions -> Person
    abstract member CreatePeople: int * Country -> Person seq
    abstract member CreatePeople: int * Country * RandomPersonOptions -> Person seq
    abstract member CreatePeopleTemplated: int * Country * string -> string seq
    abstract member CreatePeopleTemplated: int * Country * string * RandomPersonOptions -> string seq
    abstract member ValidateSSN: Country * string -> bool

/// <summary>A service class for generating random persons or validating SSNs.</summary>
type RandomPerson() =
    let i = readInputFiles ()
    let defaultOptions = RandomPersonOptions(false, false, false, false, false, false)

    let createPerson (country: Country, options: RandomPersonOptions, random: Random) =
        match country with
        | Country.Denmark     -> Person(country, i.generic, i.denmark,     options, random)
        | Country.Finland     -> Person(country, i.generic, i.finland,     options, random)
        | Country.Iceland     -> Person(country, i.generic, i.iceland,     options, random)
        | Country.Netherlands -> Person(country, i.generic, i.netherlands, options, random)
        | Country.Norway      -> Person(country, i.generic, i.norway,      options, random)
        | Country.Sweden      -> Person(country, i.generic, i.sweden,      options, random)
        | Country.USA         -> Person(country, i.generic, i.usa,         options, random)
        | _ -> invalidArg "country" "Illegal country."

    /// <summary>Create a Person object given a country.</summary>
    /// <param name="country">The country of the people to generate data for.</param>
    /// <returns>A Person object with random data.</returns>
    member this.CreatePerson (country: Country) = this.CreatePerson (country, defaultOptions)

    /// <summary>Create a Person object given a country and an options object.</summary>
    /// <param name="country">The country of the people to generate data for.</param>
    /// <param name="options">An object with options.</param>
    /// <returns>A Person object with random data.</returns>
    member __.CreatePerson (country: Country, options: RandomPersonOptions) =
        let random = getRandom options.Randomness.ManualSeed options.Randomness.Seed
        createPerson(country, options, random)

    /// <summary>Create a list of Person objects given a country.</summary>
    /// <param name="amount">Number of people to create data for.</param>
    /// <param name="country">The country of the people to generate data for.</param>
    /// <returns>A list of Person object with random data.</returns>
    member this.CreatePeople (amount: int, country: Country) = this.CreatePeople (amount, country, defaultOptions)

    /// <summary>Create a list of Person objects given a country and an options object.</summary>
    /// <param name="amount">Number of people to create data for.</param>
    /// <param name="country">The country of the poeple to generate data for.</param>
    /// <param name="options">An object with options.</param>
    /// <returns>A list of Person object with random data.</returns>
    member __.CreatePeople (amount: int, country: Country, options: RandomPersonOptions)  =
        let random = getRandom options.Randomness.ManualSeed options.Randomness.Seed
        [ 1 .. amount ] |> List.map (fun _ -> createPerson(country, options, random))

    /// Create a list of strings given a template string where certain values will be replaced.</summary>
    /// <param name="amount">Number of people to create data for.</param>
    /// <param name="country">The country of the poeple to generate data for.</param>
    /// <param name="outputString">The string to use as template for generated data.</param>
    /// <returns>A list of strings containing random data.</returns>
    member this.CreatePeopleTemplated (amount: int, country: Country, outputString: string) =
        this.CreatePeopleTemplated (amount, country, outputString, defaultOptions)

    /// <summary>Create a list of strings given a template string where certain values will be replaced.</summary>
    /// <param name="amount">Number of people to create data for.</param>
    /// <param name="country">The country of the person to generate data for.</param>
    /// <param name="outputString">The string to use as template for generated data.</param>
    /// <param name="options">An object with options.</param>
    /// <returns>A list of strings containing random data.</returns>
    member __.CreatePeopleTemplated (amount: int, country: Country, outputString: string, options: RandomPersonOptions) =
        let random = getRandom options.Randomness.ManualSeed options.Randomness.Seed
        [ 1 .. amount ] |> List.map (fun _ -> createPerson(country, options, random)) |> List.map (printForTemplateMode outputString)
        
    /// <summary>Validate an SSN for a given country.</summary>
    /// <param name="country">The country of the person to validate SSN for.</param>
    /// <param name="ssn">The SSN to validate.</param>
    /// <returns>true if valid SSN, false otherwise.</returns>
    member __.ValidateSSN (country: Country, ssn: string) =
        match country with
        | Country.Denmark     -> validateDK ssn
        | Country.Finland     -> validateFI ssn
        | Country.Iceland     -> validateIC ssn
        | Country.Netherlands -> validateNL ssn
        | Country.Norway      -> validateNO ssn
        | Country.Sweden      -> validateSE ssn
        | Country.USA         -> validateUS ssn
        | _ -> invalidArg "country" "Illegal country."

    interface IRandomPerson with
        
        /// <summary>Create a Person object given a country.</summary>
        /// <param name="country">The country of the people to generate data for.</param>
        /// <returns>A Person object with random data.</returns>
        member this.CreatePerson (country: Country) =
            this.CreatePerson (country)

        /// <summary>Create a Person object given a country.</summary>
        /// <param name="country">The country of the people to generate data for.</param>
        /// <param name="options">An object with options.</param>
        /// <returns>A Person object with random data.</returns>
        member this.CreatePerson (country: Country, options: RandomPersonOptions) =
            this.CreatePerson (country, options)

        /// <summary>Create a list of Person objects given a country.</summary>
        /// <param name="amount">Number of people to create data for.</param>
        /// <param name="country">The country of the people to generate data for.</param>
        /// <returns>An IEnumerable of Person object with random data.</returns>
        member this.CreatePeople (amount: int, country: Country) =
            this.CreatePeople (amount, country) |> List.toSeq

        /// <summary>Create a list of Person objects given a country.</summary>
        /// <param name="amount">Number of people to create data for.</param>
        /// <param name="country">The country of the poeple to generate data for.</param>
        /// <param name="options">An object with options.</param>
        /// <returns>An IEnumerable of Person object with random data.</returns>
        member this.CreatePeople (amount: int, country: Country, options: RandomPersonOptions) =
            this.CreatePeople (amount, country, options) |> List.toSeq

        /// <summary>Create a list of strings given a template string where certain values will be replaced.</summary>
        /// <param name="amount">Number of people to create data for.</param>
        /// <param name="country">The country of the poeple to generate data for.</param>
        /// <param name="outputString">The string to use as template for generated data.</param>
        /// <returns>An IEnumerable of strings containing random data.</returns>
        member this.CreatePeopleTemplated (amount: int, country: Country, outputString: string) =
            this.CreatePeopleTemplated (amount, country, outputString) |> List.toSeq

        /// <summary>Create a list of strings given a template string where certain values will be replaced.</summary>
        /// <param name="amount">Number of people to create data for.</param>
        /// <param name="country">The country of the person to generate data for.</param>
        /// <param name="outputString">The string to use as template for generated data.</param>
        /// <param name="options">An object with options.</param>
        /// <returns>An IEnumerable of strings containing random data.</returns>
        member this.CreatePeopleTemplated (amount: int, country: Country, outputString: string, options: RandomPersonOptions) =
            this.CreatePeopleTemplated (amount, country, outputString, options) |> List.toSeq

        /// <summary>Validate an SSN for a given country.</summary>
        /// <param name="country">The country of the person to validate SSN for.</param>
        /// <param name="ssn">The SSN to validate.</param>
        /// <returns>true if valid SSN, false otherwise.</returns>
        member this.ValidateSSN (country: Country, ssn: string) =
            this.ValidateSSN (country, ssn)
