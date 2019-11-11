module internal ReadInputFiles

open System
open System.IO
open System.Reflection
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

    let lowestCalendarYear  = 1800
    let highestCalendarYear = 2050
    let lowestAge  = 1
    let highestAge = 150

    match bdo.BirthDateMode with
    | "DefaultCalendarYearRange" -> 1 |> ignore
    | "ManualCalendarYearRange"  ->
        match bdo.Low.Value, bdo.High.Value with
        | (l, _) when l < lowestCalendarYear
            -> invalidArg "Low" (String.Format("The variable 'Low' is too low. {0} is minimum.", lowestCalendarYear))
        | (l, h) when h <= l
            -> invalidArg "Low" "The variable 'Low' cannot be equal or higher than 'High'"
        | (_, h) when highestCalendarYear < h
            -> invalidArg "High" (String.Format("The variable 'High' is too high. {0} is maximum.", highestCalendarYear))
        | _ -> 1 |> ignore
    | "ManualAgeRange"           ->
        match bdo.Low.Value, bdo.High.Value with
        | (l, _) when l < lowestAge
            -> invalidArg "Low" (String.Format("The variable 'Low' is too low. {0} is minimum.", lowestAge))
        | (l, h) when h <= l
            -> invalidArg "Low" "The variable 'Low' cannot be equal or higher than 'High'"
        | (_, h) when highestAge < h
            -> invalidArg "High" (String.Format("The variable 'High' is too high. {0} is maximum.", highestAge))
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
    (*
        Make sure we are in the correct working folder. Using "dotnet run" will use the folder it's executing in
        as the working folder, and we don't want that.
    *)
    Assembly.GetExecutingAssembly().Location |> Path.GetDirectoryName |> Directory.SetCurrentDirectory

    settingsFilePath |> readDataFromJsonFile<Settings> |> assertSettings |> createInputFiles
