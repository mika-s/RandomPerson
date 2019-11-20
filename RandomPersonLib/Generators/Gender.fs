module internal Gender

open System
open RandomPersonLib
open Util

let generateGender (random: Random) = 
    let chance = random.Next(0, 100)

    match chance with
    | Between 0 50 -> Gender.Male
    | _            -> Gender.Female
