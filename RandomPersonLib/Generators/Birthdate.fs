module internal Birthdate

open System
open RandomPersonLib

let generateBirthDate (random: Random) (birthDateOptions: IBirthDateOptions) =
    birthDateOptions.GetBirthDate random
