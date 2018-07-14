module internal Address

open System

let generateAddress1 (random: Random) (addresses: string[]) = 
    let randomNumber = random.Next(addresses.Length)

    addresses.[randomNumber] + " " + random.Next(50).ToString()

let generateAddress2 () = ""
