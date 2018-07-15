module internal PrintCsv

open System.IO
open System.Text
open RandomPersonLib
open Settings

let printToCsv (people: Person[]) (outputFilePath: string) (printSettings: genericPrintSettings) =
    let personToString (person: Person)  =
        let sb = StringBuilder()
        if printSettings.FirstName   then sb.Append(person.FirstName + ",")                     |> ignore
        if printSettings.LastName    then sb.Append(person.LastName + ",")                      |> ignore
        if printSettings.Address1    then sb.Append(person.Address1 + ",")                      |> ignore
        if printSettings.Address2    then sb.Append(person.Address2 + ",")                      |> ignore
        if printSettings.PostalCode  then sb.Append(person.PostalCode + ",")                    |> ignore
        if printSettings.City        then sb.Append(person.City + ",")                          |> ignore
        if printSettings.BirthDate   then sb.Append(person.BirthDate.ToShortDateString() + ",") |> ignore
        if printSettings.Gender      then sb.Append(person.Gender.ToString() + ",")             |> ignore
        if printSettings.SSN         then sb.Append(person.SSN + ",")                           |> ignore
        if printSettings.Nationality then sb.Append(person.Nationality.ToString() + ",")        |> ignore
        if printSettings.Email       then sb.Append(person.Email + ",")                         |> ignore
        if printSettings.Password    then sb.Append(person.Password + ",")                      |> ignore
        if printSettings.MobilePhone then sb.Append(person.MobilePhone + ",")                   |> ignore
        if printSettings.HomePhone   then sb.Append(person.HomePhone + ",")                     |> ignore

        sb.Remove(sb.Length - 1, 1) |> ignore

        sb.ToString ()
    
    let createHeader () =
        let sb = StringBuilder()
        if printSettings.FirstName   then sb.Append("FirstName,")   |> ignore
        if printSettings.LastName    then sb.Append("LastName,")    |> ignore
        if printSettings.Address1    then sb.Append("Address1,")    |> ignore
        if printSettings.Address2    then sb.Append("Address2,")    |> ignore
        if printSettings.PostalCode  then sb.Append("PostalCode,")  |> ignore
        if printSettings.City        then sb.Append("City,")        |> ignore
        if printSettings.BirthDate   then sb.Append("BirthDate,")   |> ignore
        if printSettings.Gender      then sb.Append("Gender,")      |> ignore
        if printSettings.SSN         then sb.Append("SSN,")         |> ignore
        if printSettings.Nationality then sb.Append("Nationality,") |> ignore
        if printSettings.Email       then sb.Append("Email,")       |> ignore
        if printSettings.Password    then sb.Append("Password,")    |> ignore
        if printSettings.MobilePhone then sb.Append("MobilePhone,") |> ignore
        if printSettings.HomePhone   then sb.Append("HomePhone,")   |> ignore

        sb.Remove(sb.Length - 1, 1) |> ignore

        let header = sb.ToString ()
        [| header |]

    let header = createHeader ()
    let printableWithoutHeader = people |> Array.map personToString
    let printableWithHeader = Array.concat [ header; printableWithoutHeader ]
    let filenameWithFixedFileEnding = outputFilePath.Replace("?", "csv")

    File.WriteAllLines(filenameWithFixedFileEnding, printableWithHeader)
