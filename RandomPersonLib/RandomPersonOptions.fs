namespace RandomPersonLib

type BirthDateOptions = {
        SetYearManually: bool;
        SetUsingAge: bool;
        Low: int;
        High: int;
}

type RandomnessOptions = {
        ManualSeed: bool;
        Seed: int;
}

type RandomPersonOptions (
                          anonymizeSSN: bool,
                          under18: bool,
                          addCountryCodeToPhoneNumber: bool,
                          removeHyphenFromPhoneNumber: bool,
                          removeSpaceFromPhoneNumber: bool,
                          removeHyphenFromSSN: bool
                         ) =

    let birthDateOptions = {
        SetYearManually = false
        SetUsingAge = false
        Low = 1900
        High = 2000
    }

    let randomnessOptions = {
        ManualSeed = false
        Seed = 1
    }

    new ()
        = RandomPersonOptions(false, false, false, false, false, false)

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
    member val BirthDateOptions = birthDateOptions with get, set
    member val RandomnessOptions = randomnessOptions with get, set
