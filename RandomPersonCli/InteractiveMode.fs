module internal InteractiveMode

open System
open RandomPersonLib
open CliUtil
open PrintToConsole
open ReadInputFiles
open Settings

let interactiveMode (settingsFilePath: string) =
    printHelp ()

    let i = readInputFiles settingsFilePath
    let lib = RandomPerson()
    let options = genericOptionsToRandomPersonOptions i.settings.InteractiveMode.Options

    let rec mainloop() = 
        if Console.KeyAvailable then
            match Console.ReadKey(true).KeyChar with
            | 'q' -> ()
            | 'd' -> lib.CreatePerson(Nationality.Danish,    options) |> printPerson i.settings.InteractiveMode.ConsolePrint |> mainloop
            | 'D' -> lib.CreatePerson(Nationality.Dutch,     options) |> printPerson i.settings.InteractiveMode.ConsolePrint |> mainloop
            | 'f' -> lib.CreatePerson(Nationality.Finnish,   options) |> printPerson i.settings.InteractiveMode.ConsolePrint |> mainloop
            | 'i' -> lib.CreatePerson(Nationality.Icelandic, options) |> printPerson i.settings.InteractiveMode.ConsolePrint |> mainloop
            | 'n' -> lib.CreatePerson(Nationality.Norwegian, options) |> printPerson i.settings.InteractiveMode.ConsolePrint |> mainloop
            | 's' -> lib.CreatePerson(Nationality.Swedish,   options) |> printPerson i.settings.InteractiveMode.ConsolePrint |> mainloop
            | _ -> mainloop()
        else
            mainloop()

    mainloop()
