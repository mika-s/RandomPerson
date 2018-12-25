module internal Password

open System

let generateRandomCharacter (random: Random) =
    let characters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789#!@&/\\()"
    let randomNumber = random.Next(0, characters.Length)
    characters.[randomNumber]

let generateRandomCharactersPassword (random: Random) =
    let charactersToGenerate = random.Next(5, 15)
    [1 .. charactersToGenerate]
    |> List.map(fun _ -> generateRandomCharacter random)
    |> Array.ofList |> String

let generatePassword (random: Random) (passwords: string[]) (firstName: string) (lastName: string) (birthDate: DateTime) = 
    match random.Next(0, 100) with
    | r when 0 <= r && r <= 33  ->
        let randomNumber = random.Next(passwords.Length)
        passwords.[randomNumber]
    | r when 33 < r && r <= 50  -> generateRandomCharactersPassword random
    | r when 50 < r && r <= 55  -> random.Next(0, 10000).ToString("D4")
    | r when 55 < r && r <= 60  -> firstName.ToLower() + birthDate.Year.ToString().Substring(0, 2)
    | r when 60 < r && r <= 65  -> lastName.ToLower() + birthDate.Year.ToString().Substring(0, 2)
    | r when 65 < r && r <= 70  -> firstName.[0].ToString () + lastName + birthDate.Year.ToString().Substring(0, 2)
    | r when 70 < r && r <= 75  -> firstName.[0].ToString () + lastName + random.Next(0, 100).ToString()
    | r when 75 < r && r <= 80  -> birthDate.Day.ToString()  + birthDate.Month.ToString() + birthDate.Year.ToString()
    | r when 80 < r && r <= 85  -> birthDate.Year.ToString() + birthDate.Month.ToString() + birthDate.Day.ToString()
    | r when 85 < r && r <= 90  -> firstName.ToCharArray() |> Array.rev |> String
    | r when 90 < r && r <= 95  -> lastName .ToCharArray() |> Array.rev |> String
    | r when 95 < r && r <= 100 -> (firstName.ToCharArray() |> Array.rev |> String) + birthDate.Year.ToString()
    | _ -> invalidOp "Outside legal random range."
