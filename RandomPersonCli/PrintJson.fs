module internal PrintJson

open System
open Newtonsoft.Json
open Newtonsoft.Json.Converters
open RandomPersonLib
open CliUtil
open Settings

[<NoEquality;NoComparison>]
type PersonSerializable = {
    [<JsonProperty("FirstName")>]
    FirstName : string

    [<JsonProperty("LastName")>]
    LastName : string

    [<JsonProperty("SSN")>]
    SSN : string

    [<JsonProperty("Country");JsonConverter(typedefof<StringEnumConverter>)>]
    Country: Country

    [<JsonProperty("Address1")>]
    Address1 : string

    [<JsonProperty("Address2")>]
    Address2 : string

    [<JsonProperty("PostalCode")>]
    PostalCode : string

    [<JsonProperty("City")>]
    City : string

    [<JsonProperty("State")>]
    State : string

    [<JsonProperty("BirthDate")>]
    BirthDate : DateTime

    [<JsonProperty("Gender");JsonConverter(typedefof<StringEnumConverter>)>]
    Gender : Gender

    [<JsonProperty("Email")>]
    Email : string

    [<JsonProperty("Password")>]
    Password : string

    [<JsonProperty("MobilePhone")>]
    MobilePhone : string

    [<JsonProperty("HomePhone")>]
    HomePhone : string

    [<JsonProperty("PIN")>]
    PIN : string

    [<JsonProperty("PAN")>]
    PAN : string

    [<JsonProperty("Expiry")>]
    Expiry : string

    [<JsonProperty("CVV")>]
    CVV : string

    [<JsonProperty("CountryNameEnglish")>]
    CountryNameEnglish : string

    [<JsonProperty("CountryNameNative")>]
    CountryNameNative : string

    [<JsonProperty("CountryNameNativeAlternative1")>]
    CountryNameNativeAlternative1 : string

    [<JsonProperty("CountryNameNativeAlternative2")>]
    CountryNameNativeAlternative2 : string

    [<JsonProperty("CountryCode2")>]
    CountryCode2 : string

    [<JsonProperty("CountryCode3")>]
    CountryCode3 : string

    [<JsonProperty("CountryNumber")>]
    CountryNumber : string

    [<JsonProperty("TLD")>]
    TLD : string
}

let createPersonSerializable (person: Person) =
    {
        FirstName = person.FirstName;
        LastName = person.LastName;
        SSN = person.SSN;
        Country = person.Country;
        Address1 = person.Address1;
        Address2 = person.Address2;
        PostalCode = person.PostalCode;
        City = person.City;
        State = person.State;
        BirthDate = person.BirthDate;
        Gender = person.Gender;
        Email = person.Email;
        Password = person.Password;
        MobilePhone = person.MobilePhone;
        HomePhone = person.HomePhone;
        PIN = person.PIN;
        PAN = person.PAN;
        Expiry = person.Expiry;
        CVV = person.CVV;
        CountryNameEnglish = person.CountryNameEnglish;
        CountryNameNative = person.CountryNameNative;
        CountryNameNativeAlternative1 = person.CountryNameNativeAlternative1;
        CountryNameNativeAlternative2 = person.CountryNameNativeAlternative2;
        CountryCode2 = person.CountryCode2;
        CountryCode3 = person.CountryCode3;
        CountryNumber = person.CountryNumber;
        TLD = person.TLD;
    }

let createJsonSerializerSettings (jsonDateTimeType: string) (isFormatted: bool) =
    match jsonDateTimeType, isFormatted with
    | "Microsoft", true  ->
       JsonSerializerSettings (Formatting = Formatting.Indented,
                               DateFormatHandling = DateFormatHandling.MicrosoftDateFormat)
    | "Microsoft", false ->
       JsonSerializerSettings (DateFormatHandling = DateFormatHandling.MicrosoftDateFormat)
    | "ISO", true        ->
       JsonSerializerSettings (Formatting = Formatting.Indented,
                               DateFormatHandling = DateFormatHandling.IsoDateFormat)
    | "ISO", false       ->
       JsonSerializerSettings (DateFormatHandling = DateFormatHandling.IsoDateFormat)
    | _                  -> invalidArg "jsonPrintType, isFormatted" "Illegal JSON print type, isFormatted pair."

let printToJson (people: Person[]) (outputFilePath: string) (settings: ListModeSettings)  =
    let filenameWithFixedFileEnding = outputFilePath.Replace("?", "json")
    let jsonPrintSettings = createJsonSerializerSettings settings.PrintOptions.JsonDateTimeType settings.PrintOptions.JsonPrettyPrint
    
    people
    |> Array.map(createPersonSerializable)
    |> writeToJsonFile<PersonSerializable[]> filenameWithFixedFileEnding jsonPrintSettings
