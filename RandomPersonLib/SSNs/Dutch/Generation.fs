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

let generateDutchIndividualNumber (random: Random) =
    let i1 = random.Next(0, 10)
    let i2 = random.Next(0, 10)
    let i3 = random.Next(0, 10)
    let i4 = random.Next(0, 10)
    let i5 = random.Next(0, 10)
    let i6 = random.Next(0, 10)
    let i7 = random.Next(0, 10)
    let i8 = random.Next(0, 10)
    sprintf "%d%d%d%d%d%d%d%d" i1 i2 i3 i4 i5 i6 i7 i8

let generateDutchChecksum (individualNumber: string)  =
    let i1 = Convert.ToInt32(individualNumber.Substring(0, 1))
    let i2 = Convert.ToInt32(individualNumber.Substring(1, 1))
    let i3 = Convert.ToInt32(individualNumber.Substring(2, 1))
    let i4 = Convert.ToInt32(individualNumber.Substring(3, 1))
    let i5 = Convert.ToInt32(individualNumber.Substring(4, 1))
    let i6 = Convert.ToInt32(individualNumber.Substring(5, 1))
    let i7 = Convert.ToInt32(individualNumber.Substring(6, 1))
    let i8 = Convert.ToInt32(individualNumber.Substring(7, 1))

    let cs = (9 * i1 + 8 * i2 + 7 * i3 + 6 * i4 + 5 * i5 + 4 * i6 + 3 * i7 + 2 * i8) % 11

    sprintf "%d" cs

let anonymizeSSN (ssn: string) = incrementNumberInString ssn 5

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
