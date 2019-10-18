module internal RandomUtil

open System

let randomForUtil = Random()

let generateRandomDateBetween (random: Random) (minYear: int) (maxYear: int) =
    let year = random.Next(minYear, maxYear)
    let month = random.Next(1, 12)
    let day = match () with
              | () when month = 1 || month = 3 || month = 5 || month = 7 || month = 8 || month = 10 || month = 12 -> random.Next(1, 31)
              | () when month = 4 || month = 6 || month = 9 || month = 11 -> random.Next(1, 30)
              | () when month = 2 && year % 4 = 0 && year <> 1900 -> 29
              | () when month = 2 -> random.Next(1, 28)
              | _ -> invalidArg "month" "Illegal month."

    sprintf "%i-%0i-%0i" year month day |> DateTime.Parse

let randomIntBetween (min: int) (max: int) = randomForUtil.Next(min, max + 1)
let randomIntBetweenWithStep (min: int) (step: int) (max: int) = (randomIntBetween 0 ((max - min) / step)) * step + min

let randomFloatBetween (min: float) (max: float) = randomForUtil.NextDouble() * (max - min) + min
let randomFloatBetweenWithStep (min: float) (step: float) (max: float) = (float (randomIntBetween 0 (int ((max - min) / step))))  * step + min

let generateRandomNumberString (random: Random) (amount: int) (min: int) (max: int) =
    ("", [1 .. amount]) ||> List.fold (fun state _ -> state + sprintf "%d" (random.Next(min, max)))

let randomUppercaseLetter (random: Random) =
    let alphabet = "ABCDEFGHIJKLMNOPQRSTUVWXYZ"
    let randomNumber = random.Next(0, alphabet.Length - 1)
    alphabet.[randomNumber]

let boxMullerTransform () =
    let u1 = randomForUtil.NextDouble()
    let u2 = randomForUtil.NextDouble()
    let z0 = sqrt(-2.0 * log u1) * cos(2.0 * Math.PI * u2)
    let z1 = sqrt(-2.0 * log u1) * sin(2.0 * Math.PI * u2)

    z0, z1

let normallyDistributedInt (mean: int) (std: int) =
    let z0, _ = boxMullerTransform ()
    int (z0 * float std + float mean)

let normallyDistributedFloat (mean: float) (std: float) =
    let z0, _ = boxMullerTransform ()
    z0 * std + mean
