module internal DutchSSNGeneration

(*
    XXXXXXXXC
    |||||||||-> Checksum
    ||||||||--> Random number
    |||||||---> Random number
    ||||||----> Random number
    |||||-----> Random number
    ||||------> Random number
    |||-------> Random number
    ||--------> Random number
    |---------> Random number
*)

open System
open Util

let generateDutchIndividualNumber (random: Random) = generateRandomNumberString random 8 0 10

let generateDutchChecksum (individualNumber: string) =
    let individualNumberArray = intArrayFromString individualNumber
    let coefficients = [| 9; 8; 7; 6; 5; 4; 3; 2 |]

    (individualNumberArray, coefficients) ||> Array.map2 (*)
    |> Array.sum |> modulus 11 |> sprintf "%d"

let anonymizeSSN (ssn: string) = ssn |> incrementAtPosition 5

let generateDutchSSN (random: Random) (isAnonymizingSSN: bool) =
    let rec loop () =
        let individualNumber = generateDutchIndividualNumber random 
        let checksum = generateDutchChecksum individualNumber
    
        if checksum.Length <> 2 then
            let ssn = sprintf "%s%s" individualNumber checksum
            match isAnonymizingSSN with
            | true  -> anonymizeSSN ssn 
            | false -> ssn
        else
            loop ()

    loop ()
