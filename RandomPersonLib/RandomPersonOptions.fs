namespace RandomPersonLib

type BirthDateOptions (setYearManually: bool, setUsingAge: bool, low: int, high: int) = 
    new () = BirthDateOptions(false, false, 1920, 2000)

    member val SetYearManually = setYearManually with get, set
    member val SetUsingAge = setUsingAge with get, set
    member val Low = low with get, set
    member val High = high with get, set

type RandomnessOptions (manualSeed: bool, seed: int) = 
    new () = RandomnessOptions(false, 1)

    member val ManualSeed = manualSeed with get, set
    member val Seed = seed with get, set

type RandomPersonOptions (
                          anonymizeSSN: bool,
                          under18: bool,
                          addCountryCodeToPhoneNumber: bool,
                          removeHyphenFromPhoneNumber: bool,
                          removeSpaceFromPhoneNumber: bool,
                          removeHyphenFromSSN: bool
                         ) =

    let birthDate  = BirthDateOptions()
    let randomness = RandomnessOptions()

    new () = RandomPersonOptions(false, false, false, false, false, false)

    new (anonymizeSSN: bool)
        = RandomPersonOptions(anonymizeSSN, false, false, false, false, false)

    new (anonymizeSSN: bool, under18: bool)
        = RandomPersonOptions(anonymizeSSN, under18, false, false, false, false)

    member val AnonymizeSSN = anonymizeSSN with get, set
    member val Under18 = under18 with get, set
    member val AddCountryCodeToPhoneNumber = addCountryCodeToPhoneNumber with get, set
    member val RemoveHyphenFromPhoneNumber = removeHyphenFromPhoneNumber with get, set
    member val RemoveSpaceFromPhoneNumber = removeSpaceFromPhoneNumber with get, set
    member val RemoveHyphenFromSSN = removeHyphenFromSSN with get, set
    member val BirthDate = birthDate with get, set
    member val Randomness = randomness with get, set
