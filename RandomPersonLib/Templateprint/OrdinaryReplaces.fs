module internal OrdinaryReplaces

open System
open RandomPersonLib
open StringUtil

let ordinaryReplacer (valueBoxed: obj) =
    match valueBoxed.GetType() with
    | x when x = typedefof<string>   -> string (unbox valueBoxed)
    | x when x = typedefof<Gender>   -> (valueBoxed :?> Gender).ToString()
    | x when x = typedefof<Country>  -> (valueBoxed :?> Country).ToString()
    | x when x = typedefof<DateTime> -> (valueBoxed :?> DateTime).ToString("yyyy-MM-dd")
    | _ -> invalidOp "Error in ordinaryReplacer"

let toLowerReplacer      (valueBoxed: obj) = valueBoxed |> ordinaryReplacer |> lowercase
let toUpperReplacer      (valueBoxed: obj) = valueBoxed |> ordinaryReplacer |> uppercase
let firstUppercaseRestLowercaseReplacer (valueBoxed: obj) =
    valueBoxed |> ordinaryReplacer |> firstUppercaseRestLowercase
let capitalizeReplacer   (valueBoxed: obj) = valueBoxed |> ordinaryReplacer |> capitalize
let uncapitalizeReplacer (valueBoxed: obj) = valueBoxed |> ordinaryReplacer |> uncapitalize

let replacer (mapping: (string * obj) list) (replaceFunc: obj -> string) (strFormat: string) (str: string) =
    let folder (acc: string) (y: string * obj) =
        let valueBoxed = snd y
        let value = replaceFunc valueBoxed

        acc.Replace(String.Format(strFormat, fst y), value)

    (str, mapping) ||> List.fold folder 

let performOrdinaryReplaces (person: Person) (originalOutput: string) =
    let mapping = 
        [
            "SSN", box person.SSN;
            "Email", box person.Email;
            "Password", box person.Password;
            "MacAddress", box person.MacAddress;
            "FirstName", box person.FirstName;
            "LastName", box person.LastName;
            "Address1", box person.Address1;
            "Address2", box person.Address2;
            "PostalCode", box person.PostalCode;
            "City", box person.City;
            "Country", box person.Country;
            "BirthDate", box person.BirthDate;
            "Gender", box person.Gender;
            "MobilePhone", box person.MobilePhone;
            "HomePhone", box person.HomePhone;
            "PIN", box person.PIN;
            "PAN", box person.PAN;
            "Expiry", box person.Expiry;
            "CVV", box person.CVV;
            "CountryNameEnglish", box person.CountryNameEnglish;
            "CountryNameNative", box person.CountryNameNative;
            "CountryNameNativeAlt1", box person.CountryNameNativeAlternative1;
            "CountryNameNativeAlt2", box person.CountryNameNativeAlternative2;
            "CountryCode2", box person.CountryCode2;
            "CountryCode3", box person.CountryCode3;
            "CountryNumber", box person.CountryNumber;
            "TLD", box person.TLD;
        ]

    originalOutput
    |> replacer mapping ordinaryReplacer                    "#{{{0}}}"
    |> replacer mapping toLowerReplacer                     "#{{{0}.ToLower()}}"
    |> replacer mapping toUpperReplacer                     "#{{{0}.ToUpper()}}"
    |> replacer mapping firstUppercaseRestLowercaseReplacer "#{{{0}.FirstToUpperRestLower()}}"
    |> replacer mapping capitalizeReplacer                  "#{{{0}.Capitalize()}}"
    |> replacer mapping uncapitalizeReplacer                "#{{{0}.Uncapitalize()}}"
