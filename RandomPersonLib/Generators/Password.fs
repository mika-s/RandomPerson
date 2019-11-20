module internal Password

open System
open Util
open StringUtil

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
    let chance = random.Next(0, 100)

    match chance with
    | Between 0  33  -> passwords.[random.Next(passwords.Length)]
    | Between 34 50  -> generateRandomCharactersPassword random
    | Between 51 55  -> random.Next(0, 10000).ToString("D4")
    | Between 56 60  -> firstName.ToLower() + birthDate.Year.ToString() |> substring 0 2
    | Between 61 65  -> lastName .ToLower() + birthDate.Year.ToString() |> substring 0 2
    | Between 66 70  -> firstName.[0].ToString () + lastName + birthDate.Year.ToString() |> substring 0 2
    | Between 71 75  -> firstName.[0].ToString () + lastName + random.Next(0, 100).ToString()
    | Between 76 80  -> birthDate.Day.ToString () + birthDate.Month.ToString() + birthDate.Year.ToString()
    | Between 81 85  -> birthDate.Year.ToString() + birthDate.Month.ToString() + birthDate.Day.ToString()
    | Between 86 90  -> firstName .ToCharArray() |> Array.rev |> String
    | Between 91 95  -> lastName  .ToCharArray() |> Array.rev |> String
    | Between 96 100 -> (firstName.ToCharArray() |> Array.rev |> String) + birthDate.Year.ToString()
    | _ -> invalidOp "Outside legal random range."
