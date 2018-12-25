module internal PrintCsv

open System.IO
open System.Text
open Microsoft.FSharp.Core.Printf
open RandomPersonLib
open Settings

let personToString (printSettings: GenericPrintSettings) (printOptions: PrintOptionsSettings) (person: Person) =
    let birthDate = person.BirthDate.ToString(printOptions.CsvDateFormat)

    let gender = match printOptions.CsvSetGenderManually with
                 | true  ->
                     match person.Gender with
                     | Gender.Male   -> printOptions.CsvGenderMale
                     | Gender.Female -> printOptions.CsvGenderFemale
                     | _ -> invalidArg "gender" "Illegal gender."
                 | false -> person.Gender.ToString()
    
    let sb = StringBuilder()
    if printSettings.FirstName                     then bprintf sb "%s" (person.FirstName + ",")                     |> ignore
    if printSettings.LastName                      then bprintf sb "%s" (person.LastName + ",")                      |> ignore
    if printSettings.Address1                      then bprintf sb "%s" (person.Address1 + ",")                      |> ignore
    if printSettings.Address2                      then bprintf sb "%s" (person.Address2 + ",")                      |> ignore
    if printSettings.PostalCode                    then bprintf sb "%s" (person.PostalCode + ",")                    |> ignore
    if printSettings.City                          then bprintf sb "%s" (person.City + ",")                          |> ignore
    if printSettings.State                         then bprintf sb "%s" (person.State + ",")                         |> ignore
    if printSettings.BirthDate                     then bprintf sb "%s" (birthDate + ",")                            |> ignore
    if printSettings.Gender                        then bprintf sb "%s" (gender + ",")                               |> ignore
    if printSettings.SSN                           then bprintf sb "%s" (person.SSN + ",")                           |> ignore
    if printSettings.Country                       then bprintf sb "%s" (person.Country.ToString() + ",")            |> ignore
    if printSettings.Email                         then bprintf sb "%s" (person.Email + ",")                         |> ignore
    if printSettings.Password                      then bprintf sb "%s" (person.Password + ",")                      |> ignore
    if printSettings.MobilePhone                   then bprintf sb "%s" (person.MobilePhone + ",")                   |> ignore
    if printSettings.HomePhone                     then bprintf sb "%s" (person.HomePhone + ",")                     |> ignore
    if printSettings.CountryNameEnglish            then bprintf sb "%s" (person.CountryNameEnglish + ",")            |> ignore
    if printSettings.CountryNameNative             then bprintf sb "%s" (person.CountryNameNative + ",")             |> ignore
    if printSettings.CountryNameNativeAlternative1 then bprintf sb "%s" (person.CountryNameNativeAlternative1 + ",") |> ignore
    if printSettings.CountryNameNativeAlternative2 then bprintf sb "%s" (person.CountryNameNativeAlternative2 + ",") |> ignore
    if printSettings.CountryCode2                  then bprintf sb "%s" (person.CountryCode2 + ",")                  |> ignore
    if printSettings.CountryCode3                  then bprintf sb "%s" (person.CountryCode3 + ",")                  |> ignore
    if printSettings.CountryNumber                 then bprintf sb "%s" (person.CountryNumber + ",")                 |> ignore
    if printSettings.TLD                           then bprintf sb "%s" (person.TLD + ",")                           |> ignore

    sb.Remove(sb.Length - 1, 1) |> ignore

    sb.ToString()

let createHeader (printSettings: GenericPrintSettings) =
    let sb = StringBuilder()
    if printSettings.FirstName                     then bprintf sb "%s" ("FirstName,")                     |> ignore
    if printSettings.LastName                      then bprintf sb "%s" ("LastName,")                      |> ignore
    if printSettings.Address1                      then bprintf sb "%s" ("Address1,")                      |> ignore
    if printSettings.Address2                      then bprintf sb "%s" ("Address2,")                      |> ignore
    if printSettings.PostalCode                    then bprintf sb "%s" ("PostalCode,")                    |> ignore
    if printSettings.City                          then bprintf sb "%s" ("City,")                          |> ignore
    if printSettings.State                         then bprintf sb "%s" ("State,")                         |> ignore
    if printSettings.BirthDate                     then bprintf sb "%s" ("BirthDate,")                     |> ignore
    if printSettings.Gender                        then bprintf sb "%s" ("Gender,")                        |> ignore
    if printSettings.SSN                           then bprintf sb "%s" ("SSN,")                           |> ignore
    if printSettings.Country                       then bprintf sb "%s" ("Country,")                       |> ignore
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
    if printSettings.TLD                           then bprintf sb "%s" ("TLD,")                           |> ignore

    match sb.Length with
    | length when 1 < length -> sb.Remove(sb.Length - 1, 1) |> ignore
    | _                      -> StringBuilder()             |> ignore
    
    let header = sb.ToString ()
    [| header |]

let printToCsv (people: Person[]) (outputFilePath: string) (printSettings: GenericPrintSettings) (printOptions: PrintOptionsSettings) =
    let header = createHeader printSettings
    let printableWithoutHeader = people |> Array.map(personToString printSettings printOptions)
    let printableWithHeader = Array.concat [ header; printableWithoutHeader ]
    let filenameWithFixedFileEnding = outputFilePath.Replace("?", "csv")

    File.WriteAllLines(filenameWithFixedFileEnding, printableWithHeader)
