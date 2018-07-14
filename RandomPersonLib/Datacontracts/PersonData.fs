module PersonData

open System.Runtime.Serialization

[<DataContract>]
type MobileData = {
        [<field : DataMember(Name="Patterns")>]
        Patterns: string[]
    }

[<DataContract>]
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

[<DataContract>]
type PersonData = {
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
}
