namespace RandomPersonLib

open System
open RandomUtil

/// <summary>Interface to enable polymorphism for birth date options.</summary>
type IBirthDateOptions =
    abstract member GetBirthDate: Random -> DateTime

/// A subclass used by RandomPersonOptions, containing birthdate options.
[<NoEquality;NoComparison>]
type ManualBirthDateOptions (manualBirthDate: DateTime) =

    member val ManualBirthDate = manualBirthDate with get, set
    
    interface IBirthDateOptions with

        member this.GetBirthDate (_: Random) = this.ManualBirthDate

/// A subclass used by RandomPersonOptions, containing birthdate options.
[<NoEquality;NoComparison>]
type DefaultYearRangeBirthDateOptions (isAllowingUnder18: bool) =

    let low  = 1920

    member val Low = low with get, set
    member val IsAllowingUnder18 = isAllowingUnder18 with get, set

    interface IBirthDateOptions with

        member this.GetBirthDate (random: Random) =

            let minLegalBirthDate = if isAllowingUnder18      then DateTime.Today else DateTime(this.Low, 1, 1)
            let maxLegalBirthDate = if this.IsAllowingUnder18 then DateTime.Today else DateTime.Today.Subtract(TimeSpan.FromDays(365.0*18.5))

            generateRandomDateBetween random minLegalBirthDate.Year maxLegalBirthDate.Year

/// A subclass used by RandomPersonOptions, containing birthdate options.
[<NoEquality;NoComparison>]
type CalendarYearRangeBirthDateOptions (low: int, high: int) =

    member val Low = low with get, set
    member val High = high with get, set

    interface IBirthDateOptions with

        member this.GetBirthDate (random: Random) =

            let minLegalBirthDate = DateTime(this.Low,  1, 1)
            let maxLegalBirthDate = DateTime(this.High, 1, 1)

            generateRandomDateBetween random minLegalBirthDate.Year maxLegalBirthDate.Year

/// A subclass used by RandomPersonOptions, containing birthdate options.
[<NoEquality;NoComparison>]
type AgeRangeBirthDateOptions (low: int, high: int) =

    member val Low = low with get, set
    member val High = high with get, set

    interface IBirthDateOptions with

        member this.GetBirthDate (random: Random) =

            let minLegalBirthDate = DateTime.Now.Subtract(TimeSpan(365 * this.High, 0, 0, 0))
            let maxLegalBirthDate = DateTime.Now.Subtract(TimeSpan(365 * this.Low, 0, 0, 0))

            generateRandomDateBetween random minLegalBirthDate.Year maxLegalBirthDate.Year

/// A subclass used by RandomPersonOptions, containing randomness options.
[<NoEquality;NoComparison>]
type RandomnessOptions (manualSeed: bool, seed: int) = 
    new () = RandomnessOptions(false, 1)

    member val ManualSeed = manualSeed with get, set
    member val Seed = seed with get, set

/// A subclass used by RandomPersonOptions, containing creditcard options.
[<NoEquality;NoComparison>]
type CreditcardOptions (cardIssuer: CardIssuer, pinLength: int) = 
    new () = CreditcardOptions(CardIssuer.Visa, 4)

    member val CardIssuer = cardIssuer with get, set
    member val PinLength = pinLength with get, set

/// The options class for the RandomPerson library.
[<NoEquality;NoComparison>]
type RandomPersonOptions (
                          anonymizeSSN: bool,
                          under18: bool,
                          addCountryCodeToPhoneNumber: bool,
                          removeHyphenFromPhoneNumber: bool,
                          removeSpaceFromPhoneNumber: bool,
                          removeHyphenFromSSN: bool,
                          removeSpacesFromPAN: bool,
                          useColonsInMacAddress: bool,
                          useUppercaseInMacAddress: bool,
                          birthDate: IBirthDateOptions
                         ) =

    let randomness = RandomnessOptions()
    let creditcard = CreditcardOptions()

    new () = RandomPersonOptions(false, false, false, false, false, false, false, false, false,
                                 DefaultYearRangeBirthDateOptions(false))

    new (anonymizeSSN: bool)
        = RandomPersonOptions(anonymizeSSN, false, false, false, false, false, false, false, false, DefaultYearRangeBirthDateOptions(false))

    new (anonymizeSSN: bool, under18: bool)
        = RandomPersonOptions(anonymizeSSN, under18, false, false, false, false, false, false, false, DefaultYearRangeBirthDateOptions(false))

    member val AnonymizeSSN = anonymizeSSN with get, set
    member val Under18 = under18 with get, set
    member val AddCountryCodeToPhoneNumber = addCountryCodeToPhoneNumber with get, set
    member val RemoveHyphenFromPhoneNumber = removeHyphenFromPhoneNumber with get, set
    member val RemoveSpaceFromPhoneNumber = removeSpaceFromPhoneNumber with get, set
    member val RemoveHyphenFromSSN = removeHyphenFromSSN with get, set
    member val RemoveSpacesFromPAN = removeSpacesFromPAN with get, set
    member val UseColonsInMacAddress = useColonsInMacAddress with get, set
    member val UseUppercaseInMacAddress = useUppercaseInMacAddress with get, set
    member val Creditcard = creditcard with get, set
    member val BirthDate = birthDate with get, set
    member val Randomness = randomness with get, set
