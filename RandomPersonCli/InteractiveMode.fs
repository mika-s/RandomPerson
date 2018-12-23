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
            | 'd' -> lib.CreatePerson(Country.Denmark,     options) |> printPerson i.settings.InteractiveMode.ConsolePrint |> mainloop
            | 'f' -> lib.CreatePerson(Country.Finland,     options) |> printPerson i.settings.InteractiveMode.ConsolePrint |> mainloop
            | 'i' -> lib.CreatePerson(Country.Iceland,     options) |> printPerson i.settings.InteractiveMode.ConsolePrint |> mainloop
            | 'N' -> lib.CreatePerson(Country.Netherlands, options) |> printPerson i.settings.InteractiveMode.ConsolePrint |> mainloop
            | 'n' -> lib.CreatePerson(Country.Norway,      options) |> printPerson i.settings.InteractiveMode.ConsolePrint |> mainloop
            | 's' -> lib.CreatePerson(Country.Sweden,      options) |> printPerson i.settings.InteractiveMode.ConsolePrint |> mainloop
            | _ -> mainloop()
        else
            mainloop()

    mainloop()
