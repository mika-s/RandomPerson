module internal ValidatePANMode

open System
open RandomPersonLib

let validatePANMode (pan: string) =
    let printHelpForValidatePan () =
        printfn "\nUsage:"
        printfn "Quit: q\n\n"

    let lib = ValidatePerson()

    match pan with
    | "" ->
        printHelpForValidatePan ()

        let rec mainloop () = 
            printf "PAN: "
            let readPAN = Console.ReadLine ()

            match readPAN with
            | "q" | "Q" -> Environment.Exit 1
            | _         -> lib.ValidatePAN(readPAN) |> printfn "%b" |> mainloop

        mainloop ()
    | _ -> lib.ValidatePAN(pan) |> printfn "%b"
