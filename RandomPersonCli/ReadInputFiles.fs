module internal ReadInputFiles

open Settings
open CliUtil

[<NoEquality;NoComparison>]
type InputFiles = {
    settings: Settings;
}

let assertDates (bdo: birthDateOptionsSettings) =
    match bdo.SetYearRangeManually.HasValue && bdo.SetYearRangeManually.Value, bdo.SetUsingAge.HasValue && bdo.SetUsingAge.Value with
    | (true, true) ->
        match bdo.Low.Value, bdo.High.Value with
        | (l, _) when l <= 1
            -> invalidArg "Low" "The variable 'Low' is too low. 1 is minimum."
        | (l, h) when h <= l
            -> invalidArg "Low" "The variable 'Low' cannot be equal or higher than 'High'"
        | (_, h) when 150 < h
            -> invalidArg "High" "The variable 'High' is too high. 150 is maximum."
        | _ -> 1 |> ignore
    | (true, false) ->
        match bdo.Low.Value, bdo.High.Value with
        | (l, _) when l <= 1800
            -> invalidArg "Low" "The variable 'Low' is too low. 1800 is minimum."
        | (l, h) when h <= l
            -> invalidArg "Low" "The variable 'Low' cannot be equal or higher than 'High'"
        | (_, h) when 2050 < h
            -> invalidArg "High" "The variable 'High' is too high. 2050 is maximum."
        | _ -> 1 |> ignore
    | (false, _) -> 1 |> ignore

let assertSettings (settings: Settings) =
    settings.InteractiveMode.Options.BirthDate |> assertDates
    settings.ListMode.Options.BirthDate        |> assertDates
    settings.TemplateMode.Options.BirthDate    |> assertDates

    settings

let createInputFiles (settings: Settings) =
    {
        settings = settings;
    }

let readInputFiles (settingsFilePath: string) = 
    settingsFilePath |> readDataFromJsonFile<Settings> |> assertSettings |> createInputFiles
