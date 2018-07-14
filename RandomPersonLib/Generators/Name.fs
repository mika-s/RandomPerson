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
    | _ -> failwith "Illegal gender."

let generateFirstName (random: Random) (gender: Gender) (data: PersonData)  =
    let firstName = makeFirstName random gender data

    let percentChanceForMiddleName = 25

    match random.Next(1, 100) with
    | x when 1 <= x && x <= percentChanceForMiddleName -> firstName + " " + makeFirstName random gender data
    | _                                                -> firstName

let generateLastName (random: Random) (data: PersonData) =
    let randomNoLastName = random.Next(data.LastNames.Length)
    data.LastNames.[randomNoLastName]
