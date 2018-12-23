module internal TemplateMode

open RandomPersonLib
open ReadInputFiles
open Settings

let templateMode (settingsFilePath: string) (amount: int) (country: Country) =
    let i = readInputFiles settingsFilePath
    let lib = RandomPerson()
    let options = genericOptionsToRandomPersonOptions i.settings.TemplateMode.Options

    lib.CreatePeopleTemplated(amount, country, i.settings.TemplateMode.Print.Output, options)
    |> List.iter (printfn "%s")
