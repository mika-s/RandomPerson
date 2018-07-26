module OrdinaryReplaces

open System
open RandomPersonLib

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
        ]

    let replaceOrdinary (str: string) =
        let ordinaryFolder (acc: string) (y: string * obj) =
            let valueBoxed = snd y
            let value = match valueBoxed.GetType() with
                        | x when x = typedefof<string>      -> string (unbox valueBoxed)
                        | x when x = typedefof<Gender>      -> (valueBoxed :?> Gender).ToString()
                        | x when x = typedefof<Nationality> -> (valueBoxed :?> Nationality).ToString()
                        | x when x = typedefof<DateTime>    -> (valueBoxed :?> DateTime).ToString()
                        | _ -> invalidOp "Error in ordinaryFolder"

            acc.Replace(String.Format("#{{{0}}}", fst y), value)

        List.fold ordinaryFolder str mapping

    let replaceToLower (str: string) =
        let toLowerFolder (acc: string) (y: string * obj) =
            let valueBoxed = snd y
            let value = match valueBoxed.GetType() with
                        | x when x = typedefof<string>      -> (string (unbox valueBoxed)).ToLower()
                        | x when x = typedefof<Gender>      -> (valueBoxed :?> Gender).ToString().ToLower()
                        | x when x = typedefof<Nationality> -> (valueBoxed :?> Nationality).ToString().ToLower()
                        | x when x = typedefof<DateTime>    -> (valueBoxed :?> DateTime).ToString().ToLower()
                        | _ -> invalidOp "Error in toLowerFolder"

            acc.Replace(String.Format("#{{{0}.ToLower()}}", fst y), value)

        List.fold toLowerFolder str mapping

    let replaceToUpper (str: string) =
        let toUpperFolder (acc: string) (y: string * obj) =
            let valueBoxed = snd y
            let value = match valueBoxed.GetType() with
                        | x when x = typedefof<string>      -> (string (unbox valueBoxed)).ToUpper()
                        | x when x = typedefof<Gender>      -> (valueBoxed :?> Gender).ToString().ToUpper()
                        | x when x = typedefof<Nationality> -> (valueBoxed :?> Nationality).ToString().ToUpper()
                        | x when x = typedefof<DateTime>    -> (valueBoxed :?> DateTime).ToString().ToUpper()
                        | _ -> invalidOp "Error in toUpperFolder"

            acc.Replace(String.Format("#{{{0}.ToUpper()}}", fst y), value)

        List.fold toUpperFolder str mapping

    originalOutput |> replaceOrdinary |> replaceToLower |> replaceToUpper
