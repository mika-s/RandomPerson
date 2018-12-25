module internal Address

open System
open PersonData

let generateAddress1 (random: Random) (addresses: string[]) (data: PersonData) = 
    let randomNumber = random.Next(addresses.Length)

    match data.Address.NumberLocation with
    | "Before" -> random.Next(1, 50).ToString() + " " + addresses.[randomNumber]
    | "After"  -> addresses.[randomNumber] + " " + random.Next(1, 50).ToString()
    | _ -> invalidArg "data.Misc.AddressNumberLocation" "Has to be either Before or After"
    
let generateAddress2 () = ""
