module internal Email

open System
open Util
open StringUtil

let generateEmailAddress (random: Random) (emailAddresses: string[]) (firstName: string) (lastName: string) (birthDate: DateTime) = 
    let randomNumber = random.Next(emailAddresses.Length)
    let domain = emailAddresses.[randomNumber]

    let chance = random.Next(0, 100)

    let localpart = match chance with
                    | Between 0  10  -> firstName
                    | Between 11 25  -> lastName
                    | Between 26 30  -> firstName + "." + lastName.ToLower()
                    | Between 31 35  -> firstName + birthDate.Year.ToString()
                    | Between 36 40  -> lastName + birthDate.Year.ToString()
                    | Between 41 45  -> firstName + lastName
                    | Between 46 50  -> firstName.[0].ToString () + lastName
                    | Between 51 55  -> firstName + lastName.[0].ToString ()
                    | Between 56 60  -> firstName.Substring(0, 2) + lastName
                    | Between 61 65  -> firstName + lastName |> substring 0 2
                    | Between 66 70  -> firstName + birthDate.Year.ToString() |> substring 2 2
                    | Between 71 75  -> lastName  + birthDate.Year.ToString() |> substring 2 2
                    | Between 76 100 -> firstName + random.Next(0, 20).ToString()
                    | _ -> invalidOp "Outside legal random range."

    let cleanLocalpart = localpart.ToLower()
                                  .Replace(" ", "")
                                  .Replace("æ", "ae")
                                  .Replace("ø", "oe")
                                  .Replace("å", "aa")
                                  .Replace("ä", "ae")
                                  .Replace("ö", "oe")
                                  .Replace("ü", "u")
                                  .Replace("ï", "i")
                                  .Replace("á", "a")
                                  .Replace("í", "i")
                                  .Replace("ó", "o")
                                  .Replace("ý", "y")
                                  .Replace("ú", "u")
                                  .Replace("þ", "th")
                                  .Replace("ð", "d")
                                  
    cleanLocalpart + "@" + domain
