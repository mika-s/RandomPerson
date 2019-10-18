module internal PrintToConsole

open RandomPersonLib
open CliUtil
open Settings
open ReadInputFiles

let isPrintingMoreThanOneLine (print: GenericPrintSettings) =
    b2i print.FirstName
    + b2i print.LastName
    + b2i print.SSN
    + b2i print.Country
    + b2i print.BirthDate
    + b2i print.Gender
    + b2i print.Address1
    + b2i print.Address2
    + b2i print.PostalCode
    + b2i print.City
    + b2i print.State
    + b2i print.Email
    + b2i print.Password
    + b2i print.MacAddress
    + b2i print.MobilePhone
    + b2i print.HomePhone
    + b2i print.PIN
    + b2i print.PAN
    + b2i print.Expiry
    + b2i print.CVV
    + b2i print.CountryNameEnglish
    + b2i print.CountryNameNative
    + b2i print.CountryNameNativeAlternative1
    + b2i print.CountryNameNativeAlternative2
    + b2i print.CountryCode2
    + b2i print.CountryCode3
    + b2i print.CountryNumber
    + b2i print.TLD
        > 1

let printToScreen (isPrinting: bool) (isLabel: bool) (label: string) (output: string) =
    match isPrinting, isLabel with
    | (true, true)  ->
        match label.Length < 7 with
        | true  -> printfn "%s:\t\t%s" label output |> ignore
        | false -> printfn "%s:\t%s"   label output |> ignore
    | (true, false) -> printfn "%s" output          |> ignore
    | _             -> ()                           |> ignore

let printPerson (print: GenericPrintSettings) (person: Person) =
    printToScreen print.FirstName                      print.Label "First name"    person.FirstName
    printToScreen print.LastName                       print.Label "Last name"     person.LastName
    printToScreen print.SSN                            print.Label "SSN"           person.SSN
    printToScreen print.Country                        print.Label "Country"      (person.Country.ToString())
    printToScreen print.BirthDate                      print.Label "Birthdate"    (person.BirthDate.ToString("yyyy-MM-dd"))
    printToScreen print.Gender                         print.Label "Gender"       (person.Gender.ToString())
    printToScreen print.Address1                       print.Label "Address 1"     person.Address1
    printToScreen print.Address2                       print.Label "Address 2"     person.Address2
    printToScreen print.PostalCode                     print.Label "Postal code"   person.PostalCode
    printToScreen print.City                           print.Label "City"          person.City
    printToScreen print.State                          print.Label "State"         person.State
    printToScreen print.Email                          print.Label "Email"         person.Email
    printToScreen print.Password                       print.Label "Password"      person.Password
    printToScreen print.MacAddress                     print.Label "MAC address"   person.MacAddress
    printToScreen print.MobilePhone                    print.Label "Mobile phone"  person.MobilePhone
    printToScreen print.HomePhone                      print.Label "Home phone"    person.HomePhone
    printToScreen print.PIN                            print.Label "PIN"           person.PIN
    printToScreen print.PAN                            print.Label "PAN"           person.PAN
    printToScreen print.Expiry                         print.Label "Expiry"        person.Expiry
    printToScreen print.CVV                            print.Label "CVV"           person.CVV
    printToScreen print.CountryNameEnglish             print.Label "Country"       person.CountryNameEnglish
    printToScreen print.CountryNameNative              print.Label "Country"       person.CountryNameNative
    printToScreen print.CountryNameNativeAlternative1  print.Label "Country"       person.CountryNameNativeAlternative1
    printToScreen print.CountryNameNativeAlternative2  print.Label "Country"       person.CountryNameNativeAlternative2
    printToScreen print.CountryCode2                   print.Label "CountryCode2"  person.CountryCode2
    printToScreen print.CountryCode3                   print.Label "CountryCode3"  person.CountryCode3
    printToScreen print.CountryNumber                  print.Label "CountryNumber" person.CountryNumber
    printToScreen print.TLD                            print.Label "TLD"           person.TLD

    if isPrintingMoreThanOneLine (print) then printfn ""

let printToConsole (i: InputFiles) (people: Person list) =
    people |> List.iter (fun (person: Person) -> printPerson i.settings.ListMode.ConsolePrint person)
