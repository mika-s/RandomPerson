module internal Name

open System
open RandomPersonLib
open PersonData

let makeFirstName (random: Random) (gender: Gender) (data: PersonData) =
    let randomNoMaleFirstName   = random.Next(data.MaleFirstNames.Length)
    let randomNoFemaleFirstName = random.Next(data.FemaleFirstNames.Length)

    match gender with
    | Gender.Male   -> data.MaleFirstNames.[randomNoMaleFirstName]
    | Gender.Female -> data.FemaleFirstNames.[randomNoFemaleFirstName]
    | _ -> invalidArg "gender" "Illegal gender."

let generateFirstName (random: Random) (gender: Gender) (data: PersonData)  =
    let firstName = makeFirstName random gender data

    let percentChanceForMiddleName = 25

    match random.Next(0, 100) with
    | x when 0 <= x && x < percentChanceForMiddleName ->
        let rec loop () =
            let middleName = makeFirstName random gender data

            match middleName with
            | m when m = firstName -> loop()
            | _                    -> firstName + " " + middleName

        loop ()
    | _ -> firstName

let generateLastName (random: Random) (gender: Gender) (data: PersonData) =
    match data.LastNames <> null && 0 < data.LastNames.Length with
    | true ->
        let randomNoLastName = random.Next(data.LastNames.Length)
        data.LastNames.[randomNoLastName]
    | false ->
        match data.MaleLastNames   <> null && 0 < data.MaleLastNames  .Length,
              data.FemaleLastNames <> null && 0 < data.FemaleLastNames.Length with
        | (true, true) ->
            match gender with
            | Gender.Male ->
                let randomNoMaleLastName = random.Next(data.MaleLastNames.Length)
                data.MaleLastNames.[randomNoMaleLastName]
            | Gender.Female ->
                let randomNoFemaleLastName = random.Next(data.FemaleLastNames.Length)
                data.FemaleLastNames.[randomNoFemaleLastName]
            | _ -> invalidArg "gender" "Illegal gender."
        | _ -> invalidArg "LastNames, MaleLastNames, FemaleLastNames" "Either populate LastNames or MaleLastNames/FemaleLastNames."
