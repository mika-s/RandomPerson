module internal PrintJson

open System
open Newtonsoft.Json
open Newtonsoft.Json.Converters
open RandomPersonLib
open Settings
open CliUtil

type PersonSerializable = {
    [<JsonProperty("FirstName")>]
    FirstName : string

    [<JsonProperty("LastName")>]
    LastName : string

    [<JsonProperty("SSN")>]
    SSN : string

    [<JsonProperty("Nationality")>]
    [<JsonConverter(typedefof<StringEnumConverter>)>]
    Nationality: Nationality

    [<JsonProperty("Address1")>]
    Address1 : string

    [<JsonProperty("Address2")>]
    Address2 : string

    [<JsonProperty("PostalCode")>]
    PostalCode : string

    [<JsonProperty("City")>]
    City : string

    [<JsonProperty("BirthDate")>]
    BirthDate : DateTime

    [<JsonProperty("Gender")>]
    [<JsonConverter(typedefof<StringEnumConverter>)>]
    Gender : Gender

    [<JsonProperty("Email")>]
    Email : string

    [<JsonProperty("MobilePhone")>]
    MobilePhone : string

    [<JsonProperty("HomePhone")>]
    HomePhone : string
}

let createPersonSerializable (person: Person) =
    {
        FirstName = person.FirstName;
        LastName = person.LastName;
        SSN = person.SSN;
        Nationality = person.Nationality;
        Address1 = person.Address1;
        Address2 = person.Address2;
        PostalCode = person.PostalCode;
        City = person.City;
        BirthDate = person.BirthDate;
        Gender = person.Gender;
        Email = person.Email;
        MobilePhone = person.MobilePhone;
        HomePhone = person.HomePhone;
    }

let createJsonSerializerSettings (jsonPrintType: string) (isFormatted: bool) =
    match jsonPrintType, isFormatted with
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
    | _                  -> failwith "Illegal JSON print type."

let printToJson (people: Person[]) (outputFilePath: string) (settings: listModeSettings)  =
    let filenameWithFixedFileEnding = outputFilePath.Replace("?", "json")
    let jsonPrintSettings = createJsonSerializerSettings settings.PrintOptions.JsonPrintType settings.PrintOptions.JsonPrettyPrint
    let toSerialize = people |> Array.map(createPersonSerializable)

    writeDataToJsonFile<PersonSerializable[]> filenameWithFixedFileEnding toSerialize jsonPrintSettings
