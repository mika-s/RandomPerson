module internal ReadInputFiles

open System.Text.RegularExpressions
open Settings
open CliUtil

[<NoEquality;NoComparison>]
type InputFiles = {
    settings: Settings;
}

let assertCreditcard (ccos: CreditcardOptionsSettings) =
    match ccos.CardIssuer with
    | "AmericanExpress" | "DinersClub" | "Discovery" | "MasterCard" | "Visa" -> 1 |> ignore
    | _ -> invalidArg "CardIssuer" ("'" + ccos.CardIssuer + "' is not a legal value.")

let assertDates (bdo: BirthDateOptionsSettings) =
    let dateRegex = Regex "(\d{4}-\d{2}-\d{2}|)"

    match bdo.BirthDateMode with
    | "DefaultCalendarYearRange" -> 1 |> ignore
    | "ManualCalendarYearRange"  ->
        match bdo.Low.Value, bdo.High.Value with
        | (l, _) when l < 1800
            -> invalidArg "Low" "The variable 'Low' is too low. 1800 is minimum."
        | (l, h) when h <= l
            -> invalidArg "Low" "The variable 'Low' cannot be equal or higher than 'High'"
        | (_, h) when 2050 < h
            -> invalidArg "High" "The variable 'High' is too high. 2050 is maximum."
        | _ -> 1 |> ignore
    | "ManualAgeRange"           ->
        match bdo.Low.Value, bdo.High.Value with
        | (l, _) when l < 1
            -> invalidArg "Low" "The variable 'Low' is too low. 1 is minimum."
        | (l, h) when h <= l
            -> invalidArg "Low" "The variable 'Low' cannot be equal or higher than 'High'"
        | (_, h) when 150 < h
            -> invalidArg "High" "The variable 'High' is too high. 150 is maximum."
        | _ -> 1 |> ignore
    | "Manual"                   ->
        match dateRegex.IsMatch bdo.ManualBirthDate with
        | true  -> 1 |> ignore
        | false -> invalidArg "ManualBirthDate" "ManualBirthDate has wrong format. The format should be yyyy-MM-dd."
    | _ -> invalidArg "BirthDateMode" ("'" + bdo.BirthDateMode + "' is not a legal value.")

let assertSettings (settings: Settings) =
    settings.InteractiveMode.Options.Creditcard |> assertCreditcard
    settings.ListMode.Options.Creditcard        |> assertCreditcard
    settings.TemplateMode.Options.Creditcard    |> assertCreditcard

    settings.InteractiveMode.Options.BirthDate  |> assertDates
    settings.ListMode.Options.BirthDate         |> assertDates
    settings.TemplateMode.Options.BirthDate     |> assertDates

    settings

let createInputFiles (settings: Settings) =
    {
        settings = settings;
    }

let readInputFiles (settingsFilePath: string) = 
    settingsFilePath |> readDataFromJsonFile<Settings> |> assertSettings |> createInputFiles
