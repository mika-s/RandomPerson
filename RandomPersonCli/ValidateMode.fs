module internal ValidateMode

open System
open RandomPersonLib

let validateDK (lib: RandomPerson) =
    let rec loop () = 
        printf "SSN: "
        let readSSN = Console.ReadLine ()

        match readSSN with
        | "q" | "Q" -> Environment.Exit 1
        | "b" | "B" -> false |> ignore
        | _ -> lib.ValidateSSN(Nationality.Danish, readSSN) |> printfn "%b" |> loop

    loop ()

let validateFI (lib: RandomPerson) =
    let rec loop () = 
        printf "SSN: "
        let readSSN = Console.ReadLine ()

        match readSSN with
        | "q" | "Q" -> Environment.Exit 1
        | "b" | "B" -> false |> ignore
        | _ -> lib.ValidateSSN(Nationality.Finnish, readSSN) |> printfn "%b" |> loop

    loop ()

let validateIC (lib: RandomPerson) =
    let rec loop () = 
        printf "SSN: "
        let readSSN = Console.ReadLine ()

        match readSSN with
        | "q" | "Q" -> Environment.Exit 1
        | "b" | "B" -> false |> ignore
        | _ -> lib.ValidateSSN(Nationality.Icelandic, readSSN) |> printfn "%b" |> loop

    loop ()

let validateNO (lib: RandomPerson) =
    let rec loop () = 
        printf "SSN: "
        let readSSN = Console.ReadLine ()

        match readSSN with
        | "q" | "Q" -> Environment.Exit 1
        | "b" | "B" -> false |> ignore
        | _ -> lib.ValidateSSN(Nationality.Norwegian, readSSN) |> printfn "%b" |> loop

    loop ()

let validateSE (lib: RandomPerson) =
    let rec loop () = 
        printf "SSN: "
        let readSSN = Console.ReadLine ()

        match readSSN with
        | "q" | "Q" -> Environment.Exit 1
        | "b" | "B" -> false |> ignore
        | _ -> lib.ValidateSSN(Nationality.Swedish, readSSN) |> printfn "%b" |> loop

    loop ()

let printHelp () =
    printfn "\nUsage:"
    printfn "Danish: d"
    printfn "Finnish: f"
    printfn "Icelandic: i"
    printfn "Norwegian: n"
    printfn "Swedish: s"
    printfn "Go back: b"
    printfn "Quit: q\n\n"

let validateMode () =
    let lib = RandomPerson()

    printHelp ()

    let rec mainloop() =
        if Console.KeyAvailable then
            match Console.ReadKey(true).Key with
            | ConsoleKey.Q -> ()
            | ConsoleKey.D -> validateDK lib |> printHelp |> mainloop
            | ConsoleKey.F -> validateFI lib |> printHelp |> mainloop
            | ConsoleKey.I -> validateIC lib |> printHelp |> mainloop
            | ConsoleKey.N -> validateNO lib |> printHelp |> mainloop
            | ConsoleKey.S -> validateSE lib |> printHelp |> mainloop
            | _ -> mainloop()
        else
            mainloop ()

    mainloop()
