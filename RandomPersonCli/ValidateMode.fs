module internal ValidateMode

open System
open RandomPersonLib

let validate (lib: RandomPerson) (nationality: Nationality) =
    let rec loop () = 
        printf "SSN: "
        let readSSN = Console.ReadLine ()

        match readSSN with
        | "q" | "Q" -> Environment.Exit 1
        | "b" | "B" -> false |> ignore
        | _         -> lib.ValidateSSN(nationality, readSSN) |> printfn "%b" |> loop

    loop ()

let printHelp () =
    printfn "\nUsage:"
    printfn "Danish: d"
    printfn "Dutch: D"
    printfn "Finnish: f"
    printfn "Icelandic: i"
    printfn "Norwegian: n"
    printfn "Swedish: s"
    printfn "Go back: b"
    printfn "Quit: q\n\n"

let validateMode (ssn: string) (nationality: Nationality) =
    let lib = RandomPerson()

    match ssn with
    | "" ->
        printHelp ()

        let rec mainloop() =
            if Console.KeyAvailable then
                match Console.ReadKey(true).KeyChar with
                | 'q' -> ()
                | 'd' -> validate lib Nationality.Danish    |> printHelp |> mainloop
                | 'D' -> validate lib Nationality.Dutch     |> printHelp |> mainloop
                | 'f' -> validate lib Nationality.Finnish   |> printHelp |> mainloop
                | 'i' -> validate lib Nationality.Icelandic |> printHelp |> mainloop
                | 'n' -> validate lib Nationality.Norwegian |> printHelp |> mainloop
                | 's' -> validate lib Nationality.Swedish   |> printHelp |> mainloop
                | _ -> mainloop()
            else
                mainloop ()

        mainloop()
    | _ ->
        match nationality with
        | Nationality.Danish    -> lib.ValidateSSN(Nationality.Danish,    ssn) |> printfn "%b"
        | Nationality.Dutch     -> lib.ValidateSSN(Nationality.Dutch,     ssn) |> printfn "%b"
        | Nationality.Finnish   -> lib.ValidateSSN(Nationality.Finnish,   ssn) |> printfn "%b"
        | Nationality.Icelandic -> lib.ValidateSSN(Nationality.Icelandic, ssn) |> printfn "%b"
        | Nationality.Norwegian -> lib.ValidateSSN(Nationality.Norwegian, ssn) |> printfn "%b"
        | Nationality.Swedish   -> lib.ValidateSSN(Nationality.Swedish,   ssn) |> printfn "%b"
        | _ -> invalidArg "nationality" "Illegal nationality."
