module internal Gender

open System
open RandomPersonLib

let generateGender (random: Random) = 
    let randomNumber = random.Next(100)

    match randomNumber with
    | randomNumber when randomNumber < 50 -> Gender.Male
    | _                                   -> Gender.Female
