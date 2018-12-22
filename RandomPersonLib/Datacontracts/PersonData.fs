module PersonData

open System.Runtime.Serialization

[<DataContract;NoEquality;NoComparison>]
type MiscData = {
    [<field : DataMember(Name="CountryNameEnglish")>]
    CountryNameEnglish : string

    [<field : DataMember(Name="CountryNameNative")>]
    CountryNameNative : string

    [<field : DataMember(Name="CountryNameNativeAlternative1")>]
    CountryNameNativeAlternative1 : string

    [<field : DataMember(Name="CountryNameNativeAlternative2")>]
    CountryNameNativeAlternative2 : string

    [<field : DataMember(Name="CountryCode2")>]
    CountryCode2 : string

    [<field : DataMember(Name="CountryCode3")>]
    CountryCode3 : string

    [<field : DataMember(Name="CountryNumber")>]
    CountryNumber : string

    [<field : DataMember(Name="TLD")>]
    TLD : string
}

[<DataContract;NoEquality;NoComparison>]
type MobileData = {
    [<field : DataMember(Name="Patterns")>]
    Patterns: string[]
}

[<DataContract;NoEquality;NoComparison>]
type PhoneData = {
    [<field : DataMember(Name="CountryCode")>]
    CountryCode : int

    [<field : DataMember(Name="TrunkPrefix")>]
    TrunkPrefix : string

    [<field : DataMember(Name="Mobile")>]
    Mobile : MobileData

    [<field : DataMember(Name="Home")>]
    Home : MobileData
}

[<DataContract;NoEquality;NoComparison>]
type PersonData = {
    [<field : DataMember(Name="Misc")>]
    Misc : MiscData

    [<field : DataMember(Name="Phone")>]
    Phone : PhoneData

    [<field : DataMember(Name="EmailEndings")>]
    EmailEndings : string[]

    [<field : DataMember(Name="MaleFirstNames")>]
    MaleFirstNames : string[]

    [<field : DataMember(Name="FemaleFirstNames")>]
    FemaleFirstNames: string[]

    [<field : DataMember(Name="LastNames")>]
    LastNames: string[]

    [<field : DataMember(Name="MaleLastNames")>]
    MaleLastNames: string[]

    [<field : DataMember(Name="FemaleLastNames")>]
    FemaleLastNames: string[]
}
