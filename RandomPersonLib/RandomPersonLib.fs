namespace RandomPersonLib

open System
open System.Runtime.CompilerServices
open RandomPersonLib
open RandomUtil
open ReadInputFiles
open ValidatePAN
open ValidateSSN
open TemplatePrint
open Util

[<assembly: InternalsVisibleTo("Tests")>]
do()

type IRandomPerson =
    abstract member CreatePerson: Country -> Person
    abstract member CreatePerson: Country * RandomPersonOptions -> Person
    abstract member CreatePeople: int * Country -> Person seq
    abstract member CreatePeople: int * Country * RandomPersonOptions -> Person seq
    abstract member CreatePeopleTemplated: int * Country * string -> string seq
    abstract member CreatePeopleTemplated: int * Country * string * RandomPersonOptions -> string seq

type IValidatePerson =
    abstract member ValidatePAN: string -> bool * string
    abstract member ValidateSSN: Country * string -> bool * string

type RandomPerson() =
    let i = readInputFiles ()
    let defaultOptions = RandomPersonOptions(false, false, false, false, false, false, false, false, false, DefaultYearRangeBirthDateOptions(false))

    let createPerson (country: Country, options: RandomPersonOptions, random: Random) =
        match country with
        | Country.Denmark     -> Person(country, i.generic, i.denmark,     options, random)
        | Country.Finland     -> Person(country, i.generic, i.finland,     options, random)
        | Country.France      -> Person(country, i.generic, i.france,      options, random)
        | Country.Iceland     -> Person(country, i.generic, i.iceland,     options, random)
        | Country.Netherlands -> Person(country, i.generic, i.netherlands, options, random)
        | Country.Norway      -> Person(country, i.generic, i.norway,      options, random)
        | Country.Sweden      -> Person(country, i.generic, i.sweden,      options, random)
        | Country.USA         -> Person(country, i.generic, i.usa,         options, random)
        | _ -> invalidArg "country" "Illegal country."

    member this.CreatePerson (country: Country) = this.CreatePerson (country, defaultOptions)

    member __.CreatePerson (country: Country, options: RandomPersonOptions) =
        let random = getRandom options.Randomness.ManualSeed options.Randomness.Seed
        createPerson(country, options, random)

    member this.CreatePeople (amount: int, country: Country) = this.CreatePeople (amount, country, defaultOptions)

    member __.CreatePeople (amount: int, country: Country, options: RandomPersonOptions)  =
        let random = getRandom options.Randomness.ManualSeed options.Randomness.Seed
        [ 1 .. amount ] |> List.map (fun _ -> createPerson(country, options, random))

    member this.CreatePeopleTemplated (amount: int, country: Country, outputString: string) =
        this.CreatePeopleTemplated (amount, country, outputString, defaultOptions)

    member __.CreatePeopleTemplated (amount: int, country: Country, outputString: string, options: RandomPersonOptions) =
        let random = getRandom options.Randomness.ManualSeed options.Randomness.Seed
        [ 1 .. amount ] |> List.map (fun _ -> createPerson(country, options, random)) |> List.map (printForTemplateMode outputString)

    interface IRandomPerson with
        
        member this.CreatePerson (country: Country) =
            this.CreatePerson (country)

        member this.CreatePerson (country: Country, options: RandomPersonOptions) =
            this.CreatePerson (country, options)

        member this.CreatePeople (amount: int, country: Country) =
            this.CreatePeople (amount, country) |> List.toSeq

        member this.CreatePeople (amount: int, country: Country, options: RandomPersonOptions) =
            this.CreatePeople (amount, country, options) |> List.toSeq

        member this.CreatePeopleTemplated (amount: int, country: Country, outputString: string) =
            this.CreatePeopleTemplated (amount, country, outputString) |> List.toSeq

        member this.CreatePeopleTemplated (amount: int, country: Country, outputString: string, options: RandomPersonOptions) =
            this.CreatePeopleTemplated (amount, country, outputString, options) |> List.toSeq

type ValidatePerson() =

    member __.ValidatePAN (pan: string) =
        validatePAN pan

    member __.ValidateSSN (country: Country, ssn: string) =
        validateSSN country ssn

    interface IValidatePerson with

        member this.ValidatePAN (pan: string) =
            this.ValidatePAN (pan)

        member this.ValidateSSN (country: Country, ssn: string) =
            this.ValidateSSN (country, ssn)

[<AbstractClass; Sealed>]
type RandomPersonUtil =

    static member RandomIntBetween (min: int, max: int) =
        randomIntBetween min max

    static member RandomIntBetween (min: int, step: int, max: int) =
        randomIntBetweenWithStep min step max

    static member RandomFloatBetween (min: float, max: float) =
        randomFloatBetween min max

    static member RandomFloatBetween (min: float, step: float, max: float) =
        randomFloatBetweenWithStep min step max

    static member GenerateNormallyDistributedInt (mean: int, std: int) =
        normallyDistributedInt mean std

    static member GenerateNormallyDistributedFloat (mean: float, std: float) =
        normallyDistributedFloat mean std
