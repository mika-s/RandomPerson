module Settings

open System
open System.Runtime.Serialization
open RandomPersonLib
open CliUtil

[<DataContract>]
type genericPrintSettings = {
    [<field : DataMember(Name="Label")>]
    Label : bool

    [<field : DataMember(Name="FirstName")>]
    FirstName : bool

    [<field : DataMember(Name="LastName")>]
    LastName : bool

    [<field : DataMember(Name="SSN")>]
    SSN : bool

    [<field : DataMember(Name="Nationality")>]
    Nationality : bool

    [<field : DataMember(Name="Address1")>]
    Address1 : bool

    [<field : DataMember(Name="Address2")>]
    Address2 : bool

    [<field : DataMember(Name="PostalCode")>]
    PostalCode : bool

    [<field : DataMember(Name="City")>]
    City : bool

    [<field : DataMember(Name="BirthDate")>]
    BirthDate : bool

    [<field : DataMember(Name="Gender")>]
    Gender : bool

    [<field : DataMember(Name="Email")>]
    Email : bool

    [<field : DataMember(Name="Password")>]
    Password : bool

    [<field : DataMember(Name="MobilePhone")>]
    MobilePhone : bool

    [<field : DataMember(Name="HomePhone")>]
    HomePhone : bool
}

[<DataContract>]
type printOptionsSettings = {
    [<field : DataMember(Name="JsonPrintType")>]
    JsonPrintType : string

    [<field : DataMember(Name="JsonPrettyPrint")>]
    JsonPrettyPrint : bool

    [<field : DataMember(Name="XmlPrettyPrint")>]
    XmlPrettyPrint : bool 
}

[<DataContract>]
type templatePrintSettings = {
    [<field : DataMember(Name="Output")>]
    Output : string
}

[<DataContract>]
type birthDateOptionsSettings = {
    [<field : DataMember(Name="SetYearManually")>]
    SetYearManually : Nullable<bool>

    [<field : DataMember(Name="SetUsingAge")>]
    SetUsingAge : Nullable<bool>

    [<field : DataMember(Name="Low")>]
    Low : Nullable<int>

    [<field : DataMember(Name="High")>]
    High : Nullable<int>
}

[<DataContract>]
type randomnessOptionsSettings = {
    [<field : DataMember(Name="ManualSeed")>]
    ManualSeed : Nullable<bool>

    [<field : DataMember(Name="Seed")>]
    Seed : Nullable<int>
}

[<DataContract>]
type genericOptionsSettings = {
    [<field : DataMember(Name="AnonymizeSSN")>]
    AnonymizeSSN : Nullable<bool>

    [<field : DataMember(Name="Under18")>]
    Under18 : Nullable<bool>

    [<field : DataMember(Name="AddCountryCodeToPhoneNumber")>]
    AddCountryCodeToPhoneNumber : Nullable<bool>

    [<field : DataMember(Name="RemoveHyphenFromPhoneNumber")>]
    RemoveHyphenFromPhoneNumber : Nullable<bool>

    [<field : DataMember(Name="RemoveSpaceFromPhoneNumber")>]
    RemoveSpaceFromPhoneNumber : Nullable<bool>

    [<field : DataMember(Name="RemoveHyphenFromSSN")>]
    RemoveHyphenFromSSN : Nullable<bool>

    [<field : DataMember(Name="BirthDate")>]
    BirthDate : birthDateOptionsSettings

    [<field : DataMember(Name="Randomness")>]
    Randomness : randomnessOptionsSettings
}

[<DataContract>]
type interactiveModeSettings = {
    [<field : DataMember(Name="Options")>]
    Options : genericOptionsSettings

    [<field : DataMember(Name="ConsolePrint")>]
    ConsolePrint : genericPrintSettings
}

[<DataContract>]
type listModeSettings = {
    [<field : DataMember(Name="Options")>]
    Options : genericOptionsSettings

    [<field : DataMember(Name="PrintOptions")>]
    PrintOptions : printOptionsSettings

    [<field : DataMember(Name="ConsolePrint")>]
    ConsolePrint : genericPrintSettings

    [<field : DataMember(Name="FilePrint")>]
    FilePrint : genericPrintSettings
}

[<DataContract>]
type templateModeSettings = {
    [<field : DataMember(Name="Options")>]
    Options : genericOptionsSettings

    [<field : DataMember(Name="Print")>]
    Print : templatePrintSettings
}

[<DataContract>]
type Settings = {
    [<field : DataMember(Name="InteractiveMode")>]
    InteractiveMode: interactiveModeSettings

    [<field : DataMember(Name="ListMode")>]
    ListMode: listModeSettings

    [<field : DataMember(Name="TemplateMode")>]
    TemplateMode: templateModeSettings
}

let genericOptionsToRandomPersonOptions (genericOptions: genericOptionsSettings) =
    let defaultAnonymizeSSN = false
    let defaultUnder18 = false
    let defaultAddCountryCodeToPhoneNumber = false
    let defaultRemoveHyphenFromPhoneNumber = false
    let defaultRemoveSpaceFromPhoneNumber  = false
    let defaultRemoveHyphenFromSSN = false
    let defaultSetYearManually = false
    let defaultSetUsingAge = false
    let defaultBirthDateLow = 1920
    let defaultBirthDateHigh = 2000
    let defaultManualSeed = false
    let defaultSeed = 1

    let finalAnonymizeSSN                = nullCoalesce genericOptions.AnonymizeSSN                  defaultAnonymizeSSN
    let finalUnder18                     = nullCoalesce genericOptions.Under18                       defaultUnder18
    let finalAddCountryCodeToPhoneNumber = nullCoalesce genericOptions.AddCountryCodeToPhoneNumber   defaultAddCountryCodeToPhoneNumber
    let finalRemoveHyphenFromPhoneNumber = nullCoalesce genericOptions.RemoveHyphenFromPhoneNumber   defaultRemoveHyphenFromPhoneNumber
    let finalRemoveSpaceFromPhoneNumber  = nullCoalesce genericOptions.RemoveSpaceFromPhoneNumber    defaultRemoveSpaceFromPhoneNumber
    let finalRemoveHyphenFromSSN         = nullCoalesce genericOptions.RemoveHyphenFromSSN           defaultRemoveHyphenFromSSN
    let finalSetYearManually             = nullCoalesce genericOptions.BirthDate.SetYearManually     defaultSetYearManually
    let finalSetUsingAge                 = nullCoalesce genericOptions.BirthDate.SetUsingAge         defaultSetUsingAge
    let finalBirthDateLow                = nullCoalesce genericOptions.BirthDate.Low                 defaultBirthDateLow
    let finalBirthDateHigh               = nullCoalesce genericOptions.BirthDate.High                defaultBirthDateHigh
    let finalManualSeed                  = nullCoalesce genericOptions.Randomness.ManualSeed         defaultManualSeed
    let finalSeed                        = nullCoalesce genericOptions.Randomness.Seed               defaultSeed

    let randomPersonOptions =
        RandomPersonOptions(
            finalAnonymizeSSN,
            finalUnder18,
            finalAddCountryCodeToPhoneNumber,
            finalRemoveHyphenFromPhoneNumber,
            finalRemoveSpaceFromPhoneNumber,
            finalRemoveHyphenFromSSN
        )

    randomPersonOptions.BirthDate  <- BirthDateOptions(finalSetYearManually, finalSetUsingAge, finalBirthDateLow, finalBirthDateHigh)
    randomPersonOptions.Randomness <- RandomnessOptions(finalManualSeed, finalSeed)

    randomPersonOptions
