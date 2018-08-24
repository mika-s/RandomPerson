module OrdinaryReplaces

open System
open RandomPersonLib
open Util

let ordinaryReplacer (valueBoxed: obj) =
    match valueBoxed.GetType() with
    | x when x = typedefof<string>      -> string (unbox valueBoxed)
    | x when x = typedefof<Gender>      -> (valueBoxed :?> Gender).ToString()
    | x when x = typedefof<Nationality> -> (valueBoxed :?> Nationality).ToString()
    | x when x = typedefof<DateTime>    -> (valueBoxed :?> DateTime).ToString("yyyy-MM-dd")
    | _ -> invalidOp "Error in ordinaryReplacer"

let toLowerReplacer    (valueBoxed: obj) = valueBoxed |> ordinaryReplacer |> lowercase
let toUpperReplacer    (valueBoxed: obj) = valueBoxed |> ordinaryReplacer |> uppercase
let firstUpperReplacer (valueBoxed: obj) = valueBoxed |> ordinaryReplacer |> firstUpper

let replacer (mapping: (string * obj) list) (replaceFunc: obj -> string) (strFormat: string) (str: string) =
    let folder (acc: string) (y: string * obj) =
        let valueBoxed = snd y
        let value = replaceFunc valueBoxed

        acc.Replace(String.Format(strFormat, fst y), value)

    List.fold folder str mapping

let performOrdinaryReplaces (person: Person) (originalOutput: string) =
    let mapping = 
        [
            "SSN", box person.SSN;
            "Email", box person.Email;
            "Password", box person.Password;
            "FirstName", box person.FirstName;
            "LastName", box person.LastName;
            "Address1", box person.Address1;
            "Address2", box person.Address2;
            "PostalCode", box person.PostalCode;
            "City", box person.City;
            "Nationality", box person.Nationality;
            "BirthDate", box person.BirthDate;
            "Gender", box person.Gender;
            "MobilePhone", box person.MobilePhone;
            "HomePhone", box person.HomePhone;
            "CountryNameEnglish", box person.CountryNameEnglish;
            "CountryNameNative", box person.CountryNameNative;
            "CountryNameNativeAlt1", box person.CountryNameNativeAlternative1;
            "CountryNameNativeAlt2", box person.CountryNameNativeAlternative2;
        ]

    originalOutput
    |> replacer mapping ordinaryReplacer   "#{{{0}}}"
    |> replacer mapping toLowerReplacer    "#{{{0}.ToLower()}}"
    |> replacer mapping toUpperReplacer    "#{{{0}.ToUpper()}}"
    |> replacer mapping firstUpperReplacer "#{{{0}.FirstUpper()}}"
