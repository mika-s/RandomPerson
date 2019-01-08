module internal ValidateSSNMode

open System
open RandomPersonLib
open CliUtil

let validate (lib: ValidatePerson) (country: Country) =
    let rec loop () = 
        printf "SSN: "
        let readSSN = Console.ReadLine ()

        match readSSN with
        | "q" | "Q" -> Environment.Exit 1
        | "b" | "B" -> false |> ignore
        | _         -> lib.ValidateSSN(country, readSSN) ||> printfn "%b: %s" |> loop

    loop ()

let validateSSNMode (ssn: string) (country: Country) =
    let lib = ValidatePerson()

    match ssn with
    | "" ->
        printHelp ()

        let rec mainloop() =
            if Console.KeyAvailable then
                match Console.ReadKey(true).KeyChar with
                | 'q' -> ()
                | 'd' -> validate lib Country.Denmark     |> printHelp |> mainloop
                | 'f' -> validate lib Country.Finland     |> printHelp |> mainloop
                | 'i' -> validate lib Country.Iceland     |> printHelp |> mainloop
                | 'N' -> validate lib Country.Netherlands |> printHelp |> mainloop
                | 'n' -> validate lib Country.Norway      |> printHelp |> mainloop
                | 's' -> validate lib Country.Sweden      |> printHelp |> mainloop
                | 'u' -> validate lib Country.USA         |> printHelp |> mainloop
                | _ -> mainloop()
            else
                mainloop ()

        mainloop()
    | _ -> lib.ValidateSSN(country, ssn) ||> printfn "%b: %s"
