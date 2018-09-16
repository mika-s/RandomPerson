module internal PrintCsv

open System.IO
open System.Text
open Microsoft.FSharp.Core.Printf
open RandomPersonLib
open Settings

let personToString (printSettings: genericPrintSettings) (person: Person) =
    let sb = StringBuilder()
    if printSettings.FirstName                     then bprintf sb "%s" (person.FirstName + ",")                        |> ignore
    if printSettings.LastName                      then bprintf sb "%s" (person.LastName + ",")                         |> ignore
    if printSettings.Address1                      then bprintf sb "%s" (person.Address1 + ",")                         |> ignore
    if printSettings.Address2                      then bprintf sb "%s" (person.Address2 + ",")                         |> ignore
    if printSettings.PostalCode                    then bprintf sb "%s" (person.PostalCode + ",")                       |> ignore
    if printSettings.City                          then bprintf sb "%s" (person.City + ",")                             |> ignore
    if printSettings.BirthDate                     then bprintf sb "%s" (person.BirthDate.ToString("yyyy-MM-dd") + ",") |> ignore
    if printSettings.Gender                        then bprintf sb "%s" (person.Gender.ToString() + ",")                |> ignore
    if printSettings.SSN                           then bprintf sb "%s" (person.SSN + ",")                              |> ignore
    if printSettings.Nationality                   then bprintf sb "%s" (person.Nationality.ToString() + ",")           |> ignore
    if printSettings.Email                         then bprintf sb "%s" (person.Email + ",")                            |> ignore
    if printSettings.Password                      then bprintf sb "%s" (person.Password + ",")                         |> ignore
    if printSettings.MobilePhone                   then bprintf sb "%s" (person.MobilePhone + ",")                      |> ignore
    if printSettings.HomePhone                     then bprintf sb "%s" (person.HomePhone + ",")                        |> ignore
    if printSettings.CountryNameEnglish            then bprintf sb "%s" (person.CountryNameEnglish + ",")               |> ignore
    if printSettings.CountryNameNative             then bprintf sb "%s" (person.CountryNameNative + ",")                |> ignore
    if printSettings.CountryNameNativeAlternative1 then bprintf sb "%s" (person.CountryNameNativeAlternative1 + ",")    |> ignore
    if printSettings.CountryNameNativeAlternative2 then bprintf sb "%s" (person.CountryNameNativeAlternative2 + ",")    |> ignore
    if printSettings.CountryCode2                  then bprintf sb "%s" (person.CountryCode2 + ",")                     |> ignore
    if printSettings.CountryCode3                  then bprintf sb "%s" (person.CountryCode3 + ",")                     |> ignore
    if printSettings.CountryNumber                 then bprintf sb "%s" (person.CountryNumber + ",")                    |> ignore

    sb.Remove(sb.Length - 1, 1) |> ignore

    sb.ToString ()

let createHeader (printSettings: genericPrintSettings) =
    let sb = StringBuilder()
    if printSettings.FirstName                     then bprintf sb "%s" ("FirstName,")                     |> ignore
    if printSettings.LastName                      then bprintf sb "%s" ("LastName,")                      |> ignore
    if printSettings.Address1                      then bprintf sb "%s" ("Address1,")                      |> ignore
    if printSettings.Address2                      then bprintf sb "%s" ("Address2,")                      |> ignore
    if printSettings.PostalCode                    then bprintf sb "%s" ("PostalCode,")                    |> ignore
    if printSettings.City                          then bprintf sb "%s" ("City,")                          |> ignore
    if printSettings.BirthDate                     then bprintf sb "%s" ("BirthDate,")                     |> ignore
    if printSettings.Gender                        then bprintf sb "%s" ("Gender,")                        |> ignore
    if printSettings.SSN                           then bprintf sb "%s" ("SSN,")                           |> ignore
    if printSettings.Nationality                   then bprintf sb "%s" ("Nationality,")                   |> ignore
    if printSettings.Email                         then bprintf sb "%s" ("Email,")                         |> ignore
    if printSettings.Password                      then bprintf sb "%s" ("Password,")                      |> ignore
    if printSettings.MobilePhone                   then bprintf sb "%s" ("MobilePhone,")                   |> ignore
    if printSettings.HomePhone                     then bprintf sb "%s" ("HomePhone,")                     |> ignore
    if printSettings.CountryNameEnglish            then bprintf sb "%s" ("CountryNameEnglish,")            |> ignore
    if printSettings.CountryNameNative             then bprintf sb "%s" ("CountryNameNative,")             |> ignore
    if printSettings.CountryNameNativeAlternative1 then bprintf sb "%s" ("CountryNameNativeAlternative1,") |> ignore
    if printSettings.CountryNameNativeAlternative2 then bprintf sb "%s" ("CountryNameNativeAlternative2,") |> ignore
    if printSettings.CountryCode2                  then bprintf sb "%s" ("CountryCode2,")                  |> ignore
    if printSettings.CountryCode3                  then bprintf sb "%s" ("CountryCode3,")                  |> ignore
    if printSettings.CountryNumber                 then bprintf sb "%s" ("CountryNumber,")                 |> ignore

    match sb.Length with
    | length when 1 < length -> sb.Remove(sb.Length - 1, 1) |> ignore
    | _                      -> StringBuilder()             |> ignore
    
    let header = sb.ToString ()
    [| header |]

let printToCsv (people: Person[]) (outputFilePath: string) (printSettings: genericPrintSettings) =
    let header = createHeader printSettings
    let printableWithoutHeader = people |> Array.map(personToString printSettings)
    let printableWithHeader = Array.concat [ header; printableWithoutHeader ]
    let filenameWithFixedFileEnding = outputFilePath.Replace("?", "csv")

    File.WriteAllLines(filenameWithFixedFileEnding, printableWithHeader)
