module internal Birthdate

open System
open RandomPersonLib

let generateBirthDate (random: Random) (isAllowingUnder18: bool) (birthDateOptions: BirthDateOptions)=
    let maxLegalBirthDate = 
        match (isAllowingUnder18, birthDateOptions.SetYearRangeManually, birthDateOptions.SetUsingAge) with
        | (  _  ,  true, false) -> DateTime(birthDateOptions.High, 1, 1)
        | (  _  ,  true,  true) -> DateTime.Now.Subtract(TimeSpan(365 * birthDateOptions.Low, 0, 0, 0))
        | ( true, false,   _  ) -> DateTime.Today
        | (false, false,   _  ) -> DateTime.Today.Subtract(TimeSpan.FromDays(365.0*18.5))

    let minLegalBirthDate = 
        match (isAllowingUnder18, birthDateOptions.SetYearRangeManually, birthDateOptions.SetUsingAge) with
        | (  _  ,  true, false) -> DateTime(birthDateOptions.Low, 1, 1)
        | (  _  ,  true,  true) -> DateTime.Now.Subtract(TimeSpan(365 * birthDateOptions.High, 0, 0, 0))
        | ( true, false,   _  ) -> DateTime.Today
        | (false, false,   _  ) -> DateTime(1920, 1, 1)

    let year = random.Next(minLegalBirthDate.Year, maxLegalBirthDate.Year)
    let month = random.Next(1, 12)
    let day = match () with
              | () when month = 1 || month = 3 || month = 5 || month = 7 || month = 8 || month = 10 || month = 12  -> random.Next(1, 31)
              | () when month = 4 || month = 6 || month = 9 || month = 11 -> random.Next(1, 30)
              | () when month = 2 && year % 4 = 0 && year <> 1900 -> 29
              | () when month = 2 -> random.Next(1, 28)
              | _ -> invalidArg "month" "Illegal month."

    sprintf "%i-%0i-%0i" year month day |> DateTime.Parse
