module internal InteractiveMode

open System
open RandomPersonLib
open PrintToConsole
open ReadInputFiles
open Settings

let interactiveMode (settingsFilePath: string) =
    printfn "Usage:"
    printfn "Danish: d"
    printfn "Finnish: f"
    printfn "Norwegian: n"
    printfn "Swedish: s"
    printfn "Quit: q\n\n"

    let i = readInputFiles settingsFilePath
    let lib = RandomPerson()
    let options = genericOptionsToRandomPersonOptions i.settings.InteractiveMode.Options

    let rec mainloop() = 
        if Console.KeyAvailable then
            match Console.ReadKey(true).Key with
            | ConsoleKey.Q -> ()
            | ConsoleKey.D -> lib.CreatePerson(Nationality.Danish, options)    |> printPerson i.settings.InteractiveMode.ConsolePrint |> mainloop
            | ConsoleKey.F -> lib.CreatePerson(Nationality.Finnish, options)   |> printPerson i.settings.InteractiveMode.ConsolePrint |> mainloop
            | ConsoleKey.N -> lib.CreatePerson(Nationality.Norwegian, options) |> printPerson i.settings.InteractiveMode.ConsolePrint |> mainloop
            | ConsoleKey.S -> lib.CreatePerson(Nationality.Swedish, options)   |> printPerson i.settings.InteractiveMode.ConsolePrint |> mainloop
            | _ -> mainloop()
        else
            mainloop()

    mainloop()
