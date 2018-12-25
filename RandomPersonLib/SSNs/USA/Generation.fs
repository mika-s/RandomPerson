module internal UsaSSNGeneration

open System

(*
    AAA-GG-SSSS
     | || |  |
     | || |  |--> Serial number
     | || |
     | || |-----> Hyphen
     | ||-------> Group number
     | |--------> Hyphen
     |----------> Area number

     Area number can't be 666 or 900-999.
*)

let generateAreaNumber (random: Random) =
    let rec loop () =
        let randomNumber = random.Next(001, 900)

        match randomNumber with
        | x when x = 666 -> loop ()
        | _ -> randomNumber.ToString("D3")

    loop ()

let generateGroupNumber (random: Random) = random.Next(01, 99).ToString("D2")

let generateSerialNumber (random: Random) = random.Next(0001, 9999).ToString("D4")

let anonymizeSSN (random: Random) (ssn: string) =
    random.Next(900, 999).ToString("D3") + ssn.[ 3 .. ssn.Length - 1 ]

let generateAmericanSSN (random: Random) (isAnonymizingSSN: bool) =
    let areaNumber   = generateAreaNumber random
    let groupNumber  = generateGroupNumber random
    let serialNumber = generateSerialNumber random
    let ssn = sprintf "%s-%s-%s" areaNumber groupNumber serialNumber

    match isAnonymizingSSN with
    | true  -> anonymizeSSN random ssn 
    | false -> ssn
