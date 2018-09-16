module internal TemplateMode

open RandomPersonLib
open ReadInputFiles
open Settings

let templateMode (settingsFilePath: string) (amount: int) (nationality: Nationality) =
    let i = readInputFiles settingsFilePath
    let lib = RandomPerson()
    let options = genericOptionsToRandomPersonOptions i.settings.TemplateMode.Options

    lib.CreatePeopleTemplated(amount, nationality, i.settings.TemplateMode.Print.Output, options)
    |> List.iter (printfn "%s")
