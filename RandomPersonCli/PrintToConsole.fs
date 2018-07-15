module internal PrintToConsole

open RandomPersonLib
open CliUtil
open Settings
open ReadInputFiles

let isPrintingMoreThanOneLine (print: genericPrintSettings) =
    b2i print.FirstName
    + b2i print.LastName
    + b2i print.SSN
    + b2i print.Nationality
    + b2i print.BirthDate
    + b2i print.Gender
    + b2i print.Address1
    + b2i print.Address2
    + b2i print.PostalCode
    + b2i print.City
    + b2i print.Email
    + b2i print.Password
    + b2i print.MobilePhone
    + b2i print.HomePhone
        > 1

let printToScreen (isPrinting: bool) (isLabel: bool) (label: string) (output: string) =
    match isPrinting, isLabel with
    | (true, true)  -> printfn "%s:\t%s" label output |> ignore
    | (true, false) -> printfn "%s" output            |> ignore
    | _             -> ()                             |> ignore

let printPerson (print: genericPrintSettings) (person: Person) =
    printToScreen print.FirstName   print.Label "First name"   person.FirstName
    printToScreen print.LastName    print.Label "Last name"    person.LastName
    printToScreen print.SSN         print.Label "SSN"          person.SSN
    printToScreen print.Nationality print.Label "Nationality" (person.Nationality.ToString())
    printToScreen print.BirthDate   print.Label "Birthdate"   (person.BirthDate.ToShortDateString())
    printToScreen print.Gender      print.Label "Gender"      (person.Gender.ToString())
    printToScreen print.Address1    print.Label "Address 1"    person.Address1
    printToScreen print.Address2    print.Label "Address 2"    person.Address2
    printToScreen print.PostalCode  print.Label "Postal code"  person.PostalCode
    printToScreen print.City        print.Label "City"         person.City
    printToScreen print.Email       print.Label "Email"        person.Email
    printToScreen print.Password    print.Label "Password"     person.Password
    printToScreen print.MobilePhone print.Label "Mobile phone" person.MobilePhone
    printToScreen print.HomePhone   print.Label "Home phone"   person.HomePhone

    if isPrintingMoreThanOneLine (print) then printfn ""

let printToConsole (i: inputFiles) (people: Person list) =
    people |> List.iter (fun (person: Person) -> printPerson i.settings.ListMode.ConsolePrint person)
