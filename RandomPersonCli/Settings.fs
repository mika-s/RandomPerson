module Settings

open System
open System.Runtime.Serialization
open RandomPersonLib
open CliUtil

[<DataContract;NoEquality;NoComparison>]
type GenericPrintSettings = {
    [<field : DataMember(Name="Label")>]
    Label : bool

    [<field : DataMember(Name="FirstName")>]
    FirstName : bool

    [<field : DataMember(Name="LastName")>]
    LastName : bool

    [<field : DataMember(Name="SSN")>]
    SSN : bool

    [<field : DataMember(Name="Country")>]
    Country : bool

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
    
    [<field : DataMember(Name="CountryNameEnglish")>]
    CountryNameEnglish : bool
    
    [<field : DataMember(Name="CountryNameNative")>]
    CountryNameNative : bool
    
    [<field : DataMember(Name="CountryNameNativeAlternative1")>]
    CountryNameNativeAlternative1 : bool
    
    [<field : DataMember(Name="CountryNameNativeAlternative2")>]
    CountryNameNativeAlternative2 : bool

    [<field : DataMember(Name="CountryCode2")>]
    CountryCode2 : bool
    
    [<field : DataMember(Name="CountryCode3")>]
    CountryCode3 : bool
    
    [<field : DataMember(Name="CountryNumber")>]
    CountryNumber : bool

    [<field : DataMember(Name="TLD")>]
    TLD : bool
}

[<DataContract;NoEquality;NoComparison>]
type PrintOptionsSettings = {
    [<field : DataMember(Name="CsvDateFormat")>]
    CsvDateFormat : string

    [<field : DataMember(Name="CsvSetGenderManually")>]
    CsvSetGenderManually : bool
    
    [<field : DataMember(Name="CsvGenderMale")>]
    CsvGenderMale : string

    [<field : DataMember(Name="CsvGenderFemale")>]
    CsvGenderFemale : string

    [<field : DataMember(Name="JsonDateType")>]
    JsonDateType : string

    [<field : DataMember(Name="JsonPrettyPrint")>]
    JsonPrettyPrint : bool

    [<field : DataMember(Name="XmlPrettyPrint")>]
    XmlPrettyPrint : bool 
}

[<DataContract;NoEquality;NoComparison>]
type TemplatePrintSettings = {
    [<field : DataMember(Name="Output")>]
    Output : string
}

[<DataContract;NoEquality;NoComparison>]
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

[<DataContract;NoEquality;NoComparison>]
type RandomnessOptionsSettings = {
    [<field : DataMember(Name="ManualSeed")>]
    ManualSeed : Nullable<bool>

    [<field : DataMember(Name="Seed")>]
    Seed : Nullable<int>
}

[<DataContract;NoEquality;NoComparison>]
type GenericOptionsSettings = {
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
    Randomness : RandomnessOptionsSettings
}

[<DataContract;NoEquality;NoComparison>]
type InteractiveModeSettings = {
    [<field : DataMember(Name="Options")>]
    Options : GenericOptionsSettings

    [<field : DataMember(Name="ConsolePrint")>]
    ConsolePrint : GenericPrintSettings
}

[<DataContract;NoEquality;NoComparison>]
type ListModeSettings = {
    [<field : DataMember(Name="Options")>]
    Options : GenericOptionsSettings

    [<field : DataMember(Name="PrintOptions")>]
    PrintOptions : PrintOptionsSettings

    [<field : DataMember(Name="ConsolePrint")>]
    ConsolePrint : GenericPrintSettings

    [<field : DataMember(Name="FilePrint")>]
    FilePrint : GenericPrintSettings
}

[<DataContract;NoEquality;NoComparison>]
type TemplateModeSettings = {
    [<field : DataMember(Name="Options")>]
    Options : GenericOptionsSettings

    [<field : DataMember(Name="Print")>]
    Print : TemplatePrintSettings
}

[<DataContract;NoEquality;NoComparison>]
type Settings = {
    [<field : DataMember(Name="InteractiveMode")>]
    InteractiveMode: InteractiveModeSettings

    [<field : DataMember(Name="ListMode")>]
    ListMode: ListModeSettings

    [<field : DataMember(Name="TemplateMode")>]
    TemplateMode: TemplateModeSettings
}

let genericOptionsToRandomPersonOptions (genericOptions: GenericOptionsSettings) =
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
