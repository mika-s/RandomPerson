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
    abstract member CreatePerson: Nationality -> Person
    abstract member CreatePerson: Nationality * RandomPersonOptions -> Person
    abstract member CreatePeople: int * Nationality -> Person seq
    abstract member CreatePeople: int * Nationality * RandomPersonOptions -> Person seq
    abstract member CreatePeopleTemplated: int * Nationality * string -> string seq
    abstract member CreatePeopleTemplated: int * Nationality * string * RandomPersonOptions -> string seq
    abstract member ValidateSSN: Nationality * string -> bool

/// <summary>A service class for generating random persons or validating SSNs.</summary>
type RandomPerson() =
    let i = readInputFiles ()
    let defaultOptions = RandomPersonOptions(false, false, false, false, false, false)

    let createPerson (nationality: Nationality, options: RandomPersonOptions, random: Random) =
        match nationality with
        | Nationality.Danish    -> Person(nationality, i.generic, i.danish,    options, random)
        | Nationality.Dutch     -> Person(nationality, i.generic, i.dutch,     options, random)
        | Nationality.Finnish   -> Person(nationality, i.generic, i.finnish,   options, random)
        | Nationality.Icelandic -> Person(nationality, i.generic, i.icelandic, options, random)
        | Nationality.Norwegian -> Person(nationality, i.generic, i.norwegian, options, random)
        | Nationality.Swedish   -> Person(nationality, i.generic, i.swedish,   options, random)
        | _ -> invalidArg "nationality" "Illegal nationality."

    /// <summary>Create a Person object given a nationality.</summary>
    /// <param name="nationality">The nationality of the people to generate data for.</param>
    /// <returns>A Person object with random data.</returns>
    member this.CreatePerson (nationality: Nationality) = this.CreatePerson (nationality, defaultOptions)

    /// <summary>Create a Person object given a nationality and an options object.</summary>
    /// <param name="nationality">The nationality of the people to generate data for.</param>
    /// <param name="options">An object with options.</param>
    /// <returns>A Person object with random data.</returns>
    member __.CreatePerson (nationality: Nationality, options: RandomPersonOptions) =
        let random = getRandom options.Randomness.ManualSeed options.Randomness.Seed
        createPerson(nationality, options, random)

    /// <summary>Create a list of Person objects given a nationality.</summary>
    /// <param name="amount">Number of people to create data for.</param>
    /// <param name="nationality">The nationality of the people to generate data for.</param>
    /// <returns>A list of Person object with random data.</returns>
    member this.CreatePeople (amount: int, nationality: Nationality) = this.CreatePeople (amount, nationality, defaultOptions)

    /// <summary>Create a list of Person objects given a nationality and an options object.</summary>
    /// <param name="amount">Number of people to create data for.</param>
    /// <param name="nationality">The nationality of the poeple to generate data for.</param>
    /// <param name="options">An object with options.</param>
    /// <returns>A list of Person object with random data.</returns>
    member __.CreatePeople (amount: int, nationality: Nationality, options: RandomPersonOptions)  =
        let random = getRandom options.Randomness.ManualSeed options.Randomness.Seed
        [ 1 .. amount ] |> List.map (fun _ -> createPerson(nationality, options, random))

    /// Create a list of strings given a template string where certain values will be replaced.</summary>
    /// <param name="amount">Number of people to create data for.</param>
    /// <param name="nationality">The nationality of the poeple to generate data for.</param>
    /// <param name="outputString">The string to use as template for generated data.</param>
    /// <returns>A list of strings containing random data.</returns>
    member this.CreatePeopleTemplated (amount: int, nationality: Nationality, outputString: string) =
        this.CreatePeopleTemplated (amount, nationality, outputString, defaultOptions)

    /// <summary>Create a list of strings given a template string where certain values will be replaced.</summary>
    /// <param name="amount">Number of people to create data for.</param>
    /// <param name="nationality">The nationality of the person to generate data for.</param>
    /// <param name="outputString">The string to use as template for generated data.</param>
    /// <param name="options">An object with options.</param>
    /// <returns>A list of strings containing random data.</returns>
    member __.CreatePeopleTemplated (amount: int, nationality: Nationality, outputString: string, options: RandomPersonOptions) =
        let random = getRandom options.Randomness.ManualSeed options.Randomness.Seed
        [ 1 .. amount ] |> List.map (fun _ -> createPerson(nationality, options, random)) |> List.map (printForTemplateMode outputString)
        
    /// <summary>Validate an SSN for a given nationality.</summary>
    /// <param name="nationality">The nationality of the person to validate SSN for.</param>
    /// <param name="ssn">The SSN to validate.</param>
    /// <returns>true if valid SSN, false otherwise.</returns>
    member __.ValidateSSN (nationality: Nationality, ssn: string) =
        match nationality with
        | Nationality.Danish    -> validateDK ssn
        | Nationality.Dutch     -> validateNL ssn
        | Nationality.Finnish   -> validateFI ssn
        | Nationality.Icelandic -> validateIC ssn
        | Nationality.Norwegian -> validateNO ssn
        | Nationality.Swedish   -> validateSE ssn
        | _ -> invalidArg "nationality" "Illegal nationality."

    interface IRandomPerson with
        
        /// <summary>Create a Person object given a nationality.</summary>
        /// <param name="nationality">The nationality of the people to generate data for.</param>
        /// <returns>A Person object with random data.</returns>
        member this.CreatePerson (nationality: Nationality) =
            this.CreatePerson (nationality)

        /// <summary>Create a Person object given a nationality.</summary>
        /// <param name="nationality">The nationality of the people to generate data for.</param>
        /// <param name="options">An object with options.</param>
        /// <returns>A Person object with random data.</returns>
        member this.CreatePerson (nationality: Nationality, options: RandomPersonOptions) =
            this.CreatePerson (nationality, options)

        /// <summary>Create a list of Person objects given a nationality.</summary>
        /// <param name="amount">Number of people to create data for.</param>
        /// <param name="nationality">The nationality of the people to generate data for.</param>
        /// <returns>An IEnumerable of Person object with random data.</returns>
        member this.CreatePeople (amount: int, nationality: Nationality) =
            this.CreatePeople (amount, nationality) |> List.toSeq

        /// <summary>Create a list of Person objects given a nationality.</summary>
        /// <param name="amount">Number of people to create data for.</param>
        /// <param name="nationality">The nationality of the poeple to generate data for.</param>
        /// <param name="options">An object with options.</param>
        /// <returns>An IEnumerable of Person object with random data.</returns>
        member this.CreatePeople (amount: int, nationality: Nationality, options: RandomPersonOptions) =
            this.CreatePeople (amount, nationality, options) |> List.toSeq

        /// <summary>Create a list of strings given a template string where certain values will be replaced.</summary>
        /// <param name="amount">Number of people to create data for.</param>
        /// <param name="nationality">The nationality of the poeple to generate data for.</param>
        /// <param name="outputString">The string to use as template for generated data.</param>
        /// <returns>An IEnumerable of strings containing random data.</returns>
        member this.CreatePeopleTemplated (amount: int, nationality: Nationality, outputString: string) =
            this.CreatePeopleTemplated (amount, nationality, outputString) |> List.toSeq

        /// <summary>Create a list of strings given a template string where certain values will be replaced.</summary>
        /// <param name="amount">Number of people to create data for.</param>
        /// <param name="nationality">The nationality of the person to generate data for.</param>
        /// <param name="outputString">The string to use as template for generated data.</param>
        /// <param name="options">An object with options.</param>
        /// <returns>An IEnumerable of strings containing random data.</returns>
        member this.CreatePeopleTemplated (amount: int, nationality: Nationality, outputString: string, options: RandomPersonOptions) =
            this.CreatePeopleTemplated (amount, nationality, outputString, options) |> List.toSeq

        /// <summary>Validate an SSN for a given nationality.</summary>
        /// <param name="nationality">The nationality of the person to validate SSN for.</param>
        /// <param name="ssn">The SSN to validate.</param>
        /// <returns>true if valid SSN, false otherwise.</returns>
        member this.ValidateSSN (nationality: Nationality, ssn: string) =
            this.ValidateSSN (nationality, ssn)
